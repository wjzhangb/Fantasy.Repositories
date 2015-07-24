using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class NavigatingCancelEventArgs : NavigationBaseEventArgs
    {
        public Boolean IsParentFrameNavigating { get; internal set; }
        public NavigationType NavigationType { get; internal set; }
        public Boolean Cancel { get; set; }
    }
}
