using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace DragAndDrop
{
    /// <summary>
    /// ウィンドウの管理するためのメソッドを定義するクラス
    /// </summary>
    public static class WindowManager
    {
        /// <summary>
        /// 対象のウィンドウ型のウィンドウインスタンスを探してアクティブにする
        /// 開かれていなければ新たに開く
        /// </summary>
        /// <typeparam name="TWindow">対象のウィンドウ型</typeparam>
        public static void ShowOrActivate<TWindow>()
            where TWindow : Window, new()
        {
            // 対象Windowが開かれているか探す
            var window = System.Windows.Application.Current.Windows.OfType<TWindow>().FirstOrDefault();
            if (window == null)
            {
                // 開かれてなかったら開く
                window = new TWindow();
                window.Show();
            }
            else
            {
                // 既に開かれていたらアクティブにする
                window.Activate();
            }
        }

        /// <summary>
        /// ウィンドウの状態を確認する
        /// </summary>
        public static void CheckWindowStatus()
        {
            foreach (var window in System.Windows.Application.Current.Windows)
            {
                Debug.Print(window.ToString());
            }
            Debug.Print($"{System.Windows.Application.Current.Windows.Count}");
        }
    }
}
