using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyNavigationButton : Button
    {
        public FantasyNavigationButton()
        {
            this.DefaultStyleKey = typeof(FantasyNavigationButton);
            this.Click += OnClick;
        }

        private void OnClick(Object sender, RoutedEventArgs e)
        {
            if (this.NavigationUri != null)
            {
                NavigationManager.Navigate(this.NavigationUri);
            }
        }

        public Uri NavigationUri 
        {
            get { return (Uri)GetValue(NavigationUriProperty); }
            set { SetValue(NavigationUriProperty, value); }
        }

        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.Register("NavigationUri",
                typeof(Uri),
                typeof(FantasyNavigationButton),
                new PropertyMetadata(null));

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasyNavigationButton),
                new PropertyMetadata(String.Empty));

        public Style HeaderStyle
        {
            get { return (Style)GetValue(HeaderStyleProperty); }
            set { SetValue(HeaderStyleProperty, value); }
        }

        public static readonly DependencyProperty HeaderStyleProperty =
            DependencyProperty.Register("HeaderStyle",
                typeof(Style),
                typeof(FantasyNavigationButton),
                new PropertyMetadata(null));
    }
}
