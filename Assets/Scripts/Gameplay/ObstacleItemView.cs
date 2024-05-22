using DG.Tweening;
using Gameplay;

public abstract class ObstacleItemView : ItemView
{
    public override void Execute()
    {
        transform.DOShakeRotation(.15f);
    }
}
