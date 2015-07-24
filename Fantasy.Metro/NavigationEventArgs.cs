using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class NavigationEventArgs : NavigationBaseEventArgs
    {
        public NavigationType NavigationType { get; internal set; }
        public Object Content { get; internal set; }
        public Object Parameter { get; set; }
    }
}
