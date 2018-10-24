﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using Prism.Commands;

namespace DragAndDrop.Model
{
    /// <summary>
    /// カードに画像を表示するためのクラス
    /// </summary>
    public class ImageCard : IImageCard
    {
        /// <summary>
        /// 画像ファイルパス
        /// </summary>
        public string ImageFilePath { get; }

        /// <summary>
        /// 自動設定された画像カテゴリ
        /// </summary>
        public string AutoCategory { get; set; }

        /// <summary>
        /// 手動設定された画像カテゴリ
        /// </summary>
        public string ManualCategory { get; set; }

        /// <summary>
        /// 画像ID
        /// </summary>
        public Guid ImageGuid { get; }

        /// <summary>
        /// 画像ファイルをビューアで開くコマンド
        /// </summary>
        public ICommand ImagePreviewCommand { get; }

        /// <summary>
        /// 画像判別済
        /// TODO: 2018-10-25 makoto.uwaya Undefined
        /// </summary>
        public bool IsChecked { get; set; }

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

            this.ImagePreviewCommand = new DelegateCommand(() =>
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
