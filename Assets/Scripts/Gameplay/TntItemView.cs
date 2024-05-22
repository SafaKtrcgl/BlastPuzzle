using Enums;
using Gameplay;

public class TntItemView : SpecialItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        IsMatchable = true;
        IsFallable = true;
        ItemType = ItemTypeEnum.TntItem;
        MatchType = matchType;
    }

    public override void Execute(ExecuteTypeEnum executeType)
    {
        OnItemExecute?.Invoke();
        Destroy(gameObject);
    }
}
