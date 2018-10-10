using System.Windows;

namespace DragAndDrop.Behaviors
{
    /// <summary>
    /// WPF ウィンドウ操作に関する振る舞いを拡張
    /// </summary>
    public class CloseWindowAttachedBehavior
    {
        /// <summary>
        /// CloseProperty の Getter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        /// <summary>
        /// CloseProperty の Setter
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        /// <summary>
        /// ウィンドウの開閉状態を表す
        /// <para>Using a DependencyProperty as the backing store for Close.  This enables animation, styling, binding, etc...</para>
        /// </summary>
        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(CloseWindowAttachedBehavior), new PropertyMetadata(false, OnCloseChanged));

        private static void OnCloseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window win))
            {
                // Window以外のコントロールにこの添付ビヘイビアが付けられていた場合は、
                // コントロールの属しているWindowを取得
                win = Window.GetWindow(d);
            }

            if (GetClose(d))
            {
                win?.Close();
            }               
        }
    }
}
