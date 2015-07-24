using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class LinkCollection : ObservableCollection<Link>
    {
        public LinkCollection()
            : base()
        { 
        }

        public LinkCollection(IEnumerable<Link> collection)
            : base(collection)
        {
            //foreach (Link link in collection)
            //{
            //    Add(link);
            //}
        }

        public LinkCollection(IList<Link> collection)
            : base(collection)
        {
        }
    }
}
