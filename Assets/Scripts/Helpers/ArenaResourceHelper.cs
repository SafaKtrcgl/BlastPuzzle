using Singleton;
using System.Diagnostics;
using UnityEngine;

namespace Helper
{
    public class ArenaResourceHelper : HelperBase
    {
        [SerializeField] private ArenaResource[] arenaResources;

        public ArenaResource TryGetArenaResource(int arenaIndex)
        {
            if (arenaIndex >= arenaResources.Length) return null;
            return arenaResources[arenaIndex];
        }
    }
}
