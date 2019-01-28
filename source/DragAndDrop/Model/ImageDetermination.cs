using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codeplex.Data;

namespace DragAndDrop.Model
{
    /// <summary>
    /// 画像判定
    /// </summary>
    public class ImageDetermination
    {

        private const string ConsumerKeyString = "gclass_client";

        /// <summary>
        /// 判定処理
        /// </summary>
        /// <param name="imageCards">判定する画像のリスト</param>
        internal static async Task Determinate(IEnumerable<IImageCard> imageCards)
        {
            var timer = System.Diagnostics.Stopwatch.StartNew();

            var client = new HttpClient();
            var content = new ByteArrayContent(CreateStreamContent(imageCards.First().ImageFilePath));
            content.Headers.ContentType = new MediaTypeHeaderValue(@"image/jpg");
            var elapsedImageCreate = $"Content: {timer.ElapsedMilliseconds:#,0}";

            var response = await client.PostAsync(Properties.Settings.Default.ImageDeterminationUrl, content); 
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException(response.ReasonPhrase);
            }

            // TODO: makoto.uwaya 2019-01-21 画像判別結果を反映
            var result = DynamicJson.Parse(await response.Content.ReadAsStringAsync());
            imageCards.Take(1).ToList().ForEach(c => 
            {
                c.IsChecked = true;
                c.AutoCategory = $"prediction_index: {result.prediction_index}\nprobability: {result.probability:#,0.00}";
                c.Time = $"{elapsedImageCreate} Network: {timer.ElapsedMilliseconds:#,0}";
            });
        }

        private static byte[] CreateStreamContent(string imageFilePath)
        {
             return ImageEditor.SquareClipFromImageFile(imageFilePath).ToArray();
        }

        private static MultipartFormDataContent CreateContent(IEnumerable<IImageCard> imageCards)
        {
            var content = new MultipartFormDataContent();
            foreach (var imageCard in imageCards)
            {
                var fileContent = new StreamContent(ImageEditor.SquareClipFromImageFile(imageCard.ImageFilePath));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = ConsumerKeyString,
                    FileName = Path.GetFileName(imageCard.ImageFilePath),
                    Parameters =
                    {
                        new NameValueHeaderValue("fileguid", imageCard.ImageGuid.ToString()),
                    },
                };

                content.Add(fileContent);
            }
            return content;
        }
    }
}
