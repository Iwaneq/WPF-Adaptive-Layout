using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ResponsiveDEMOS.Converters
{
    public class BreakpointConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Actual Height/Width</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Breakpoint</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double actual = (double)value;
            int breakpoint = int.Parse(parameter.ToString());

            return actual < breakpoint;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
