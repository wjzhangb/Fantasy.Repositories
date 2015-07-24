using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro.Utils
{
    public static class StringExtensions
    {
        public static Boolean SeverelyEquals(this String str, String str2)
        {
            return str.Equals(str2, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
