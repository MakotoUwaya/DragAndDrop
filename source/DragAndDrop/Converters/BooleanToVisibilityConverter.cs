using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DragAndDrop.Converters
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> 
    /// and false to <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// boolean to visibility
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// visibility to boolean
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
