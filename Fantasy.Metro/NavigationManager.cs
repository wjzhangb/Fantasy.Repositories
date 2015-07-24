using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Fantasy.Metro.Controls;

namespace Fantasy.Metro
{
    public static class NavigationManager
    {
        public const String FrameSelf = "FrameSelf";
        public const String FrameTop = "FrameTop";
        public const String FrameParent = "FrameParent";

        public static FantasyFrame ContentFrame { get; internal set; }
        public static void Navigate(Uri uri, Object parameter = null)
        {
            if (uri != null && ContentFrame != null)
            {
                ContentFrame.NavigatingParameter = parameter;
                ContentFrame.Source = uri;
            }
        }
        public static void Navigate(String uri, Object parameter = null)
        {
            if (ContentFrame != null && !String.IsNullOrEmpty(uri))
            {
                ContentFrame.NavigatingParameter = parameter;
                ContentFrame.Source = new Uri(uri, UriKind.Relative);
            }
        }

        //public static void SetContent(Object content)
        //{
        //    if (content != null)
        //    {
        //        ContentFrame.SetContent(content);
        //    }
        //}

        public static void Navigate(FantasyFrame frame, String uri, Object parameter = null)
        {
            if (frame != null && !String.IsNullOrEmpty(uri))
            {
                frame.NavigatingParameter = parameter;
                frame.Source = new Uri(uri, UriKind.RelativeOrAbsolute);
            }
        }

        public static FantasyFrame FindFrame(String name, FrameworkElement context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // collect all ancestor frames
            FantasyFrame[] frames = context.Ancestors().OfType<FantasyFrame>().ToArray();
            if (String.IsNullOrEmpty(name) || name.Equals(FrameSelf))
            {
                // find the first frame.
                return frames.FirstOrDefault();
            }
            if (name.Equals(FrameParent))
            {
                // find parent frame
                return frames.Skip(1).FirstOrDefault();
            }
            if (name.Equals(FrameTop))
            {
                // find top-most frame
                return frames.LastOrDefault();
            }

            // find ancestor frame having a name matching the target
            FantasyFrame frame = frames.FirstOrDefault(f => f.Name == name);
            if (frame == null)
            {
                // find frame in context scope
                frame = context.FindName(name) as FantasyFrame;
                if (frame == null)
                {
                    // find frame in scope of ancestor frame content
                    FantasyFrame parent = frames.FirstOrDefault();
                    if (parent != null && parent.Content != null)
                    {
                        FrameworkElement content = parent.Content as FrameworkElement;
                        if (content != null)
                        {
                            frame = content.FindName(name) as FantasyFrame;
                        }
                    }
                }
            }

            return frame;
        }

        public static Uri RemoveFragment(Uri uri)
        {
            string fragment;
            return RemoveFragment(uri, out fragment);
        }

        public static Uri RemoveFragment(Uri uri, out string fragment)
        {
            fragment = null;
            if (uri != null)
            {
                String value = uri.OriginalString;
                int i = value.IndexOf('#');
                if (i != -1)
                {
                    fragment = value.Substring(i + 1);
                    uri = new Uri(value.Substring(0, i), uri.IsAbsoluteUri ? UriKind.Absolute : UriKind.Relative);
                }
            }

            return uri;
        }
    }
}
