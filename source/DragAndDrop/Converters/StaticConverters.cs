using System.Windows.Data;

namespace DragAndDrop.Converters
{
    /// <summary>
    /// Static converters
    /// </summary>
    public static class StaticConverters
    {
        /// <summary>
        /// <see cref="BooleanToVisibilityConverter" />
        /// </summary>
        public static IValueConverter BooleanToVisibility { get; }

        /// <summary>
        /// <see cref="DateTimeToStringConverter" />
        /// </summary>
        public static IValueConverter DateTimeToString { get; }

        /// <summary>
        /// <see cref="InvertBoolConverter" />
        /// </summary>
        public static IValueConverter InvertBool { get; }

        /// <summary>
        /// <see cref="EnumToBooleanConverter" />
        /// </summary>
        public static IValueConverter EnumToBoolean { get; }

        static StaticConverters()
        {
            BooleanToVisibility = new BooleanToVisibilityConverter();
            DateTimeToString = new DateTimeToStringConverter();
            InvertBool = new InvertBoolConverter();
            EnumToBoolean = new EnumToBooleanConverter();
        }
    }
}
