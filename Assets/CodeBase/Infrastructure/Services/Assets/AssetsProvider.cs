using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, Object> _assets = new Dictionary<string, Object>();

        public void Set<T>(string key, T asset) where T : Object
        {
            _assets[key] = asset;
        }

        public bool TryGet<T>(string key, out T asset) where T : Object
        {
            if (_assets.TryGetValue(key, out Object stored) && stored is T typed)
            {
                asset = typed;
                return true;
            }

            asset = null;
            return false;
        }

        public T Get<T>(string key) where T : Object
        {
            if (TryGet(key, out T asset))
                return asset;

            throw new KeyNotFoundException($"Asset not found for key '{key}' and type '{typeof(T).Name}'.");
        }
    }
}
