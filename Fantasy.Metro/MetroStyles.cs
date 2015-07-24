using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Fantasy.Metro
{
    public static class MetroStyles
    {
        internal static ResourceDictionary GetResource(String uri)
        {
            ResourceDictionary rd = new ResourceDictionary 
            {
                Source = new Uri(uri, UriKind.Relative)
            };

            return rd;
        }

        public static class Text
        {
            public static Style Header 
            {
                get { return Text.Resources["HeaderTextBlockStyle"] as Style; }
            }

            public static Style SubHeader
            {
                get { return Text.Resources["SubheaderTextBlockStyle"] as Style; }
            }

            public static Style Caption
            {
                get { return Text.Resources["CaptionTextBlockStyle"] as Style; }
            }

            public static Style SubTitle
            {
                get { return Text.Resources["SubTitle"] as Style; }
            }

            public static Style Title
            {
                get { return Text.Resources["Title"] as Style; }
            }

            public static Style Heading1
            {
                get { return Text.Resources["Heading1"] as Style; }
            }

            public static Style Heading2
            {
                get { return Text.Resources["Heading2"] as Style; }
            }

            public static Style Small
            {
                get { return Text.Resources["Small"] as Style; }
            }

            public static Style Emphasis
            {
                get { return Text.Resources["Emphasis"] as Style; }
            }

            public static Style Fixed
            {
                get { return Text.Resources["Fixed"] as Style; }
            }
            
            public static Style Body
            {
                get { return Text.Resources["BodyTextBlockStyle"] as Style; }
            }

            public static Style ButtonText
            {
                get { return Text.Resources["ButtonTextStyle"] as Style; }
            }

            public static Style Normal
            {
                get { return Text.Resources["Normal"] as Style; }
            }

            public static Style ToolBarButtonText
            {
                get { return Text.Resources["ToolBarButton"] as Style; }
            }

            static Text()
            {
                Text.Resources = MetroStyles.GetResource(ResourceUri);
            }

            private const String ResourceUri = "/Fantasy.Metro;component/Themes/Assets/TextBlock.xaml";
            private static ResourceDictionary Resources { get; set; }
        }
        
    }
}
