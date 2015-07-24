using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Fantasy.Metro.Utils
{
    public static class FantasyStyle
    {
        public static class Foundations
        {
            public static Double ControlContentThemeFontSize 
            {
                get { return (Double)Resource["ControlContentThemeFontSize"]; }
            }

            public static SolidColorBrush ReadOnlyForegroundBrush 
            {
                get { return (SolidColorBrush)Resource["ReadOnlyForegroundBrush"]; }
            }
            public static SolidColorBrush EnabledForegroundBrush 
            {
                get { return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x00)); }
            }
            public static SolidColorBrush DisabledForegroundBrush 
            {
                get { return new SolidColorBrush(Color.FromArgb(0x66, 0x00, 0x00, 0x00)); }
            }

            public static FontFamily ContentControlThemeFontFamily
            {
                get { return (FontFamily)Resource["ContentControlThemeFontFamily"]; }
            }
            public static FontFamily SymbolThemeFontFamily
            {
                get { return (FontFamily)Resource["SymbolThemeFontFamily"]; }
            }
            public static FontFamily LightTextFontFamily
            {
                get { return (FontFamily)Resource["LightTextFontFamily"]; }
            }
            public static FontFamily SemiLightTextFontFamily
            {
                get { return (FontFamily)Resource["SemiLightTextFontFamily"]; }
            }
            
            static Foundations()
            {
                Resource = new ResourceDictionary
                {
                    Source = new Uri("/Fantasy.Metro;component/Themes/Assets/Foundations.xaml", UriKind.RelativeOrAbsolute)
                };
            }
            private static ResourceDictionary Resource { get; set; }
        }

        public static class Image 
        {
            public static Style FixedImage16 { get { return Resource["FixedImage16"] as Style; } }
            public static Style FixedImage24 { get { return Resource["FixedImage24"] as Style; } }
            public static Style FixedImage32 { get { return Resource["FixedImage32"] as Style; } }
            public static Style FixedImage48 { get { return Resource["FixedImage48"] as Style; } }
            public static Style FixedImage64 { get { return Resource["FixedImage64"] as Style; } }
            public static Style FixedImage96 { get { return Resource["FixedImage96"] as Style; } }
            static Image()
            {
                Resource = new ResourceDictionary
                {
                    Source = new Uri("/Fantasy.Metro;component/Themes/Assets/FantasyMetro.Generic.xaml", UriKind.RelativeOrAbsolute)
                };
            }
            private static ResourceDictionary Resource { get; set; }
        }

        public static class Generics 
        {
            public static Style HorizontalSeparator { get { return Resource["HorizontalSeparator"] as Style; } }
            public static Style VerticalSeparator { get { return Resource["VerticalSeparator"] as Style; } }

            static Generics()
            {
                Resource = new ResourceDictionary
                {
                    Source = new Uri("/Fantasy.Metro;component/Themes/Assets/FantasyMetro.Generic.xaml", UriKind.RelativeOrAbsolute)
                };
            }
            private static ResourceDictionary Resource { get; set; }
        }

        public static class TextBlock
        {
            public static Style BodyText { get { return Resource["BodyTextBlockStyle"] as Style; } }
            public static Style HeaderText { get { return Resource["HeaderTextBlockStyle"] as Style; } }
            public static Style SubheaderText { get { return Resource["SubheaderTextBlockStyle"] as Style; } }
            public static Style TitleText { get { return Resource["TitleTextBlockStyle"] as Style; } }
            public static Style CaptionText { get { return Resource["CaptionTextBlockStyle"] as Style; } }
            public static Style SubtitleText { get { return Resource["SubtitleTextBlockStyle"] as Style; } }
            public static Style Normal { get { return Resource["Normal"] as Style; } }
            public static Style SubTitle { get { return Resource["SubTitle"] as Style; } }
            public static Style ButtonText { get { return Resource["ButtonTextStyle"] as Style; } }
            public static Style Heading1 { get { return Resource["Heading1"] as Style; } }
            public static Style Heading1Light { get { return Resource["Heading1Light"] as Style; } }
            public static Style Heading2 { get { return Resource["Heading2"] as Style; } }
            public static Style Title { get { return Resource["Title"] as Style; } }
            public static Style TitleBold { get { return Resource["TitleBold"] as Style; } }
            public static Style Small { get { return Resource["Small"] as Style; } }
            public static Style Emphasis { get { return Resource["Emphasis"] as Style; } }
            public static Style Fixed { get { return Resource["Fixed"] as Style; } }

            private static ResourceDictionary Resource = new ResourceDictionary 
            {
                Source = new Uri("/Fantasy.Metro;component/Themes/Assets/TextBlock.xaml", UriKind.RelativeOrAbsolute)
            };
        }
    }
}
