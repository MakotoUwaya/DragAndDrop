﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
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
        public async Task<string> Determinate(Uri serverUrl, string filePath, string consumerKey = "gclass_client")
        {
            var client = new HttpClient();
            var content = new ByteArrayContent(CreateStreamContent(filePath));
            content.Headers.ContentType = new MediaTypeHeaderValue(@"image/jpg");

            var response = await client.PostAsync(serverUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new WebException(response.ReasonPhrase);
            }
            return await response.Content.ReadAsStringAsync();
        }

        private static byte[] CreateStreamContent(string imageFilePath)
        {
             return ImageEditor.SquareClipFromImageFile(imageFilePath).ToArray();
        }
    }
}
