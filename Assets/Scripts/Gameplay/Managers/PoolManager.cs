using Enums;
using Interfaces.Recycle;
using Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Managers
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Dictionary<ItemTypeEnum, Stack<IRecyclable>> _poolDictionary = new();

        public override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        public void SendToPool(IRecyclable recyclableObject, ItemTypeEnum itemType)
        {
            recyclableObject.RecyclableGameObject.transform.SetParent(transform);
            recyclableObject.RecyclableGameObject.SetActive(false);

            if (_poolDictionary.ContainsKey(itemType))
            {
                _poolDictionary[itemType].Push(recyclableObject);
            }
            else
            {
                _poolDictionary.Add(itemType, new Stack<IRecyclable>());
                _poolDictionary[itemType].Push(recyclableObject);
            }
        }

        public T GetFromPool<T>(ItemTypeEnum itemType) where T : MonoBehaviour
        {
            if (!_poolDictionary.ContainsKey(itemType) || _poolDictionary[itemType].Count == 0) return null;

            var item = _poolDictionary[itemType].Pop();

            item.RecyclableGameObject.SetActive(true);

            return item as T;
        }
    }
}
