using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            var request = new HttpClient();
            var response = await request.PostAsync(Properties.Settings.Default.ImageDeterminationUrl, CreateContent(imageCards));
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException(response.ReasonPhrase);
            }

            // TODO: makoto.uwaya 2019-01-21 画像判別結果を反映
            var result = await response.Content.ReadAsStringAsync();
            imageCards.ToList().ForEach(c => 
            {
                c.IsChecked = true;
                c.AutoCategory = "画像のカテゴリを取得！";
            });
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
