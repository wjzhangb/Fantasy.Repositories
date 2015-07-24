using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Fantasy.Metro.Controls
{
    //
    // A Button that allows the user to toggle between two states.
    //
    public class FantasySwitchButton : ToggleButton
    {
        public FantasySwitchButton()
        {
            this.DefaultStyleKey = typeof(FantasySwitchButton);
            this.IsDragging = false;
        }

        public override void OnApplyTemplate()
        {
            if (this.SwitchThumb != null)
            {
                this.SwitchThumb.SizeChanged -= this.OnSizeChanged;
            }

            if (this.SwitchTrack != null)
            {
                this.SwitchTrack.SizeChanged -= this.OnSizeChanged;
            }

            base.OnApplyTemplate();

            this.SwitchRoot = GetTemplateChild("SwitchRoot") as Grid;
            this.SwitchTrack = GetTemplateChild("SwitchTrack") as Grid;
            this.SwitchBackground = GetTemplateChild("SwitchBackground") as Rectangle;
            this.SwitchThumb = GetTemplateChild("SwitchThumb") as Border;

            this.BackgroundTranslation = this.SwitchBackground.RenderTransform as TranslateTransform;
            this.ThumbTranslation = this.SwitchThumb.RenderTransform as TranslateTransform;

            this.SwitchTrack.SizeChanged += OnSizeChanged;
            this.SwitchThumb.SizeChanged += OnSizeChanged;

            this.ChangeVisualState(false);
        }

        protected override void OnToggle()
        {
            IsChecked = (IsChecked != true);
            this.ChangeVisualState(true);
        }

        public Brush SwitchForeground 
        {
            get { return (Brush)GetValue(SwitchForegroundProperty); }
            set { SetValue(SwitchForegroundProperty, value); }
        }

        public static readonly DependencyProperty SwitchForegroundProperty = 
            DependencyProperty.Register("SwitchForeground",
            typeof(Brush),
            typeof(FantasySwitchButton), 
            new PropertyMetadata(null));

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SwitchTrack.Clip = new RectangleGeometry
            {
                Rect = new Rect(0, 0, this.SwitchTrack.ActualWidth, this.SwitchTrack.ActualHeight) 
            };

            // This value is being assigned on each callback but not used anywhere
            Double checkedTranslation = this.SwitchTrack.ActualWidth -
                this.SwitchThumb.ActualWidth - 
                this.SwitchThumb.Margin.Left - 
                this.SwitchThumb.Margin.Right;
        }

        private void ChangeVisualState(Boolean useTransitions)
        {
            String stateName = (this.IsEnabled) ? "NormalState" : "DisabledState";
            VisualStateManager.GoToState(this, stateName, useTransitions);

            if (this.IsDragging)
            {
                // IsDragging is never set to true, so we never enter this state
                VisualStateManager.GoToState(this, "DraggingState", useTransitions);
            }
            else if (this.IsChecked == true)
            {
                VisualStateManager.GoToState(this, "CheckedState", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "UncheckedState", useTransitions);
            }
        }

        private TranslateTransform BackgroundTranslation { get; set; }
        private TranslateTransform ThumbTranslation { get; set; }
        private Grid SwitchRoot { get; set; }
        private Grid SwitchTrack { get; set; }
        private Rectangle SwitchBackground { get; set; }
        private Border SwitchThumb { get; set; }
        private bool IsDragging { get; set; }
    }
}
