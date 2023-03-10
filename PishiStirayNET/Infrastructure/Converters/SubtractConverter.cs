using System;
using System.Windows.Data;
using System.Windows.Markup;

namespace PishiStiray.Infrastructure.Converters
{
    public class SubtractConverter : MarkupExtension, IValueConverter
    {
        public double Value { get; set; }

        public object Convert(object baseValue, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = System.Convert.ToDouble(baseValue);
            return val - Value;
        }

        public object ConvertBack(object baseValue, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
