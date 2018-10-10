using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DragAndDrop.Converters
{
    /// <summary>
    /// <see cref="bool"/>を<see cref="Visibility"/>に変換するコンバータ
    /// </summary>
    public class BoolToVisibleConverter : IValueConverter
    {
        /// <summary>
        /// 変換処理
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool?)value == true) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// 変換戻し処理
        /// <see cref="Visibility"/>を<see cref="bool"/>に変換するコンバータ
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Visibility visibility))
            {
                return false;
            }
            return visibility == Visibility.Visible;
        }
    }
}
