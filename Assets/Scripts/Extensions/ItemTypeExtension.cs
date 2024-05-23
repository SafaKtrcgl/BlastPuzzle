using Enums;

public static class ItemTypeExtension
{
    public static bool IsObstacle(this ItemTypeEnum itemType)
    {
        return itemType is 
            ItemTypeEnum.BoxItem or 
            ItemTypeEnum.StoneItem or 
            ItemTypeEnum.VaseItem;
    }

    public static bool IsMatchable(this ItemTypeEnum itemType)
    {
        return itemType is
            ItemTypeEnum.CubeItem or
            ItemTypeEnum.TntItem;
    }

    public static bool IsFallable(this ItemTypeEnum itemType)
    {
        return itemType is
            ItemTypeEnum.CubeItem or
            ItemTypeEnum.TntItem or
            ItemTypeEnum.VaseItem;
    }

    public static bool IsSpecial(this ItemTypeEnum itemType)
    {
        return itemType is
            ItemTypeEnum.TntItem;
    }

    public static int GetRequiredAmount(this ItemTypeEnum itemType)
    {
        switch (itemType)
        {
            case ItemTypeEnum.TntItem:
                return 5;
            case ItemTypeEnum.TntTntItem:
                return 2;
            default:
                return 0;
        }
    }
}
