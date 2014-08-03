using System;
using System.Collections.Generic;
using AutoMapper;

namespace Paramount
{
    public interface IMappingBehaviour
    {
        /// <summary>
        /// Callback for getting mapping details
        /// </summary>
        /// <param name="configuration"></param>
        void OnRegisterMaps(IConfiguration configuration);
    }

    public static class MappingExtensions
    {
        private static readonly MappingEngineCache mapCache = new MappingEngineCache();

        /// <summary>
        /// Maps between the <typeparamref name="TFrom"/> and <typeparamref name="TTo"/> types using the <paramref name="from"/> instance.
        /// If the maps have not been defined, OnRegisterMaps will be invoked first.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <returns>Instance of <typeparamref name="TTo"/> maps from <paramref name="from"/> instance</returns>
        public static TTo Map<TFrom, TTo>(this IMappingBehaviour target, TFrom from)
        {
            return mapCache.GetEngine(target).Map<TFrom, TTo>(from);
        }

        /// <summary>
        /// Maps between the <typeparamref name="TFrom"/> and <typeparamref name="TTo"/> types using the <paramref name="from"/> instance, populating the supplied <paramref name="to"/>
        /// If the maps have not been defined, OnRegisterMaps will be invoked first.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns>Instance of <typeparamref name="TTo"/> maps from <paramref name="from"/> instance</returns>
        public static TTo Map<TFrom, TTo>(this IMappingBehaviour target, TFrom from, TTo to)
        {
            return mapCache.GetEngine(target).Map<TFrom, TTo>(from, to);
        }

        /// <summary>
        /// Maps between a list of the <typeparamref name="TFrom"/> and <typeparamref name="TTo"/> types using the <paramref name="from"/> instance.
        /// If the maps have not been defined, OnRegisterMaps will be invoked first.
        /// </summary>
        /// <typeparam name="TFrom"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        /// <param name="target"></param>
        /// <param name="from"></param>
        /// <returns>Instance of List&lt;<typeparamref name="TTo"/>&gt; maps from <paramref name="from"/> instance</returns>
        public static List<TTo> MapList<TFrom, TTo>(this IMappingBehaviour target, List<TFrom> from)
        {
            List<TTo> destination = (List<TTo>)Activator.CreateInstance(typeof(List<>).MakeGenericType(typeof(TTo)));
            return mapCache.GetEngine(target).Map(from, destination);
        }

        /// <summary>
        /// Stores the AutoMappers engines for each object implementing IMappingBehaviour, or IUnityMappingBehaviour
        /// </summary>
        private class MappingEngineCache : ReaderWriterCache<IMappingBehaviour, AutoMapper.IMappingEngine>
        {
            public MappingEngineCache()
                : base(new MapperBehaviourComparer())
            {
            }

            public AutoMapper.IMappingEngine GetEngine(IMappingBehaviour target)
            {
                return this.FetchOrCreate(target, () => MappingEngine(target));
            }

            private static IMappingEngine MappingEngine(IMappingBehaviour target)
            {
                var configuration = new ConfigurationStore(new TypeMapFactory(), AutoMapper.Mappers.MapperRegistry.Mappers);
                
                target.OnRegisterMaps(configuration);
                
                IMappingEngine engine = new MappingEngine(configuration);

                return engine;
            }

            private class MapperBehaviourComparer : IEqualityComparer<IMappingBehaviour>
            {
                public bool Equals(IMappingBehaviour x, IMappingBehaviour y)
                {
                    return x.GetType() == y.GetType();
                }

                public int GetHashCode(IMappingBehaviour obj)
                {
                    return obj.GetType().GetHashCode();
                }
            }
        }
    }
}
