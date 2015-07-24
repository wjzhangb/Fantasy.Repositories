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
    /// Interaction logic for FantasyMessageDialog.xaml
    /// </summary>
    public partial class FantasyMessageDialog : FantasyBaseDialog
    {
        public FantasyMessageDialog(FantasyWindow owner)
            : base(owner)
        {
            InitializeComponent();
        }

        public Task<Object> WaitForButtonPressAsync()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Focus();
            }));

            TaskCompletionSource<Object> tcs = new TaskCompletionSource<Object>();
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
                tcs.TrySetResult("Close");
                e.Handled = true;
            });

            this.CloseButton.Click += closeHandler;
            this.KeyDown += escapeKeyHandler;
            return tcs.Task;
        }

        public String Message
        {
            get { return (String)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message",
                typeof(String),
                typeof(FantasyMessageDialog),
                new PropertyMetadata(default(String)));
    }
}
