using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Fantasy.Metro.Controls
{
    public class FantasyPopup : Popup
    {
        public FantasyPopup()
        {
            this.DefaultStyleKey = typeof(FantasyPopup);
            this.AllowsTransparency = true;
            this.StaysOpen = false;
        }

        
    }
}
