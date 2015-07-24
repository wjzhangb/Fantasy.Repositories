using System;
using Fantasy.Metro.Controls;

namespace Fantasy.Metro
{
    public class NavigationBaseEventArgs : EventArgs
    {
        public FantasyFrame Frame { get; internal set; }
        public Uri Source { get; internal set; }
    }
}
