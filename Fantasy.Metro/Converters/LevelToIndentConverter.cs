using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Fantasy.Metro.Converters
{
    public class LevelToIndentConverter : IValueConverter
    {
        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            return new Thickness((int)o * DefaultIndentSize, 0, 0, 0);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private const double DefaultIndentSize = 19.0;
    }
}
