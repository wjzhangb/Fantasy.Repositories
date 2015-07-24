using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyPagingControl : Control
    {
        public FantasyPagingControl()
        { 
            this.DefaultStyleKey = typeof(FantasyPagingControl);
            SetCurrentValue(PageSizesProperty, new List<int> { 10, 20, 50, 100 });
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemsSourceProperty = 
            DependencyProperty.Register("ItemsSource",
                typeof(IEnumerable), 
                typeof(FantasyPagingControl), 
                new PropertyMetadata(null));

        public int Page 
        {
            get { return (int)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }
        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", 
            typeof(int), 
            typeof(FantasyPagingControl));

        public int TotalPages
        {
            get { return (int)GetValue(TotalPagesProperty); }
            set { SetValue(TotalPagesProperty, value); }
        }
        public static readonly DependencyProperty TotalPagesProperty = 
            DependencyProperty.Register("TotalPages", 
                typeof(int),
                typeof(FantasyPagingControl));

        public IEnumerable PageSizes
        {
            get { return (IEnumerable)GetValue(PageSizesProperty); }
            set { SetValue(PageSizesProperty, value); }
        }
        public static readonly DependencyProperty PageSizesProperty =
            DependencyProperty.Register("PageSizes", 
                typeof(IEnumerable),
                typeof(FantasyPagingControl));

        public IFantasyPagingContract PagingContract
        {
            get { return (IFantasyPagingContract)GetValue(PagingContractProperty); }
            set { SetValue(PagingContractProperty, value); }
        }
        public static readonly DependencyProperty PagingContractProperty =
            DependencyProperty.Register("PagingContract",
                typeof(IFantasyPagingContract),
                typeof(FantasyPagingControl),
                new PropertyMetadata(null));

        public Object FilterTag
        {
            get { return (Object)GetValue(FilterTagProperty); }
            set { SetValue(FilterTagProperty, value); }
        }
        public static readonly DependencyProperty FilterTagProperty =
            DependencyProperty.Register("FilterTag",
                typeof(Object),
                typeof(FantasyPagingControl),
                new PropertyMetadata(null));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();


        }

        private FantasyEllipseButton FirstPageButton { get; set; }
        private FantasyEllipseButton LastPageButton { get; set; }
        private FantasyEllipseButton PreviousPageButton { get; set; }
        private FantasyEllipseButton NextPageButton { get; set; }
        private ComboBox PageSizeComboBox { get; set; }
    }
}
