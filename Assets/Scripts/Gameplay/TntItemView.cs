using Enums;
using Gameplay;

public class TntItemView : SpecialItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        base.Init(matchType);

        IsFallable = true;
        ItemType = ItemTypeEnum.TntItem;
    }

    public override void Execute(ExecuteTypeEnum executeType)
    {
        OnItemExecute?.Invoke();
        Destroy(gameObject);
    }
}
