using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro.Controls
{
    public interface IFantasyPagingContract
    {
        int GetTotalCount();
        ICollection<Object> GetItems(int startIndex, int numberOfItems, Object filterTag);
    }
}
