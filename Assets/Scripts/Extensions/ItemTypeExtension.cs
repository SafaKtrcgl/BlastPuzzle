using Enums;

namespace Extensions
{
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

        public static bool IsSpecial(this ItemTypeEnum itemType)
        {
            return itemType is
                ItemTypeEnum.TntItem or
                ItemTypeEnum.TntTntItem;
        }

        public static bool IsRecyclable(this ItemTypeEnum itemType)
        {
            return itemType is
                ItemTypeEnum.CubeItem;
        }
    }
}
