using UnityEngine;

public interface IRecyclable
{
    public GameObject RecyclableGameObject { get; set; }
    public void Recycle();
}
