using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro.Controls
{
    public class SourceEventArgs : EventArgs
    {
        public SourceEventArgs(Uri from, Uri to)
            : base()
        {
            this.From = from;
            this.To = to;
        }

        public Uri From { get; private set; }
        public Uri To { get; private set; }
    }
}
