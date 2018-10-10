using System;
using System.Globalization;
using System.Windows.Data;

namespace DragAndDrop.Converters
{
    /// <summary>
    /// <see cref="DateTime"/>を<see cref="string"/>に変換するコンバータ
    /// </summary>
    public class DateTimeToStringConverter : IValueConverter
    {
        /// <summary>
        /// 変換処理
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return $"{dateTime:yyyy/MM/dd HHmm:ss}";
            }
            return string.Empty;
        }

        /// <summary>
        /// 変換戻し処理
        /// <see cref="string"/>を<see cref="DateTime"/>に変換するコンバータ
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.TryParse((string)value, out var dateTime) 
                ? dateTime 
                : default(DateTime);
        }
    }
}
