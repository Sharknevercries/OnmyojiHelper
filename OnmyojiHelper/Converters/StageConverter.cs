using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace OnmyojiHelper.Converters
{
    public class StageCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (Models.Enums.StageCategory)value;
        }
    }
}
