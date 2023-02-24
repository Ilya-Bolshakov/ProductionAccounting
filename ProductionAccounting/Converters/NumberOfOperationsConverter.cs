using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ProductionAccounting.Converters
{
    [ValueConversion(typeof(int), typeof(string))]
    public class NumberOfOperationsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int? v = (int?)value;
            if (v == null) return "Не удается найти данные по выбранным параметрам";
            return $"Количество выполненных операций: {v}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var regex = new Regex(@"\d+");
            if (value is string str)
            {
                var matches = regex.Matches(str);
                if (matches.Count != 1) return null;
                else
                {
                    var num = int.Parse(matches.First().Value);
                    return num;
                }
            }
            return null;
        }
    }
}
