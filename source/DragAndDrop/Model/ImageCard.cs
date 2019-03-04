using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

using Interfaces;

namespace DragAndDrop.Model
{
    /// <summary>
    /// カードに画像を表示するためのクラス
    /// </summary>
    public class ImageCard : BindableBase, IImageCard
    {
        /// <summary>
        /// 画像ファイルパス
        /// </summary>
        public string ImageFilePath { get; }

        private string autoCategory;
        /// <summary>
        /// 自動設定された画像カテゴリ
        /// </summary>
        public string AutoCategory
        {
            get => this.autoCategory;
            set => this.SetProperty(ref this.autoCategory, value);
        }

        public ReadOnlyDictionary<string, string> Probabilities { get; set; }

        private string manualCategory;
        /// <summary>
        /// 手動設定された画像カテゴリ
        /// </summary>
        public string ManualCategory
        {
            get => this.manualCategory;
            set => this.SetProperty(ref this.manualCategory, value);
        }

        private string time;
        /// <summary>
        /// 自動設定された画像カテゴリ
        /// </summary>
        public string Time
        {
            get => this.time;
            set => this.SetProperty(ref this.time, value);
        }

        /// <summary>
        /// 画像ID
        /// </summary>
        public Guid ImageGuid { get; }

        /// <summary>
        /// 画像ファイルをビューアで開くコマンド
        /// </summary>
        public ICommand PreviewImageCommand { get; }

        private bool isChecked;
        /// <summary>
        /// 画像判別済
        /// TODO: 2018-10-25 makoto.uwaya Undefined
        /// </summary>
        public bool IsChecked
        {
            get => this.isChecked;
            set => this.SetProperty(ref this.isChecked, value);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="imageFilePath">画像ファイルパス</param>
        public ImageCard(string imageFilePath)
        {
            if (!File.Exists(imageFilePath))
            {
                throw new FileNotFoundException("指定されたファイルは存在しません", imageFilePath);
            }

            this.ImageFilePath = imageFilePath;
            this.ImageGuid = Guid.NewGuid();

            this.PreviewImageCommand = new DelegateCommand(() =>
            {
                Process.Start(this.ImageFilePath);
            });
        }

        /// <summary>
        /// 画像ファイルの概要を出力
        /// </summary>
        /// <returns>画像ID</returns>
        public override string ToString()
        {
            return this.ImageGuid.ToString();
        }
    }
}
