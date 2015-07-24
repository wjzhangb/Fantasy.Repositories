using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyTab : Control
    {
        public FantasyTab()
        {
            this.DefaultStyleKey = typeof(FantasyTab);
            SetCurrentValue(LinksProperty, new LinkCollection());

        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public static readonly DependencyProperty ContentLoaderProperty = 
            DependencyProperty.Register("ContentLoader",
                typeof(IContentLoader), 
                typeof(FantasyTab), 
                new PropertyMetadata(new DefaultContentLoader()));

        public FantasyTabLayout Layout
        {
            get { return (FantasyTabLayout)GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }        

        public static readonly DependencyProperty LayoutProperty = 
            DependencyProperty.Register("Layout",
                typeof(FantasyTabLayout),
                typeof(FantasyTab), 
                new PropertyMetadata(FantasyTabLayout.Tab));

        public HorizontalAlignment LinksHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(LinksHorizontalAlignmentProperty); }
            set { SetValue(LinksHorizontalAlignmentProperty, value); }
        }

        public static readonly DependencyProperty LinksHorizontalAlignmentProperty =
            DependencyProperty.Register("LinksHorizontalAlignment",
                typeof(HorizontalAlignment),
                typeof(FantasyTab),
                new PropertyMetadata(HorizontalAlignment.Left));

        public Thickness LinksMargin
        {
            get { return (Thickness)GetValue(LinksMarginProperty); }
            set { SetValue(LinksMarginProperty, value); }
        }

        public static readonly DependencyProperty LinksMarginProperty =
            DependencyProperty.Register("LinksMargin",
                typeof(Thickness),
                typeof(FantasyTab),
                new PropertyMetadata(new Thickness(0, -28, 44, 0)));

        public Thickness ContentMargin
        {
            get { return (Thickness)GetValue(ContentMarginProperty); }
            set { SetValue(ContentMarginProperty, value); }
        }

        public static readonly DependencyProperty ContentMarginProperty =
            DependencyProperty.Register("ContentMargin",
                typeof(Thickness),
                typeof(FantasyTab),
                new PropertyMetadata(new Thickness(30, 0, 0, 0)));

        public Visibility SeperatorVisibility 
        {
            get { return (Visibility)GetValue(SeperatorVisibilityProperty); }
            set { SetValue(SeperatorVisibilityProperty, value); }
        }

        public static readonly DependencyProperty SeperatorVisibilityProperty =
            DependencyProperty.Register("SeperatorVisibility",
                typeof(Visibility),
                typeof(FantasyTab),
                new PropertyMetadata(Visibility.Visible));

        public Double LinksWidth
        {
            get { return (Double)GetValue(LinksWidthProperty); }
            set { SetValue(LinksWidthProperty, value); }
        }

        public static readonly DependencyProperty LinksWidthProperty =
            DependencyProperty.Register("LinksWidth",
                typeof(Double),
                typeof(FantasyTab),
                new PropertyMetadata(Constants.DefaultLinksWidth));

        public LinkCollection Links
        {
            get { return (LinkCollection)GetValue(LinksProperty); }
            set { SetValue(LinksProperty, value); }
        }

        public static readonly DependencyProperty LinksProperty =
            DependencyProperty.Register("Links",
                typeof(LinkCollection),
                typeof(FantasyTab),
                new PropertyMetadata(FantasyTab.OnLinksChanged));

        public Uri SelectedSource
        {
            get { return (Uri)GetValue(SelectedSourceProperty); }
            set { SetValue(SelectedSourceProperty, value); }
        }

        public static readonly DependencyProperty SelectedSourceProperty =
            DependencyProperty.Register(
                "SelectedSource",
                typeof(Uri),
                typeof(FantasyTab),
                new PropertyMetadata(FantasyTab.OnSelectedSourceChanged));

        public event EventHandler<SourceEventArgs> SelectedSourceChanged;

        private static void OnLinksChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FantasyTab tab = sender as FantasyTab;
            tab.UpdateSelection();
        }

        private static void OnSelectedSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            FantasyTab tab = sender as FantasyTab;
            Uri from = e.OldValue as Uri;
            Uri to = e.NewValue as Uri;
            tab.OnSelectedSourceChanged(from, to);
        }

        private void OnSelectedSourceChanged(Uri from, Uri to)
        {
            UpdateSelection();
            if (this.SelectedSourceChanged != null) 
            {
                this.SelectedSourceChanged(this, new SourceEventArgs(from, to));
            }
        }

        private void UpdateSelection()
        {
            if (this.LinkList == null || this.Links == null)
            {
                return;
            }

            // sync list selection with current source
            this.LinkList.SelectedItem = this.Links.FirstOrDefault(
                l => l.Source == this.SelectedSource);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.LinkList != null)
            {
                this.LinkList.SelectionChanged -= OnLinkListSelectionChanged;
            }

            this.LinkList = GetTemplateChild("LinkList") as ListBox;
            if (this.LinkList != null)
            {
                this.LinkList.SelectionChanged += OnLinkListSelectionChanged;
            }

            UpdateSelection();
        }

        private void OnLinkListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Link link = this.LinkList.SelectedItem as Link;
            if (link != null && link.Source != this.SelectedSource)
            {
                SetCurrentValue(SelectedSourceProperty, link.Source);
            }
        }

        private ListBox LinkList { get; set; }
    }
}
