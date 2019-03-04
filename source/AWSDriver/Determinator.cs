using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Codeplex.Data;
using Interfaces;

namespace AWSDriver
{
    /// <summary>
    /// Image detarmination
    /// </summary>
    public class Determinator : IDeterminator
    {
        /// <summary>
        /// Detarmination
        /// </summary>
        /// <param name="serverUrl">Execute determinatation server url</param>
        /// <param name="filePath">Target file path</param>
        /// <param name="consumerKey">(option)access user assertion</param>
        /// <returns></returns>
        public async Task<IImageCard> Determinate(Uri serverUrl, string filePath, string consumerKey = "gclass_client")
        {
            var client = new HttpClient();
            var content = new ByteArrayContent(CreateStreamContent(filePath));
            content.Headers.ContentType = new MediaTypeHeaderValue(@"image/jpg");

            var response = await client.PostAsync(serverUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException(response.ReasonPhrase);
            }
            var result =  DynamicJson.Parse(await response.Content.ReadAsStringAsync());
            var dict = new Dictionary<string, string>
            {
                {result.prediction, result.probability}
            };

            return new ImageCard(true, result.prediction)
            {
                Probabilities = new ReadOnlyDictionary<string, string>(dict)
            };
        }

        private static byte[] CreateStreamContent(string imageFilePath)
        {
             return ImageEditor.SquareClipFromImageFile(imageFilePath).ToArray();
        }
    }
}
