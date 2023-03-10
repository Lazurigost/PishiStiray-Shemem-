using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace PishiStiray.Infrastructure.Converters
{
    public class IntToBrushConverter :  MarkupExtension,IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            float? number = (float?)value;

            if(number == null || number <= 15)
            {
                return new SolidColorBrush(Colors.White);
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#7fff00"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
