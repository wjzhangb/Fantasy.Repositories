using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fantasy.Metro.Controls
{
    [System.Windows.Markup.ContentProperty("DialogBody")]
    public class FantasyBaseDialog : Control
    {
        public FantasyBaseDialog()
        {
            this.Owner = null;
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Fantasy.Metro;component/Themes/FantasyBaseDialog.xaml")
            });
        }

        protected FantasyBaseDialog(FantasyWindow owner)
        {
            this.Owner = owner;
            this.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Fantasy.Metro;component/Themes/FantasyBaseDialog.xaml")
            });
        }

        public virtual void OnShown() 
        {
        }

        public virtual void OnClose()
        {            
        }

        static FantasyBaseDialog()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FantasyBaseDialog),
                new FrameworkPropertyMetadata(typeof(FantasyBaseDialog)));
        }

        public override void OnApplyTemplate()
        {
            this.DialogBodyContentPresenter = GetTemplateChild("DialogBodyContentPresenter") as ContentPresenter;
            base.OnApplyTemplate();
        }

        public Task WaitForCloseAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Storyboard closingStoryboard = this.Resources["DialogCloseStoryboard"] as Storyboard;
            EventHandler handler = null;
            handler = new EventHandler((sender, args) =>
            {
                closingStoryboard.Completed -= handler;
                tcs.TrySetResult(null);
            });

            closingStoryboard = closingStoryboard.Clone();
            closingStoryboard.Completed += handler;
            closingStoryboard.Begin(this);

            return tcs.Task;
        }

        public Task WaitForLoadAsync()
        {
            Dispatcher.VerifyAccess();
            if (this.IsLoaded)
            {
                return new Task(() => { });
            }

            //if (!DialogSettings.AnimateShow)
            //    this.Opacity = 1.0; //skip the animation

            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();

            RoutedEventHandler handler = null;
            handler = new RoutedEventHandler((sender, args) =>
            {
                this.Loaded -= handler;
                tcs.TrySetResult(null);
            });

            this.Loaded += handler;
            return tcs.Task;
        }

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = 
            DependencyProperty.Register("Title",
                typeof(String), typeof(FantasyBaseDialog),
                new PropertyMetadata(default(String)));

        public Object DialogBody
        {
            get { return (Object)GetValue(DialogBodyProperty); }
            set { SetValue(DialogBodyProperty, value); }
        }
        public static readonly DependencyProperty DialogBodyProperty =
            DependencyProperty.Register("DialogBody",
                typeof(Object),
                typeof(FantasyBaseDialog),
                new PropertyMetadata(null, (o, e) => 
                {
                    FantasyBaseDialog dialog = (o as FantasyBaseDialog);
                    if (dialog != null)
                    {
                        if (e.OldValue != null)
                        {
                            dialog.RemoveLogicalChild(e.OldValue);
                        }
                        if (e.NewValue != null)
                        {
                            dialog.AddLogicalChild(e.NewValue);
                        }
                    }
                }));

        public Object DialogTop
        {
            get { return (Object)GetValue(DialogTopProperty); }
            set { SetValue(DialogTopProperty, value); }
        }
        public static readonly DependencyProperty DialogTopProperty =
            DependencyProperty.Register("DialogTop", 
                typeof(Object),
                typeof(FantasyBaseDialog),
                new PropertyMetadata(null));

        public Object DialogBottom
        {
            get { return (Object)GetValue(DialogBottomProperty); }
            set { SetValue(DialogBottomProperty, value); }
        }
        public static readonly DependencyProperty DialogBottomProperty = 
            DependencyProperty.Register("DialogBottom",
                typeof(Object), 
                typeof(FantasyBaseDialog), 
                new PropertyMetadata(null));

        protected ContentPresenter DialogBodyContentPresenter = null;
        protected FantasyWindow Owner { get; set; }
        public SizeChangedEventHandler SizeChangedHandler { get; set; }
        
    }
}
