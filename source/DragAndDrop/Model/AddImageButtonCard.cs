using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Prism.Commands;

namespace DragAndDrop.Model
{
    internal class AddImageButtonCard : IImageCard
    {
        public Guid ImageGuid => Guid.Empty;

        public string ImageFilePath => string.Empty;

        public string AutoCategory {
            get => string.Empty;
            set { }
        }

        public bool IsChecked
        {
            get => true;
            set {}
        }

        public string Time
        {
            get => string.Empty;
            set { }
        }

        /// <summary>
        /// 画像ファイルを追加するコマンド
        /// </summary>
        public ICommand AddImageCommand { get; }

        public AddImageButtonCard()
        {
            this.AddImageCommand = new DelegateCommand<object>(async obj =>
            {

                if (!(obj is MainWindowViewModel vm))
                {
                    Debug.WriteLine($"Failed get ViewModel class. instead: {obj.GetType()}");
                    return;
                }

                var dialog = new OpenFileDialog
                {
                    Filter = "画像ファイル(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif",
                    Multiselect = true,
                    Title = "画像ファイルを追加",
                };

                if (!dialog.ShowDialog() ?? false)
                {
                    return;
                }

                try
                {
                    await vm.AddImageCards(dialog.FileNames);
                    await ImageDetermination.Determinate(vm.ImageCards.Where(c => !c.IsChecked));

                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            });
        }
    }
}
