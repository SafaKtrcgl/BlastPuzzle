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
}
