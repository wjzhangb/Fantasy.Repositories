using System;
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
    /// Interaction logic for FantasyInput.xaml
    /// </summary>
    public partial class FantasyInput : UserControl
    {
        public FantasyInput()
        {
            InitializeComponent();
        }

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public Double InputWidth
        {
            get { return (Double)GetValue(InputWidthProperty); }
            set { SetValue(InputWidthProperty, value); }
        }

        public Double InputHeight
        {
            get { return (Double)GetValue(InputHeightProperty); }
            set { SetValue(InputHeightProperty, value); }
        }

        public Double HeaderWidth
        {
            get { return (Double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public Boolean IsReadOnly
        {
            get { return (Boolean)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation",
                typeof(Orientation),
                typeof(FantasyInput),
                new PropertyMetadata(Orientation.Horizontal));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasyInput),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth",
                typeof(Double),
                typeof(FantasyInput),
                new PropertyMetadata(Constants.DefaultHeaderWidth));

        public static readonly DependencyProperty InputWidthProperty =
            DependencyProperty.Register("InputWidth",
                typeof(Double),
                typeof(FantasyInput),
                new PropertyMetadata(Constants.DefaultInputWidth));

        public static readonly DependencyProperty InputHeightProperty =
            DependencyProperty.Register("InputHeight",
                typeof(Double),
                typeof(FantasyInput));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text",
                typeof(String),
                typeof(FantasyInput),
                new FrameworkPropertyMetadata(String.Empty, 
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly",
                typeof(Boolean),
                typeof(FantasyInput),
                new PropertyMetadata(false));
    }
}
