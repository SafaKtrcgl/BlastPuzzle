using Enums;
using UnityEngine;

namespace Interfaces.Recycle
{
    public interface IRecyclable
    {
        public RecyclableTypeEnum RecyclableType { get; set; }
        public GameObject RecyclableGameObject { get; set; }
        public void Recycle();
    }
}
