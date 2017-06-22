using OnmyojiHelper.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

    public class StageCategoryDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var attr = typeof(StageCategory).GetMember(((StageCategory)value).ToString())[0].GetCustomAttributes(typeof(DescriptionAttribute), false).First();
            return ((DescriptionAttribute)attr).Description; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
