using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fantasy.Metro.Controls
{
    public class FantasyTile : UserControl
    {
        public FantasyTile()
        {
            this.DefaultStyleKey = typeof(FantasyTile);
            this.Click += OnClick;
            //this.Width = 220d;
            //this.Height = 220d;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border border = GetTemplateChild("TileBorder") as Border;
            if (border != null)
            {
                border.MouseEnter += (s, e) =>
                {
                    VisualStateManager.GoToState(this, "MouseEnter", true);
                };

                border.MouseLeave += (s, e) =>
                {
                    VisualStateManager.GoToState(this, "MouseLeave", true);
                };

                border.MouseLeftButtonDown += (s, e) => 
                {
                    FantasyTileEventArgs<FantasyTile> args = new FantasyTileEventArgs<FantasyTile>(this);
                    args.Title = this.Title;
                    args.ImageUri = this.ImageUri;
                    args.NavigationUri = this.NavigationUri;

                    if (Click != null)
                    {
                        Click(this, args);
                    }

                    if (OnNavigated != null)
                    {
                        OnNavigated(this, args);
                    }
                };
            }
        }

        public event FantasyTileClickEventHandler Click;
        public event EventHandler OnNavigated;

        private void OnClick(Object sender, FantasyTileEventArgs<FantasyTile> e)
        {
            if (this.NavigationUri != null)
            {
                NavigationManager.Navigate(this.NavigationUri);                
            }
        }

        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public Double TitleFontSize
        {
            get { return (Double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public Double TileSize
        {
            get { return (Double)GetValue(TileSizeProperty); }
            set { SetValue(TileSizeProperty, value); }
        }


        public Uri NavigationUri 
        {
            get { return (Uri)GetValue(NavigationUriProperty); }
            set { SetValue(NavigationUriProperty, value); }
        }

        public Thickness TitlePadding
        {
            get { return (Thickness)GetValue(TitlePaddingProperty); }
            set { SetValue(TitlePaddingProperty, value); }
        }

        public static readonly DependencyProperty TitleFontSizeProperty =
            DependencyProperty.Register("TitleFontSize",
                typeof(Double),
                typeof(FantasyTile));

        public static readonly DependencyProperty TileSizeProperty =
            DependencyProperty.Register("TileSize",
                typeof(Double),
                typeof(FantasyTile),
                new PropertyMetadata(220d));

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri",
                typeof(Uri),
                typeof(FantasyTile), 
                new PropertyMetadata(null));

        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.Register("NavigationUri",
                typeof(Uri),
                typeof(FantasyTile),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
                typeof(String),
                typeof(FantasyTile),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty TitlePaddingProperty =
            DependencyProperty.Register("TitlePadding",
                typeof(Thickness),
                typeof(FantasyTile),
                new PropertyMetadata(new Thickness(20)));
    }
}
