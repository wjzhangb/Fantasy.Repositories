using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Fantasy.Metro.Converters;

namespace Fantasy.Metro.Controls
{
    public class FantasySwitch : ContentControl
    {
        public FantasySwitch()
        {
            this.DefaultStyleKey = typeof(FantasySwitch);
            this.PreviewKeyUp += (s, e) => 
            {
                if (e.Key == System.Windows.Input.Key.Space && 
                    e.OriginalSource == s)
                {
                    this.IsChecked = !this.IsChecked; 
                }
            };
        }

        public String OnLabel
        {
            get { return (String)GetValue(OnLabelProperty); }
            set { SetValue(OnLabelProperty, value); }
        }
        public static readonly DependencyProperty OnLabelProperty = 
            DependencyProperty.Register("OnLabel", 
                typeof(String),
                typeof(FantasySwitch),
                new PropertyMetadata("On"));

        public String OffLabel
        {
            get { return (String)GetValue(OffLabelProperty); }
            set { SetValue(OffLabelProperty, value); }
        }
        public static readonly DependencyProperty OffLabelProperty = 
            DependencyProperty.Register("OffLabel",
                typeof(String),
                typeof(FantasySwitch),
                new PropertyMetadata("Off"));

        public Object Header
        {
            get { return (Object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(Object),
                typeof(FantasySwitch),
                new PropertyMetadata(null));

        public Thickness HeaderMargin
        {
            get { return (Thickness)GetValue(HeaderMarginProperty); }
            set { SetValue(HeaderMarginProperty, value); }
        }
        public static readonly DependencyProperty HeaderMarginProperty =
            DependencyProperty.Register("HeaderMargin",
                typeof(Thickness),
                typeof(FantasySwitch));

        public Double HeaderWidth
        {
            get { return (Double)GetValue(HeaderWidthProperty); }
            set { SetValue(HeaderWidthProperty, value); }
        }
        public static readonly DependencyProperty HeaderWidthProperty =
            DependencyProperty.Register("HeaderWidth",
                typeof(Double),
                typeof(FantasySwitch));

        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }
        public static readonly DependencyProperty HeaderTemplateProperty =
            DependencyProperty.Register("HeaderTemplate",
                typeof(DataTemplate),
                typeof(FantasySwitch),
                new PropertyMetadata(null));

        public Orientation Orientation 
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation",
                typeof(Orientation),
                typeof(FantasySwitch),
                new PropertyMetadata(Orientation.Horizontal));

        public Brush SwitchForeground
        {
            get { return (Brush)GetValue(SwitchForegroundProperty); }
            set { SetValue(SwitchForegroundProperty, value); }
        }
        public static readonly DependencyProperty SwitchForegroundProperty =
            DependencyProperty.Register("SwitchForeground", 
                typeof(Brush),
                typeof(FantasySwitch), 
                null);

        [TypeConverter(typeof(NullableBoolConverter))]
        public Boolean? IsChecked
        {
            get { return (Boolean?)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked",
                typeof(Boolean?),
                typeof(FantasySwitch), 
                new FrameworkPropertyMetadata(false,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    OnIsCheckedChanged));

        public FlowDirection ContentDirection
        {
            get { return (FlowDirection)GetValue(ContentDirectionProperty); }
            set { SetValue(ContentDirectionProperty, value); }
        }
        // LeftToRight means content left and button right and RightToLeft vise versa
        public static readonly DependencyProperty ContentDirectionProperty = 
            DependencyProperty.Register("ContentDirection",
                typeof(FlowDirection),
                typeof(FantasySwitch), 
                new PropertyMetadata(FlowDirection.LeftToRight));

        public event EventHandler<RoutedEventArgs> Checked;
        public event EventHandler<RoutedEventArgs> Unchecked;
        public event EventHandler<RoutedEventArgs> Indeterminate;
        public event EventHandler<RoutedEventArgs> Click;
        public event EventHandler IsCheckedChanged;

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FantasySwitch fantasySwitch = (FantasySwitch)d;
            if (fantasySwitch.SwitchButton != null)
            {
                var oldValue = (Boolean?)e.OldValue;
                var newValue = (Boolean?)e.NewValue;

                if (oldValue != newValue && fantasySwitch.IsCheckedChanged != null)
                {
                    fantasySwitch.IsCheckedChanged(fantasySwitch, EventArgs.Empty);
                }
            }
        }

        private void SetDefaultContent()
        {
            Binding binding = new Binding("IsChecked") 
            {
                Source = this, Converter = new OffOnConverter(), 
                ConverterParameter = this 
            };

            SetBinding(ContentProperty, binding);
        }

        private void ChangeVisualState(Boolean useTransitions)
        {
            String stateName = this.IsEnabled ? "NormalState" : "DisabledState";
            VisualStateManager.GoToState(this, stateName, useTransitions);
        }

        protected override void OnContentChanged(Object oldContent, Object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
            this.WasContentSet = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (!this.WasContentSet && GetBindingExpression(ContentProperty) == null)
            {
                SetDefaultContent();
            }

            if (this.SwitchButton != null)
            {
                this.SwitchButton.Checked -= this.OnChecked;
                this.SwitchButton.Unchecked -= this.OnUnchecked;
                this.SwitchButton.Indeterminate -= this.OnIndeterminate;
                this.SwitchButton.Click -= this.OnClick;
                this.SwitchButton.IsEnabledChanged -= OnIsEnabled;
                BindingOperations.ClearBinding(this.SwitchButton, 
                    ToggleButton.IsCheckedProperty);
            }

            this.SwitchButton = GetTemplateChild("SwitchButton") as FantasySwitchButton;
            this.SwitchButton.Checked += OnChecked;
            this.SwitchButton.Unchecked += OnUnchecked;
            this.SwitchButton.Indeterminate += OnIndeterminate;
            this.SwitchButton.Click += OnClick;
            this.SwitchButton.IsEnabledChanged += OnIsEnabled;
            Binding binding = new Binding("IsChecked") { Source = this };
            this.SwitchButton.SetBinding(ToggleButton.IsCheckedProperty, binding);

            this.ChangeVisualState(false);
        }

        private void OnIsEnabled(Object sender, DependencyPropertyChangedEventArgs e)
        {
            this.ChangeVisualState(false);
        }

        private void OnChecked(Object sender, RoutedEventArgs e)
        {
            if (this.Checked != null)
            {
                this.Checked(this, e);
            }
        }

        private void OnUnchecked(Object sender, RoutedEventArgs e)
        {
            if (this.Unchecked != null)
            {
                this.Unchecked(this, e);
            }
        }

        private void OnIndeterminate(Object sender, RoutedEventArgs e)
        {
            if (this.Indeterminate != null)
            {
                this.Indeterminate(this, e);
            }
        }

        private void OnClick(Object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        private FantasySwitchButton SwitchButton { get; set; }
        private Boolean WasContentSet { get; set; }
    }
}
