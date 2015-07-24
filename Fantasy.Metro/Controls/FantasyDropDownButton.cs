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
    public class FantasyDropDownButton : Button
    {
        public FantasyDropDownButton()
        {
            this.DefaultStyleKey = typeof(FantasyDropDownButton);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            this.Click += (s, e) =>
            {
                if (this.DropDownMenu != null)
                {
                    this.DropDownMenu.IsOpen = true;
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

        public ContextMenu DropDownMenu
        {
            get { return (ContextMenu)GetValue(DropDownMenuProperty); }
            set { SetValue(DropDownMenuProperty, value); }
        }        

        public static readonly DependencyProperty ImageUriProperty =
            DependencyProperty.Register("ImageUri",
                typeof(Uri),
                typeof(FantasyDropDownButton),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ImageSizeProperty =
            DependencyProperty.Register("ImageSize",
                typeof(Double),
                typeof(FantasyDropDownButton),
                new PropertyMetadata(16d));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header",
                typeof(String),
                typeof(FantasyDropDownButton),
                new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty DropDownMenuProperty =
            DependencyProperty.Register("DropDownMenu",
                typeof(ContextMenu),
                typeof(FantasyDropDownButton));

        
    }
}
