using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Metro
{
    public class Displayable : NotifyPropertyChanged
    {
        private String m_DisplayName = String.Empty;
        public string DisplayName
        {
            get { return this.m_DisplayName; }
            set
            {
                this.m_DisplayName = value;
                OnPropertyChanged("DisplayName");
            }
        }
    }
}
