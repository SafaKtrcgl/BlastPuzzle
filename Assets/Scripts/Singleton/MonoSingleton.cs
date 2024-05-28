using UnityEngine;

namespace Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;
        public static T Instance => instance;

        public virtual void Awake()
        {
            instance = this as T;
        }
    }
}
