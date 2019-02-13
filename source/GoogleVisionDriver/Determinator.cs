using System;
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
	        return new ImageCard(true, $"Category: {firstLabel.Description}\nProbability: {firstLabel.Score}");
		}
    }
}
