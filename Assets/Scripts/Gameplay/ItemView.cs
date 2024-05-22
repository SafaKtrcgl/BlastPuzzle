using Enums;
using System;
using UnityEngine;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour
    {
        [NonSerialized] public bool IsMatchable = false;
        [NonSerialized] public bool IsFallable = false;

        public virtual bool IsSpecial { get; set; } = false;

        public Action OnItemExecute;
        public ItemTypeEnum ItemType { get; protected set; }
        public MatchTypeEnum MatchType { get; protected set; }
        public abstract void Init(MatchTypeEnum matchType);
        public abstract void Execute(ExecuteTypeEnum executeType);
        public abstract void OnNeighbourExecute(ExecuteTypeEnum executeType);

    }
}
