using System;
using System.Runtime.Caching;

namespace TaskManagementSystem.Utils
{
    public class Cache
    {
        private static readonly MemoryCache cache = MemoryCache.Default;

        public static void Set(string key, object obejto, int seconds)
        {
           cache.Set(key, obejto, DateTimeOffset.Now.AddSeconds(seconds));
        }

        public static void Remove(string key)
        {
            cache.Remove(key);
        }

        public static object Get(string key)
        {
            return cache.Get(key);
        }
    }
}