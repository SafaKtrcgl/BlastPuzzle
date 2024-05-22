using Enums;
using UnityEngine;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour
    {
        public bool IsMatchable;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        public abstract void Init(MatchTypeEnum matchType);
        public abstract void Execute();

    }
}
