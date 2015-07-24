using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Fantasy.Metro.Utils
{
    public static class FantasyUtility
    {
        public static Uri GetUri(String uriString, UriKind uriKind = UriKind.Relative)
        {
            return new Uri(uriString, uriKind);
        }

        public static ImageSource GetImageSource(String uri, UriKind uriKind = UriKind.Relative)
        {
            return new BitmapImage(new Uri(uri, uriKind));
        }

        public static Image GetImage(String uri, Style style, UriKind uriKind)
        {
            return new Image 
            {
                Source = GetImageSource(uri, uriKind),
                Style = style,
            };
        }

        public static Thickness MakeThickness(Double uniformLength)
        {
            return new Thickness(uniformLength);
        }
        public static Thickness MakeThickness(Double left, Double top, Double right, Double bottom)
        {
            return new Thickness(left, top, right, bottom);
        }

        // colorString: #AARRGGBB
        public static Brush GetColorBrush(String colorString)
        {
            BrushConverter brushConverter = new BrushConverter();
            return (Brush)brushConverter.ConvertFromString(colorString);
        }
        public static SolidColorBrush MakeSolidColor(Byte a, Byte r, Byte g, Byte b)
        {
            return new SolidColorBrush(Color.FromArgb(a, r, g, b));
        }

        public static Thickness ZeroThickness = new Thickness(0);
        public static Thickness OneThickness = new Thickness(1);
        public static SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);
    }
}
