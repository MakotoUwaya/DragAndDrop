﻿using System;

namespace Interfaces
{
    public interface IImageCard
    {
        Guid ImageGuid { get; }

        string ImageFilePath { get; }

        bool IsChecked { get; set; }

        string AutoCategory { get; set; }

        string Time { get; set; }
    }
}
