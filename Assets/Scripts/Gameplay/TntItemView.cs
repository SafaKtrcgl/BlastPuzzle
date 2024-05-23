using Enums;
using Gameplay;
using UnityEngine;

public class TntItemView : SpecialItemView
{
    public override void Init(MatchTypeEnum matchType)
    {
        IsFallable = true;
        ItemType = ItemTypeEnum.TntItem;
        base.Init(matchType);
    }

    public override void Execute(ExecuteTypeEnum executeType)
    {
        OnItemExecute?.Invoke(ItemType);
        Destroy(gameObject);
    }
}
