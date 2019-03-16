using System.IO;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mbiza.Address.Book.Api.Cache
{
    public static class CacheManager<T> where T : new()
    {
        /// <summary>
        /// Gets the specified cache.
        /// </summary>
        /// <param name="_cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public static T Get(IMemoryCache _cache, string cacheKey)
        {
            var cachedObj = _cache.Get(cacheKey);
            if (cachedObj == null)
                return new T();
            else
                return (T)cachedObj;
        }

        /// <summary>
        /// Sets the specified cache.
        /// </summary>
        /// <param name="_cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <param name="cacheObj">The cache object.</param>
        public static void Set(IMemoryCache _cache, string cacheKey, T cacheObj)
        {
            _cache.Set(cacheKey, cacheObj);
        }

        /// <summary>
        /// Removes the specified cache.
        /// </summary>
        /// <param name="_cache">The cache.</param>
        /// <param name="cacheKey">The cache key.</param>
        public static void Remove(IMemoryCache _cache, string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        /// <summary>
        /// Froms the byte array.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static T FromByteArray(byte[] data)
        {
            if (data == null)
                return default(T);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }

        /// <summary>
        /// To the byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static byte[] ToByteArray(T obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}
