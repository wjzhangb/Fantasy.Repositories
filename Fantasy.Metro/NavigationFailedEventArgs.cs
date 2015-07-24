using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class NavigationFailedEventArgs : NavigationBaseEventArgs
    {
        public Exception Error { get; internal set; }
        public Boolean Handled { get; set; }
    }
}
