using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDeterminator
    {
        Task<string> Determinate(Uri serverUrl, string filePath, string consumerKey);
    }
}
