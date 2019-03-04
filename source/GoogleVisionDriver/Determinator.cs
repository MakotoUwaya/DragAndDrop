using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Vision.V1;
using Interfaces;

namespace GoogleVisionDriver
{
    public class Determinator : IDeterminator
    {
        public async Task<IImageCard> Determinate(Uri serverUrl, string filePath, string consumerKey = null)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"Not exists file: {filePath}");
            }

            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(filePath);

            var labels = await client.DetectLabelsAsync(image);
            var firstLabel = labels.FirstOrDefault();
            if (firstLabel == null)
            {
                throw new Google.GoogleApiException("Google.Cloud.Vision.V1", "Not response.");
            }

            var dict = new Dictionary<string, string>
            {
                { firstLabel.Description, $"{firstLabel.Score}" }
            };

            var random = new Random();

            return new ImageCard(true, "ポスト")
            {
                Probabilities = new ReadOnlyDictionary<string, string>(dict)
            };
        }
    }
}
