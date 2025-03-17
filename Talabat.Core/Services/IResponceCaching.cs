using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Services
{
    public interface IResponceCaching
    {
        Task CachingResponseAsync(string cachKey, object responce, TimeSpan timeToLive);
        Task<string> GetCachingResponseAsync(string cachKey);
    }
}
