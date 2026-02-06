using UnityEngine;

namespace CodeBase.Infrastructure.Services.Assets
{
    public interface IAssetsProvider
    {
        void Set<T>(string key, T asset) where T : Object;
        bool TryGet<T>(string key, out T asset) where T : Object;
        T Get<T>(string key) where T : Object;
    }
}
