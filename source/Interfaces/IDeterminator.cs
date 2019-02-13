using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDeterminator
    {
        Task<IImageCard> Determinate(Uri serverUrl, string filePath, string consumerKey);
    }
}
