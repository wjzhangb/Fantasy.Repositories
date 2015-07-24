using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyTileItem : UserControl
    {
        public FantasyTileItem()
        {
            this.DefaultStyleKey = typeof(FantasyTileItem);
            this.Click += OnClick;
        }

        public event FantasyTileItemClickEventHandler Click;

        private void OnClick(Object sender, FantasyTileItemEventArgs<FantasyTileItem> e)
        {
            if (this.NavigationUri != null)
            {
                NavigationManager.Navigate(this.NavigationUri);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Border border = GetTemplateChild("TileItemBorder") as Border;
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
                    FantasyTileItemEventArgs<FantasyTileItem> args = new FantasyTileItemEventArgs<FantasyTileItem>(this);
                    args.Title = this.Title;
                    args.Description = this.Description;
                    args.ImageUri = this.ImageUri;
                    args.NavigationUri = this.NavigationUri;

                    if (Click != null)
                    {
                        Click(this, args);
                    }
                };
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

        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public Uri NavigationUri
        {
            get { return (Uri)GetValue(NavigationUriProperty); }
            set { SetValue(NavigationUriProperty, value); }
        }

        public Double ItemWidth
        {
            get { return (Double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }

        public Double ItemHeight
        {
            get { return (Double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri",
                typeof(Uri),
                typeof(FantasyTileItem),
                new PropertyMetadata(null));

        public static readonly DependencyProperty NavigationUriProperty =
            DependencyProperty.Register("NavigationUri",
                typeof(Uri),
                typeof(FantasyTileItem),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title",
                typeof(String),
                typeof(FantasyTileItem),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description",
                typeof(String),
                typeof(FantasyTileItem),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth",
                typeof(Double),
                typeof(FantasyTileItem),
                new PropertyMetadata(480d));

        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight",
                typeof(Double),
                typeof(FantasyTileItem),
                new PropertyMetadata(110d));
    }
}
