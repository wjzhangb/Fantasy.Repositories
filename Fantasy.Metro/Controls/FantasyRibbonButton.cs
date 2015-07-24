using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyRibbonButton : Button
    {
        public FantasyRibbonButton()
        {
            this.DefaultStyleKey = typeof(FantasyRibbonButton);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.IsNavigationButton)
            {
                this.Click += (s, e) =>
                {
                    if (this.NavigationUri != null)
                    {
                        NavigationManager.Navigate(this.NavigationUri);
                    }
                };
            }
        }

        public Uri NavigationUri
        {
            get { return (Uri)GetValue(NavigationUriProperty); }
            set { SetValue(NavigationUriProperty, value); }
        }

        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public Boolean IsNavigationButton 
        {
            get { return (Boolean)GetValue(IsNavigationButtonProperty); }
            set { SetValue(IsNavigationButtonProperty, value); }
        }

        public static readonly DependencyProperty IsNavigationButtonProperty =
            DependencyProperty.Register("IsNavigationButton",
                typeof(Boolean),
                typeof(FantasyRibbonButton),
                new PropertyMetadata(false));

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri",
                typeof(Uri),
                typeof(FantasyRibbonButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.Register("NavigationUri",
                typeof(Uri),
                typeof(FantasyRibbonButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasyRibbonButton),
                new PropertyMetadata(String.Empty));
    }
}
