using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fantasy.Metro.Controls
{
    public class FantasyDialog : Window
    {
        public FantasyDialog()
        {
            this.DefaultStyleKey = typeof(FantasyDialog);
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            this.closeCommand = new RelayCommand(o =>
            {
                var result = o as MessageBoxResult?;
                if (result.HasValue)
                {
                    this.dialogResult = result.Value;
                }

                Close();
            });

            this.Buttons = new Button[] { this.CloseButton };

            // set the default owner to the app main window (if possible)
            if (Application.Current != null && 
                Application.Current.MainWindow != this)
            {
                this.Owner = Application.Current.MainWindow;
            }
        }

        private ICommand closeCommand;
        private MessageBoxResult dialogResult = MessageBoxResult.None;
        public ICommand CloseCommand
        {
            get { return this.closeCommand; }
        }

        private Button CreateCloseDialogButton(string content, bool isDefault, bool isCancel, MessageBoxResult result)
        {
            return new Button
            {
                Content = content,
                Command = this.CloseCommand,
                CommandParameter = result,
                IsDefault = isDefault,
                IsCancel = isCancel,
                MinHeight = 21,
                MinWidth = 65,
                Margin = new Thickness(4, 0, 0, 0)
            };
        }

        private Button m_CloseButton = null;
        public Button CloseButton
        {
            get
            {
                if (this.m_CloseButton == null)
                {
                    CultureInfo ci = CultureInfo.CurrentUICulture;
                    String content = "Close";
                    if (ci.Name == "zh-cn")
                        content = "关闭";
                    this.m_CloseButton = CreateCloseDialogButton(content, true, 
                        false, MessageBoxResult.None);
                }

                return this.m_CloseButton;
            }
        }

        public static readonly DependencyProperty BackgroundContentProperty = 
            DependencyProperty.Register("BackgroundContent", 
                typeof(object), 
                typeof(FantasyDialog));        
        public object BackgroundContent
        {
            get { return GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        public static readonly DependencyProperty ButtonsProperty = 
            DependencyProperty.Register("Buttons", 
                typeof(IEnumerable<Button>),
                typeof(FantasyDialog));
        public IEnumerable<Button> Buttons
        {
            get { return (IEnumerable<Button>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }
    }
}
