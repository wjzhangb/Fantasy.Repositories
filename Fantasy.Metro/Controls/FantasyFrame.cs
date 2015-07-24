using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Fantasy.Metro.Controls
{
    public class FantasyFrame : ContentControl
    {
        public FantasyFrame()
        {
            this.DefaultStyleKey = typeof(FantasyFrame);
            
            this.History = new Stack<Uri>();
            this.ContentCache = new Dictionary<Uri, Object>();
            this.ChildFrames = new List<WeakReference<FantasyFrame>>();

            this.CommandBindings.Add(new CommandBinding(
                NavigationCommands.BrowseBack, 
                OnBrowseBack,
                OnCanBrowseBack));

            this.CommandBindings.Add(new CommandBinding(
                NavigationCommands.GoToPage, 
                OnGoToPage,
                OnCanGoToPage));
            
            this.Loaded += OnLoaded;
        }

        private static void OnKeepContentAliveChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((FantasyFrame)o).OnKeepContentAliveChanged((Boolean)e.NewValue);
        }
        private void OnKeepContentAliveChanged(Boolean keepAlive)
        {
            // clear content cache
            this.ContentCache.Clear();
        }

        private static void OnContentLoaderChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                // null values for content loader not allowed
                throw new ArgumentNullException("ContentLoader");
            }
        }

        private static void OnSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            FantasyFrame frame = o as FantasyFrame;
            frame.OnSourceChanged((Uri)e.OldValue, (Uri)e.NewValue,
                frame.NavigatingParameter);
        }

        private void OnSourceChanged(Uri oldValue, Uri newValue, Object parameter)
        {
            // if resetting source or old source equals new, don't do anything
            if (this.IsResetSource || newValue != null && newValue.Equals(oldValue))
            {
                return;
            }

            // handle fragment navigation
            string newFragment = null;
            var oldValueNoFragment = NavigationManager.RemoveFragment(oldValue);
            var newValueNoFragment = NavigationManager.RemoveFragment(newValue, out newFragment);

            if (newValueNoFragment != null && newValueNoFragment.Equals(oldValueNoFragment))
            {
                // fragment navigation
                FragmentNavigationEventArgs args = new FragmentNavigationEventArgs
                {
                    Fragment = newFragment,
                    Parameter = parameter,
                };

                OnFragmentNavigation(this.Content as IContent, args);
            }
            else
            {
                var navType = this.IsNavigatingHistory ? NavigationType.Back : NavigationType.New;

                // only invoke CanNavigate for new navigation
                if (!this.IsNavigatingHistory && !CanNavigate(oldValue, newValue, navType))
                {
                    return;
                }

                Navigate(oldValue, newValue, navType, parameter);
            }
        }

        private bool CanNavigate(Uri oldValue, Uri newValue, NavigationType navigationType)
        {
            var cancelArgs = new NavigatingCancelEventArgs
            {
                Frame = this,
                Source = newValue,
                IsParentFrameNavigating = true,
                NavigationType = navigationType,
                Cancel = false,
            };
            OnNavigating(this.Content as IContent, cancelArgs);

            // check if navigation cancelled
            if (cancelArgs.Cancel)
            {
                if (this.Source != oldValue)
                {
                    // enqueue the operation to reset the source back to the old value
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        this.IsResetSource = true;
                        SetCurrentValue(SourceProperty, oldValue);
                        this.IsResetSource = false;
                    }));
                }
                return false;
            }

            return true;
        }

        private void Navigate(Uri oldValue, Uri newValue, NavigationType navigationType, Object parameter)
        {
            // set IsLoadingContent state
            SetValue(IsLoadingContentPropertyKey, true);

            // cancel previous load content task (if any)
            // note: no need for thread synchronization, this code always executes on the UI thread
            if (this.TokenSource != null)
            {
                this.TokenSource.Cancel();
                this.TokenSource = null;
            }

            // push previous source onto the history stack (only for new navigation types)
            if (oldValue != null && navigationType == NavigationType.New)
            {
                this.History.Push(oldValue);
            }

            object newContent = null;

            if (newValue != null)
            {
                // content is cached on uri without fragment
                var newValueNoFragment = NavigationManager.RemoveFragment(newValue);

                if (navigationType == NavigationType.Refresh || !this.ContentCache.TryGetValue(newValueNoFragment, out newContent))
                {
                    var localTokenSource = new CancellationTokenSource();
                    this.TokenSource = localTokenSource;
                    // load the content (asynchronous!)
                    var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
                    var task = this.ContentLoader.LoadContentAsync(newValue, this.TokenSource.Token);

                    task.ContinueWith(t =>
                    {
                        try
                        {
                            if (t.IsCanceled || localTokenSource.IsCancellationRequested)
                            {
                                //Debug.WriteLine("Cancelled navigation to '{0}'", newValue);
                            }
                            else if (t.IsFaulted)
                            {
                                // raise failed event
                                var failedArgs = new NavigationFailedEventArgs
                                {
                                    Frame = this,
                                    Source = newValue,
                                    Error = t.Exception.InnerException,
                                    Handled = false
                                };

                                OnNavigationFailed(failedArgs);

                                // if not handled, show error as content
                                newContent = failedArgs.Handled ? null : failedArgs.Error;

                                SetContent(newValue, navigationType, newContent, true, parameter);
                            }
                            else
                            {
                                newContent = t.Result;
                                if (ShouldKeepContentAlive(newContent))
                                {
                                    // keep the new content in memory
                                    this.ContentCache[newValueNoFragment] = newContent;
                                }

                                SetContent(newValue, navigationType, newContent, false, parameter);
                            }
                        }
                        finally
                        {
                            // clear global tokenSource to avoid a Cancel on a disposed object
                            if (this.TokenSource == localTokenSource)
                            {
                                this.TokenSource = null;
                            }

                            // and dispose of the local tokensource
                            localTokenSource.Dispose();
                        }
                    }, scheduler);
                    return;
                }
            }

            // newValue is null or newContent was found in the cache
            SetContent(newValue, navigationType, newContent, false, parameter);
        }

        private void SetContent(Uri newSource, NavigationType navigationType, object newContent, bool contentIsError, Object parameter)
        {
            var oldContent = this.Content as IContent;

            // assign content
            this.Content = newContent;

            // do not raise navigated event when error
            if (!contentIsError)
            {
                var args = new NavigationEventArgs
                {
                    Frame = this,
                    Source = newSource,
                    Content = newContent,
                    NavigationType = navigationType,
                    Parameter = parameter,
                };

                OnNavigated(oldContent, newContent as IContent, args);
            }

            // set IsLoadingContent to false
            SetValue(IsLoadingContentPropertyKey, false);

            if (!contentIsError)
            {
                // and raise optional fragment navigation events
                string fragment;
                NavigationManager.RemoveFragment(newSource, out fragment);
                if (fragment != null)
                {
                    // fragment navigation
                    var fragmentArgs = new FragmentNavigationEventArgs
                    {
                        Fragment = fragment
                    };

                    OnFragmentNavigation(newContent as IContent, fragmentArgs);
                }
            }
        }

        private IEnumerable<FantasyFrame> GetChildFrames()
        {
            var refs = this.ChildFrames.ToArray();
            foreach (var r in refs)
            {
                var valid = false;
                FantasyFrame frame;
                if (r.TryGetTarget(out frame)) 
                {
                    if (NavigationManager.FindFrame(null, frame) == this)
                    {
                        valid = true;
                        yield return frame;
                    }
                }

                if (!valid)
                {
                    this.ChildFrames.Remove(r);
                }
            }
        }

        private void OnFragmentNavigation(IContent content, FragmentNavigationEventArgs e)
        {
            // invoke optional IContent.OnFragmentNavigation
            if (content != null)
            {
                content.OnFragmentNavigation(e);
            }

            // raise the FragmentNavigation event
            if (FragmentNavigation != null)
            {
                FragmentNavigation(this, e);
            }
        }

        private void OnNavigating(IContent content, NavigatingCancelEventArgs e)
        {
            // first invoke child frame navigation events
            foreach (var f in GetChildFrames())
            {
                f.OnNavigating(f.Content as IContent, e);
            }

            e.IsParentFrameNavigating = e.Frame != this;

            // invoke IContent.OnNavigating (only if content implements IContent)
            if (content != null)
            {
                content.OnNavigatingFrom(e);
            }

            // raise the Navigating event
            if (Navigating != null)
            {
                Navigating(this, e);
            }
        }

        private void OnNavigated(IContent oldContent, IContent newContent, NavigationEventArgs e)
        {
            // invoke IContent.OnNavigatedFrom and OnNavigatedTo
            if (oldContent != null)
            {
                oldContent.OnNavigatedFrom(e);
            }
            if (newContent != null)
            {
                newContent.OnNavigatedTo(e);
            }

            // raise the Navigated event
            if (Navigated != null)
            {
                Navigated(this, e);
            }
        }

        private void OnNavigationFailed(NavigationFailedEventArgs e)
        {
            if (NavigationFailed != null)
            {
                NavigationFailed(this, e);
            }
        }

        private void OnCanBrowseBack(object sender, CanExecuteRoutedEventArgs e)
        {
            // only enable browse back for source frame, do not bubble
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.History.Count > 0;
            }
        }

        private void OnCanCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Content != null;
            }
        }

        private void OnCanGoToPage(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = e.Parameter is String || e.Parameter is Uri;
            }
        }

        private void OnCanRefresh(object sender, CanExecuteRoutedEventArgs e)
        {
            if (HandleRoutedEvent(e))
            {
                e.CanExecute = this.Source != null;
            }
        }

        private void OnBrowseBack(object target, ExecutedRoutedEventArgs e)
        {
            if (this.History.Count > 0)
            {
                var oldValue = this.Source;
                var newValue = this.History.Peek();     // do not remove just yet, navigation may be cancelled

                if (CanNavigate(oldValue, newValue, NavigationType.Back))
                {
                    this.IsNavigatingHistory = true;
                    SetCurrentValue(SourceProperty, this.History.Pop());
                    this.IsNavigatingHistory = false;
                }
            }
        }

        private void OnGoToPage(object target, ExecutedRoutedEventArgs e)
        {
            var newValue = e.Parameter as Uri;

            if (newValue == null)
            {
                var newValueStr = e.Parameter as string;
                if (newValueStr != null)
                {
                    newValue = new Uri(newValueStr, UriKind.RelativeOrAbsolute);
                }
                else
                {
                    // no valid command parameter (not a uri or string), ignore
                    return;
                }
            }
            SetCurrentValue(SourceProperty, newValue);
        }

        private void OnRefresh(object target, ExecutedRoutedEventArgs e)
        {
            if (CanNavigate(this.Source, this.Source, NavigationType.Refresh))
            {
                Navigate(this.Source, this.Source, NavigationType.Refresh, this.NavigatingParameter);
            }
        }

        private void OnCopy(object target, ExecutedRoutedEventArgs e)
        {
            // copies the string representation of the current content to the clipboard
            Clipboard.SetText(this.Content.ToString());
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            FantasyFrame parent = NavigationManager.FindFrame(null, this);
            if (parent != null)
            {
                parent.RegisterChildFrame(this);
            }
        }

        private void RegisterChildFrame(FantasyFrame frame)
        {
            // do not register existing frame
            if (!GetChildFrames().Contains(frame))
            {
                var r = new WeakReference<FantasyFrame>(frame);
                this.ChildFrames.Add(r);
            }
        }

        private bool ShouldKeepContentAlive(object content)
        {
            var o = content as DependencyObject;
            if (o != null)
            {
                var result = GetKeepAlive(o);

                // if a value exists for given content, use it
                if (result.HasValue)
                {
                    return result.Value;
                }
            }
            // otherwise let the ModernFrame decide
            return this.KeepContentAlive;
        }

        public static bool? GetKeepAlive(DependencyObject o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }
            
            return (bool?)o.GetValue(KeepAliveProperty);
        }

        public static void SetKeepAlive(DependencyObject o, bool? value)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o");
            }

            o.SetValue(KeepAliveProperty, value);
        }

        public void SetContent(Object content)
        {
            this.Content = content;
        }

        // Prevents parent frame from handing routed commands.
        private Boolean HandleRoutedEvent(CanExecuteRoutedEventArgs args)
        {
            var originalSource = args.OriginalSource as DependencyObject;

            if (originalSource == null)
            {
                return false;
            }
            return originalSource.AncestorsAndSelf().OfType<FantasyFrame>().FirstOrDefault() == this;
        }

        public bool KeepContentAlive
        {
            get { return (bool)GetValue(KeepContentAliveProperty); }
            set { SetValue(KeepContentAliveProperty, value); }
        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public bool IsLoadingContent
        {
            get { return (bool)GetValue(IsLoadingContentProperty); }
        }

        public Uri Source
        {
            get { return (Uri)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty KeepAliveProperty =
            DependencyProperty.RegisterAttached("KeepAlive", 
            typeof(Boolean?),
            typeof(FantasyFrame), 
            new PropertyMetadata(null));
        
        public static readonly DependencyProperty KeepContentAliveProperty = 
            DependencyProperty.Register("KeepContentAlive",
                typeof(Boolean), 
                typeof(FantasyFrame), 
                new PropertyMetadata(true, OnKeepContentAliveChanged));

        public static readonly DependencyProperty ContentLoaderProperty =
            DependencyProperty.Register("ContentLoader", 
                typeof(IContentLoader), 
                typeof(FantasyFrame), 
                new PropertyMetadata(new DefaultContentLoader(), OnContentLoaderChanged));

        private static readonly DependencyPropertyKey IsLoadingContentPropertyKey = 
            DependencyProperty.RegisterReadOnly("IsLoadingContent",
                typeof(Boolean), 
                typeof(FantasyFrame),
                new PropertyMetadata(false));
        
        public static readonly DependencyProperty IsLoadingContentProperty = IsLoadingContentPropertyKey.DependencyProperty;

        public static readonly DependencyProperty SourceProperty = 
            DependencyProperty.Register("Source",
                typeof(Uri), 
                typeof(FantasyFrame), 
                new PropertyMetadata(OnSourceChanged));

        // Occurs when navigation to a content fragment begins.
        public event EventHandler<FragmentNavigationEventArgs> FragmentNavigation;

        // The navigating event is also raised when a parent frame is navigating. 
        // This allows for cancelling parent navigation.
        public event EventHandler<NavigatingCancelEventArgs> Navigating;

        // Occurs when navigation to new content has completed.
        public event EventHandler<NavigationEventArgs> Navigated;

        // Occurs when navigation has failed.
        public event EventHandler<NavigationFailedEventArgs> NavigationFailed;
        public Object NavigatingParameter { get; set; }
        private Stack<Uri> History { get; set; }
        private Dictionary<Uri, Object> ContentCache { get; set; }

        // list of registered frames in sub tree
        private List<WeakReference<FantasyFrame>> ChildFrames { get; set; }
        private CancellationTokenSource TokenSource { get; set; }
        private Boolean IsNavigatingHistory { get; set; }
        private Boolean IsResetSource { get; set; }
    }
}
