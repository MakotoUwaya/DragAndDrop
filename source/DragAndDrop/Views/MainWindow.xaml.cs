using System;
using System.Linq;
using System.Windows;

using DragAndDrop.Model;
using DragAndDrop.ViewModels;
using Interfaces;

namespace DragAndDrop.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    internal partial class MainWindow
    {
        private readonly ImageDetermination imageDetermination;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainWindow(IDeterminator determinator)
        {
            this.InitializeComponent();
            this.imageDetermination = new ImageDetermination(determinator);
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
                this.imageDetermination.Determinate(vm.ImageCards.Where(c => !c.IsChecked));

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}
