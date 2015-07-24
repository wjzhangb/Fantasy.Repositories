using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro.Controls
{
    public delegate void FantasyTileItemClickEventHandler(Object sender, 
        FantasyTileItemEventArgs<FantasyTileItem> e);
    
    public class FantasyTileItemEventArgs<T> : EventArgs
        where T : class
    {
        public FantasyTileItemEventArgs(T tag = null)
        {
            this.Tag = tag;
        }

        public String Title { get; set; }
        public String Description { get; set; }
        public Uri ImageUri { get; set; }
        public Uri NavigationUri { get; set; }
        public T Tag { get; private set; }
    }
}
