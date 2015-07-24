using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class FragmentNavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the uniform resource identifier (URI) fragment.
        /// </summary>
        public String Fragment { get; internal set; }
        public Object Parameter { get; set; }
    }
}
