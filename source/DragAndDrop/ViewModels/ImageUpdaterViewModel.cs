using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using DragAndDrop.Model;
using Prism.Commands;
using Prism.Mvvm;
using GongSolutions.Wpf.DragDrop;
using Interfaces;

namespace DragAndDrop.ViewModels
{
    /// <summary>
    /// Image updator ViewModel
    /// </summary>
    public class ImageUpdaterViewModel : BindableBase, IDropTarget
    {
        private readonly ImageDetermination imageDetermination;

        /// <summary>
        /// 画像イメージのコレクション
        /// </summary>
        public ObservableCollection<IImageCard> ImageCards { get; }

        /// <summary>
        /// カード削除コマンド
        /// </summary>
        public DelegateCommand<IImageCard> RemoveCardCommand { get; set; }

        private bool isBusy;
        /// <summary>
        /// 待機状態
        /// </summary>
        public bool IsBusy
        {
            get => this.isBusy;
            set => this.SetProperty(ref this.isBusy, value);
        }

        /// <summary>
        /// Constractor
        /// </summary>
        public ImageUpdaterViewModel(IDeterminator determinator)
        {
            this.ImageCards = new ObservableCollection<IImageCard>();
            BindingOperations.EnableCollectionSynchronization(this.ImageCards, new object());
            this.imageDetermination = new ImageDetermination(determinator);
            this.ImageCards.Add(new AddImageButtonCard(determinator));

            this.RemoveCardCommand = new DelegateCommand<IImageCard>(c =>
            {
                var card = this.ImageCards.SingleOrDefault(ic => ic.ImageGuid == c.ImageGuid);
                if (card == null)
                {
                    return;
                }

                this.ImageCards.Remove(card);
            });
        }

        /// <summary>
        /// 画像カードを追加する
        /// </summary>
        /// <param name="imagePath">画像パス</param>
        public async Task AddImageCards(string[] imagePath)
        {
            foreach (var path in imagePath)
            {
                if (File.Exists(path))
                {
                    await Task.Run(() =>
                    {
                        var imageCard = new ImageCard(path);
                        this.ImageCards.Insert(this.ImageCards.Count - 1, imageCard);
                    });
                }
                else if (Directory.Exists(path))
                {
                    await this.AddImageCards(Directory.GetDirectories(path));
                    await this.AddImageCards(Directory.GetFiles(path));
                }
            }
        }
        
        /// <summary>
        /// File drag over action
        /// </summary>
        /// <param name="dropInfo"></param>
        public void DragOver(IDropInfo dropInfo)
        {
            var data = (IDataObject)dropInfo.Data;
            dropInfo.Effects = data.GetDataPresent(DataFormats.FileDrop, true)
                ? DragDropEffects.Copy
                : DragDropEffects.None;
        }

        /// <summary>
        /// File drop on action
        /// </summary>
        /// <param name="dropInfo"></param>
        public async void Drop(IDropInfo dropInfo)
        {
            var data = (IDataObject)dropInfo.Data;
            if (!(data.GetData(DataFormats.FileDrop) is string[] files))
            {
                return;
            }

            try
            {
                this.IsBusy = true;
                await this.AddImageCards(files);
                this.imageDetermination.Determinate(this.ImageCards.Where(c => !c.IsChecked));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                this.IsBusy = false;
            }
        }
    }
}
