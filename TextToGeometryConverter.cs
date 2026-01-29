using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Calculadora
{
     public class TextToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            if (value == null)
                return null;

            var text = value.ToString();
            var typeface = new Typeface("Segoe UI");

            var formatted = new FormattedText(
                text,
                CultureInfo.InvariantCulture,
                FlowDirection.LeftToRight,
                typeface, 80,
                Brushes.Black,
                1.0               
                );

            return formatted.BuildGeometry(new Point(0, 0));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
        {
            throw new NotImplementedException();
        }
    }
}
