using Enums;
using UnityEngine;

namespace Gameplay
{
    public abstract class ItemView : MonoBehaviour
    {
        protected ItemTypeEnum itemType;
        public abstract void OnInteract();
    }
}
