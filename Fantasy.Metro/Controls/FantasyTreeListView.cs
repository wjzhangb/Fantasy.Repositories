using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Metro.Controls
{
    public class FantasyTreeListView : TreeView
    {
        public FantasyTreeListView()
        {
            this.DefaultStyleKey = typeof(FantasyTreeListView);
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FantasyTreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FantasyTreeListViewItem;
        }

        public GridViewColumnCollection Columns
        {
            get
            {
                if (_columns == null)
                {
                    _columns = new GridViewColumnCollection();
                }

                return _columns;
            }
        }

        //public GridViewColumnCollection Columns
        //{
        //    get { return (GridViewColumnCollection)GetValue(ColumnsProperty); }
        //    set { SetValue(ColumnsProperty, value); }
        //}
        //public static readonly DependencyProperty ColumnsProperty =
        //    DependencyProperty.Register("Columns",
        //        typeof(GridViewColumnCollection), typeof(FantasyTreeListView),
        //        new PropertyMetadata(new GridViewColumnCollection()));

        private GridViewColumnCollection _columns;
    }

    public class FantasyTreeListViewItem : TreeViewItem
    {
        public int Level
        {
            get
            {
                if (m_Level == -1)
                {
                    FantasyTreeListViewItem parent = ItemsControl.ItemsControlFromItemContainer(this) as FantasyTreeListViewItem;
                    m_Level = (parent != null) ? parent.Level + 1 : 0;
                }
                return m_Level;
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FantasyTreeListViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is FantasyTreeListViewItem;
        }

        private int m_Level = -1;
    }
}
