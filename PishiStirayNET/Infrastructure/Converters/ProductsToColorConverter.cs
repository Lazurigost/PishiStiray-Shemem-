using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace PishiStiray.Infrastructure.Converters
{
    internal class ProductsToColorConverter : MarkupExtension, IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<int> orderproducts = (List<int>)value;

            Debug.WriteLine(orderproducts.Count);

            if (orderproducts.All(op => op >= 3))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20b2aa"));
            }
            else if (orderproducts.Any(op => op == 0))
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff8c00"));
            }
            return new SolidColorBrush(Colors.White);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
