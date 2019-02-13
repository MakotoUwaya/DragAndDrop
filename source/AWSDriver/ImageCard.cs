using System;
using Interfaces;

namespace AWSDriver
{
	public class ImageCard : IImageCard
	{
		public Guid ImageGuid => Guid.Empty;

		public string ImageFilePath => string.Empty;

		public bool IsChecked { get; set; }

		public string AutoCategory { get; set; }

		public string Time { get; set; }

		public ImageCard(bool isChecked, string autoCategory)
		{
			this.IsChecked = isChecked;
			this.AutoCategory = autoCategory;
		}
	}
}
