using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// ファイル送信
        /// </summary>
        /// <param name="filePathList">送信するファイルパスのリスト</param>
        public static async Task SendImage(IEnumerable<string> filePathList)
        {
            var request = new HttpClient();
            var response = await request.PostAsync(Properties.Settings.Default.ImageDeterminationUrl, CreateContent(filePathList));
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException(response.ReasonPhrase);
            }
        }

        private static MultipartFormDataContent CreateContent(IEnumerable<string> filePathList)
        {
            var content = new MultipartFormDataContent();
            foreach (var filePath in filePathList)
            {
                var fileContent = new StreamContent(File.OpenRead(filePath));
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(filePath)
                };

                content.Add(fileContent);
            }
            return content;
        }
    }
}
