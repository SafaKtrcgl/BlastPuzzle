using UnityEngine;

namespace Interfaces.Recycle
{
    public interface IRecyclable
    {
        public GameObject RecyclableGameObject { get; set; }
        public void Recycle();
    }
}
