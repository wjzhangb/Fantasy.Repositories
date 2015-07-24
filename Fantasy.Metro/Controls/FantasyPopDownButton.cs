using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Fantasy.Metro.Controls
{
    public class FantasyPopDownButton : Button
    {
        public FantasyPopDownButton()
        {
            this.DefaultStyleKey = typeof(FantasyPopDownButton);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Click += (s, e) =>
            {
                if (this.DropDownPopup != null)
                {
                    this.DropDownPopup.IsOpen = true;
                }
            };
        }

        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public Double ImageSize
        {
            get { return (Double)GetValue(ImageSizeProperty); }
            set { SetValue(ImageSizeProperty, value); }
        }

        public String Header
        {
            get { return (String)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public Popup DropDownPopup
        {
            get { return (Popup)GetValue(DropDownPopupProperty); }
            set { SetValue(DropDownPopupProperty, value); }
        }

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri",
                typeof(Uri),
                typeof(FantasyPopDownButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize",
                typeof(Double),
                typeof(FantasyPopDownButton),
                new PropertyMetadata(16d));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasyPopDownButton),
                new PropertyMetadata(String.Empty));
                
        public static readonly DependencyProperty DropDownPopupProperty =
            DependencyProperty.Register("DropDownPopup",
                typeof(Popup),
                typeof(FantasyPopDownButton));
    }
}
