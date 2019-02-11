using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codeplex.Data;
using Interfaces;

namespace DragAndDrop.Model
{
    /// <summary>
    /// 画像判定
    /// </summary>
    public class ImageDetermination
    {

        private const string ConsumerKeyString = "gclass_client";

        private readonly IDeterminator _determinator;

        /// <summary>
        /// Constractor
        /// </summary>
        /// <param name="determinator">判別処理の実装</param>
        public ImageDetermination(IDeterminator determinator)
        {
            this._determinator = determinator;
        }

        /// <summary>
        /// 判定処理
        /// </summary>
        /// <param name="imageCards">判定する画像のリスト</param>
        internal void Determinate(IEnumerable<IImageCard> imageCards)
        {
            Parallel.ForEach(imageCards, imageCard =>
            {
                var timer = System.Diagnostics.Stopwatch.StartNew();

                var resultString = this._determinator.Determinate(
                    new Uri(Properties.Settings.Default.ImageDeterminationUrl),
                    imageCard.ImageFilePath,
                    ConsumerKeyString
                ).Result;

                var result = DynamicJson.Parse(resultString);
                imageCard.IsChecked = true;
                imageCard.AutoCategory = $"prediction_index: {result.prediction_index}\nprobability: {result.probability}";
                imageCard.Time = $"Determinate time: {timer.ElapsedMilliseconds:#,0}";
            });
        }
    }
}
