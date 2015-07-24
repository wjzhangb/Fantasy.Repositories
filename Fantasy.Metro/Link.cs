using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Fantasy.Metro
{
    public class Link : Displayable
    {
        private Uri m_Source = null;
        public Uri Source 
        {
            get { return this.m_Source; }
            set 
            {
                this.m_Source = value;
                this.OnPropertyChanged("Source");
            }
        }

        private ImageSource m_Icon = null;
        public ImageSource Icon
        {
            get { return this.m_Icon; }
            set
            {
                this.m_Icon = value;
                this.OnPropertyChanged("Icon");
            }
        }

        private Style m_TextStyle = null;
        public Style TextStyle 
        {
            get { return this.m_TextStyle; }
            set
            {
                this.m_TextStyle = value;
                this.OnPropertyChanged("TextStyle");
            }
        }
    }
}
