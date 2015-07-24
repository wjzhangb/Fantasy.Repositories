using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fantasy.Metro.Controls
{
    /// <summary>
    /// Interaction logic for FantasySelectInput.xaml
    /// </summary>
    public partial class FantasySelectInput : UserControl
    {
        public FantasySelectInput()
        {
            InitializeComponent();
            this.Combo.SelectionChanged += (s, e) => 
            {
                if (this.SelectionChanged != null)
                {
                    this.SelectionChanged(s, e);
                }
            };
        }

        public event SelectionChangedEventHandler SelectionChanged;

        public Boolean IsReadOnly
        {
            get { return (Boolean)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public Boolean IsEditable
        {
            get { return (Boolean)GetValue(IsEditableProperty); }
            set { SetValue(IsEditableProperty, value); }
        }

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Double InputWidth
        {
            get { return (Double)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public Double HeaderWidth
        {
            get { return (Double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public Object SelectedItem
        {
            get { return (Object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public String DisplayMemberPath
        {
            get { return (String)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation",
                typeof(Orientation),
                typeof(FantasySelectInput),
                new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasySelectInput),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth",
                typeof(Double),
                typeof(FantasySelectInput),
                new PropertyMetadata(Constants.DefaultHeaderWidth));

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register("InputWidth",
                typeof(Double),
                typeof(FantasySelectInput),
                new PropertyMetadata(Constants.DefaultInputWidth));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
                typeof(String),
                typeof(FantasySelectInput),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly",
                typeof(Boolean),
                typeof(FantasySelectInput),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsEditableProperty =
            DependencyProperty.Register("IsEditable",
                typeof(Boolean),
                typeof(FantasySelectInput),
                new PropertyMetadata(false));

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                typeof(IEnumerable),
                typeof(FantasySelectInput),
                new FrameworkPropertyMetadata(null, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem",
                typeof(Object),
                typeof(FantasySelectInput),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath",
                typeof(String),
                typeof(FantasySelectInput),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register("ItemTemplate",
                typeof(DataTemplate),
                typeof(FantasySelectInput));
    }
}
