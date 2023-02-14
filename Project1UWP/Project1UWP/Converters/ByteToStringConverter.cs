using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Project1UWP.Converters
{
    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object paramter, string language)
        {
            try
            {
                if (value == null) return string.Empty;
                return String.Format("{0:n0}", value);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object paramter, string language)
        {
            try
            {
                string val = value.ToString();

                string cleanVal = new string(val.Where(c => (char.IsDigit(c))).ToArray());
                return System.Convert.ToByte(cleanVal);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
