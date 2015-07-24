using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro.Controls
{
    public delegate void FantasyTileClickEventHandler(Object sender, 
        FantasyTileEventArgs<FantasyTile> e);
    
    public class FantasyTileEventArgs<T> : EventArgs
        where T : class
    {
        public FantasyTileEventArgs(T tag = null)
        {
            this.Tag = tag;
        }

        public String Title { get; set; }
        public Uri ImageUri { get; set; }
        public Uri NavigationUri { get; set; }
        public T Tag { get; private set; }
    }
}
