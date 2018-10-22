using System;
using System.Linq;
using System.Windows;
using DragAndDrop.Model;

namespace DragAndDrop
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void MetroWindow_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop, true)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;            
        }

        private async void MetroWindow_Drop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(DataFormats.FileDrop) is string[] files))
            {
                return;
            }

            if (!(this.DataContext is MainWindowViewModel vm))
            {
                return;
            }

            try
            {
                await vm.AddImageCards(files);
                await ImageDetermination.SendImage(vm.ImageCards.Select(c => c.ImageFilePath));

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
