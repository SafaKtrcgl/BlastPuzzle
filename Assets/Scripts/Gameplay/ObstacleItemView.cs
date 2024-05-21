using DG.Tweening;
using Gameplay;

public abstract class ObstacleItemView : ItemView
{
    public override void OnInteract()
    {
        transform.DOShakeRotation(.15f);
    }
}
