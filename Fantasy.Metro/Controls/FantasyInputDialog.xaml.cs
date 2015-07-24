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
    /// Interaction logic for FantasyInputDialog.xaml
    /// </summary>
    public partial class FantasyInputDialog : FantasyBaseDialog
    {
        public FantasyInputDialog(FantasyWindow owner)
            : base(owner)
        {
            InitializeComponent();
        }

        public Task<String> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Focus();
                this.InputTextBox.Focus();
            }));

            TaskCompletionSource<String> tcs = new TaskCompletionSource<String>();
            RoutedEventHandler closeHandler = null;
            KeyEventHandler escapeKeyHandler = null;

            Action CleanUpHandlers = () =>
            {
                this.CloseButton.Click -= closeHandler;
                this.KeyDown -= escapeKeyHandler;
            };

            escapeKeyHandler = new KeyEventHandler((sender, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    CleanUpHandlers();
                    tcs.TrySetResult(null);
                }
            });

            closeHandler = new RoutedEventHandler((s, e) =>
            {
                CleanUpHandlers();
                tcs.TrySetResult(this.Input);
                e.Handled = true;
            });

            this.KeyDown += escapeKeyHandler;
            this.CloseButton.Click += closeHandler;
            return tcs.Task;
        }

        public String Input
        {
            get { return (String)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }
        public static readonly DependencyProperty InputProperty = 
            DependencyProperty.Register(
                "Input",
                typeof(String),
                typeof(FantasyInputDialog),
                new PropertyMetadata(default(String)));
        
    }
}
