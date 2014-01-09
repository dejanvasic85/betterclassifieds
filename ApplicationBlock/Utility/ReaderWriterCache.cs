using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Paramount
{
    public abstract class ReaderWriterCache<TKey, TValue>
    {
        private readonly ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        private readonly Dictionary<TKey, TValue> cache;

        private static readonly List<Type> KnownTypesWithImplicitEquality = new List<Type>() { typeof(Type) };

        /// <summary>Gets all the values currently in the cache</summary>
        public List<KeyValuePair<TKey, TValue>> Entries { get { return this.cache.Select(x => x).ToList(); } }

        /// <summary>
        /// default constructor when <typeparamref name="TKey"/> doesn't need an equality comparer implementation.
        /// </summary>
        protected ReaderWriterCache()
            : this(null)
        {
        }

        /// <summary>
        /// intialises underlying dictionary using the supplied Equality comparer
        /// </summary>
        /// <param name="comparer"></param>
        protected ReaderWriterCache(IEqualityComparer<TKey> comparer)
        {
            //This enforces the use of a comparer when the key type TKey is a class.
            if (comparer == null && typeof(TKey).IsClass && !KnownTypesWithImplicitEquality.Contains(typeof(TKey)))
                throw new NotSupportedException("All implementations of ReaderWriterCache where the key is not a value type must supply a comparer.");

            this.cache = new Dictionary<TKey, TValue>(comparer);
        }

        /// <summary>
        /// Provides thread safe access to cache, using supplied function to create new item if not in cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="creator"></param>
        /// <returns></returns>
        protected TValue FetchOrCreate(TKey key, Func<TValue> creator)
        {
            TValue local;

            // Aquire a local and attempt to read
            this.locker.EnterReadLock();
            try
            {
                if (this.cache.TryGetValue(key, out local))
                    return local;
            }
            finally
            {
                this.locker.ExitReadLock();
            }

            // Not in cache, create the item via the Func delegate and attempt to store
            local = creator();

            return Add(key, local);
        }

        /// <summary>
        /// Adds the supplied value to the dictionary using the supplied key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected TValue Add(TKey key, TValue value)
        {
            this.locker.EnterUpgradeableReadLock();
            try
            {
                TValue local;
                if (this.cache.TryGetValue(key, out local))
                    return local;

                this.locker.EnterWriteLock();

                try
                {
                    this.cache.Add(key, value);
                }
                finally
                {
                    this.locker.ExitWriteLock();
                }


                return value;
            }
            finally
            {
                this.locker.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// Clears all entries in the cache.  For each item, the OnRemove method will be called
        /// </summary>
        public void Clear()
        {
            this.locker.EnterWriteLock();

            try
            {
                this.cache.ForEach<KeyValuePair<TKey, TValue>>(item => OnRemove(item.Value));
                this.cache.Clear();
            }
            finally
            {
                this.locker.ExitWriteLock();
            }
        }

        /// <summary>
        /// Removes the specfic 
        /// </summary>
        /// <param name="key"></param>
        public void RemoveItem(TKey key)
        {
            this.locker.EnterWriteLock();

            try
            {
                TValue item;
                if (this.cache.TryGetValue(key, out item))
                {
                    this.OnRemove(item);
                    this.cache.Remove(key);
                }
            }
            finally
            {
                this.locker.ExitWriteLock();
            }
        }

        /// <summary>
        /// Provides a hook for derived implementations to clean up any resources
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnRemove(TValue item)
        {
        }
    }
}