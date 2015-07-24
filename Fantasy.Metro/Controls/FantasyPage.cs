using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Fantasy.Metro.Controls
{
    public class FantasyPage : Page
    {
        public FantasyPage()
        {
            this.DefaultStyleKey = typeof(FantasyPage);

            // listen for theme changes
            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
            this.Unloaded += (s, e) => 
            {
                AppearanceManager.Current.PropertyChanged -= OnAppearanceManagerPropertyChanged;
            };
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            // retrieve BackgroundAnimation storyboard
            Border border = GetTemplateChild("WindowBorder") as Border;
            if (border != null)
            {
                this.backgroundAnimation = border.Resources["BackgroundAnimation"] as Storyboard;

                if (this.backgroundAnimation != null)
                {
                    this.backgroundAnimation.Begin();
                }
            }

            this.ContentFrame = GetTemplateChild("ContentFrame") as FantasyFrame;
            NavigationManager.ContentFrame = this.ContentFrame;

            this.OverlayBox = GetTemplateChild("OverlayBox") as Grid;
            this.MetroDialogContainer = GetTemplateChild("MetroDialogContainer") as Grid;

            this.LogoButton = GetTemplateChild("LogoButton") as Button;
            this.LogoButton.Click += (s, e) =>
            {
                this.ConstructLogoContextMenu();
            };

            this.BrowseHomeButton = GetTemplateChild("BrowseHomeButton") as FantasyEllipseButton;
            this.BrowseHomeButton.Click += (s, e) =>
            {
                this.NavigateHome();
            };

            this.HelpButton = GetTemplateChild("HelpButton") as Button;
            this.HelpPopup = GetTemplateChild("HelpPopup") as Popup;
            this.HelpButton.Click += (s, e) => 
            {
                this.HelpPopup.IsOpen = true;
                StackPanel menu = this.HelpPopup.FindName("Menu") as StackPanel;
                menu.Children.Clear();
                this.ConstructHelpMenu(menu);
            };
        }

        protected virtual void ConstructHelpMenu(StackPanel menu)
        { 

        }

        protected virtual void ConstructLogoContextMenu()
        {
        }

        protected virtual void NavigateHome()
        {
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // start background animation if theme has changed
            if (e.PropertyName == "ThemeSource" && this.backgroundAnimation != null)
            {
                this.backgroundAnimation.Begin();
            }
        }

        public Boolean IsOverlayVisible()
        {
            return this.OverlayBox.Visibility == Visibility.Visible &&
                this.OverlayBox.Opacity >= 0.7;
        }
        public void ShowOverlay()
        {
            this.OverlayBox.Visibility = Visibility.Visible;
            this.OverlayBox.SetCurrentValue(Grid.OpacityProperty, 0.7);
        }
        public Task ShowOverlayAsync()
        {
            if (IsOverlayVisible() && OverlayStoryboard == null)
            {
                return new Task(() => { });
            }

            this.Dispatcher.VerifyAccess();
            this.OverlayBox.Visibility = Visibility.Visible;
            TaskCompletionSource<Object> tcs = new TaskCompletionSource<Object>();
            Storyboard sb = (Storyboard)this.Template.Resources["OverlayFastSemiFadeIn"];
            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (s, e) =>
            {
                sb.Completed -= completionHandler;

                if (this.OverlayStoryboard == sb)
                {
                    this.OverlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;
            this.OverlayBox.BeginStoryboard(sb);
            this.OverlayStoryboard = sb;
            return tcs.Task;
        }

        public void HideOverlay()
        {
            this.OverlayBox.SetCurrentValue(Grid.OpacityProperty, 0.0);
            this.OverlayBox.Visibility = System.Windows.Visibility.Hidden;
        }
        public Task HideOverlayAsync()
        {
            if (this.OverlayBox.Visibility == Visibility.Visible &&
                this.OverlayBox.Opacity == 0.0)
            {
                return new System.Threading.Tasks.Task(() => { });
            }

            Dispatcher.VerifyAccess();
            TaskCompletionSource<Object> tcs = new TaskCompletionSource<Object>();

            Storyboard sb = (Storyboard)this.Template.Resources["OverlayFastSemiFadeOut"];
            sb = sb.Clone();

            EventHandler completionHandler = null;
            completionHandler = (s, e) =>
            {
                sb.Completed -= completionHandler;

                if (this.OverlayStoryboard == sb)
                {
                    this.OverlayBox.Visibility = Visibility.Hidden;
                    this.OverlayStoryboard = null;
                }

                tcs.TrySetResult(null);
            };

            sb.Completed += completionHandler;
            this.OverlayBox.BeginStoryboard(sb);
            this.OverlayStoryboard = sb;
            return tcs.Task;
        }

        public Object BackgroundContent
        {
            get { return (Object)GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        public Boolean IsTitleVisible
        {
            get { return (Boolean)GetValue(IsTitleVisibleProperty); }
            set { SetValue(IsTitleVisibleProperty, value); }
        }

        public Geometry LogoData
        {
            get { return (Geometry)GetValue(LogoDataProperty); }
            set { SetValue(LogoDataProperty, value); }
        }

        public Uri ContentSource
        {
            get { return (Uri)GetValue(ContentSourceProperty); }
            set { SetValue(ContentSourceProperty, value); }
        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public String ContentHeader
        {
            get { return (String)GetValue(ContentHeaderProperty); }
            set { SetValue(ContentHeaderProperty, value); }
        }

        public String CurrentUser
        {
            get { return (String)GetValue(CurrentUserProperty); }
            set { SetValue(CurrentUserProperty, value); }
        }

        public static readonly DependencyProperty BackgroundContentProperty =
            DependencyProperty.Register("BackgroundContent",
                typeof(Object),
                typeof(FantasyPage));

        public static readonly DependencyProperty IsTitleVisibleProperty =
            DependencyProperty.Register("IsTitleVisible",
                typeof(Boolean),
                typeof(FantasyPage),
                new PropertyMetadata(true));

        public static readonly DependencyProperty LogoDataProperty =
            DependencyProperty.Register("LogoData",
                typeof(Geometry),
                typeof(FantasyPage),
                new PropertyMetadata(Geometry.Parse("F1 M 27.5314,21.8628L 33.0126,19.4224L 34.7616,23.3507C 36.6693,22.9269 38.6044,22.8903 40.4668,23.2026L 42.0083,19.1868L 47.6098,21.337L 46.0683,25.3528C 47.6612,26.3669 49.0747,27.6889 50.2088,29.2803L 54.1371,27.5313L 56.5776,33.0126L 52.6493,34.7616C 53.0731,36.6693 53.1097,38.6043 52.7974,40.4668L 56.8131,42.0083L 54.6629,47.6097L 50.6472,46.0683C 49.6331,47.6613 48.3111,49.0748 46.7197,50.2089L 48.4686,54.1372L 42.9874,56.5776L 41.2384,52.6493C 39.3307,53.0731 37.3957,53.1097 35.5333,52.7974L 33.9918,56.8131L 28.3903,54.6629L 29.9318,50.6472C 28.3388,49.6331 26.9252,48.3111 25.7911,46.7196L 21.8628,48.4686L 19.4224,42.9873L 23.3507,41.2383C 22.9269,39.3307 22.8903,37.3957 23.2026,35.5332L 19.1869,33.9918L 21.3371,28.3903L 25.3528,29.9318C 26.3669,28.3388 27.6889,26.9252 29.2804,25.7911L 27.5314,21.8628 Z M 34.3394,29.7781C 29.7985,31.7998 27.7564,37.1198 29.7781,41.6606C 31.7998,46.2015 37.1198,48.2436 41.6606,46.2219C 46.2015,44.2002 48.2436,38.8802 46.2219,34.3394C 44.2002,29.7985 38.8802,27.7564 34.3394,29.7781 Z ")));

        public static readonly DependencyProperty ContentSourceProperty =
            DependencyProperty.Register("ContentSource",
                typeof(Uri),
                typeof(FantasyPage));

        public static readonly DependencyProperty ContentLoaderProperty =
            DependencyProperty.Register("ContentLoader",
                typeof(IContentLoader),
                typeof(FantasyPage),
                new PropertyMetadata(new DefaultContentLoader()));

        public static readonly DependencyProperty ContentHeaderProperty =
            DependencyProperty.Register("ContentHeader",
                typeof(String),
                typeof(FantasyPage),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty CurrentUserProperty =
            DependencyProperty.Register("CurrentUser",
                typeof(String),
                typeof(FantasyPage),
                new PropertyMetadata(String.Empty));

        private Storyboard backgroundAnimation;
        public FantasyFrame ContentFrame { get; private set; }
        public Grid OverlayBox { get; set; }
        private Button LogoButton { get; set; }
        private Button HelpButton { get; set; }
        private Popup HelpPopup { get; set; }
        private FantasyEllipseButton BrowseHomeButton { get; set; }
        private Storyboard OverlayStoryboard { get; set; }
        public Grid MetroDialogContainer { get; set; }
    }
}
