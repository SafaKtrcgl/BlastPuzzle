using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class ArenaResource : ScriptableObject
    {
        [SerializeField] private int arenaId;
        [SerializeField] private Sprite arenaBackgroundSprite;

        public int ArenaId => arenaId;
        public Sprite ArenaBackgroundSprite => arenaBackgroundSprite;
    }
}
