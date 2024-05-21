using Singleton;
using System.Linq;
using UnityEngine;

namespace Helper
{
    public class ArenaResourceHelper : HelperBase
    {
        [SerializeField] private ArenaResource[] arenaResources;

        public ArenaResource TryGetArenaResource(int arenaId)
        {
            return arenaResources.FirstOrDefault(x => x.ArenaId == arenaId);
        }
    }
}
