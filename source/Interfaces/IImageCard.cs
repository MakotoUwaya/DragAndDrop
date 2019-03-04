using System;
using System.Collections.ObjectModel;

namespace Interfaces
{
    public interface IImageCard
    {
        Guid ImageGuid { get; }

        string ImageFilePath { get; }

        bool IsChecked { get; set; }

        string AutoCategory { get; set; }

        ReadOnlyDictionary<string, string> Probabilities { get; set; }

        string Time { get; set; }
    }
}
