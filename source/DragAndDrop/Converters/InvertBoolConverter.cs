using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DragAndDrop.Converters
{
    /// <summary>
    /// <see cref="bool"/>を反転するコンバータ
    /// </summary>
    public class InvertBoolConverter : IValueConverter
    {
        /// <summary>
        /// 変換処理
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool?)value != true;
        }

        /// <summary>
        /// 変換戻し処理
        /// <see cref="Visibility"/>を<see cref="bool"/>に変換するコンバータ
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }
    }
}
