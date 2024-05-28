using Enums;
using System;

namespace Utilities
{
    public class ItemDataParser
    {
        private static Random _random = new();

        public static readonly MatchTypeEnum[] cubeItemTypes = { MatchTypeEnum.Red, MatchTypeEnum.Green, MatchTypeEnum.Blue, MatchTypeEnum.Yellow };

        public static ItemTypeEnum GetItemType(string key)
        {
            switch (key)
            {
                case "r":
                case "g":
                case "b":
                case "y":
                case "rand":
                    return ItemTypeEnum.CubeItem;
                case "bo":
                    return ItemTypeEnum.BoxItem;
                case "t":
                    return ItemTypeEnum.TntItem;
                case "s":
                    return ItemTypeEnum.StoneItem;
                case "v":
                case "v1":
                    return ItemTypeEnum.VaseItem;
                default:
                    return ItemTypeEnum.None;
            }
        }

        public static MatchTypeEnum GetMatchType(string key)
        {
            switch (key)
            {
                case "r":
                    return MatchTypeEnum.Red;
                case "g":
                    return MatchTypeEnum.Green;
                case "b":
                    return MatchTypeEnum.Blue;
                case "y":
                    return MatchTypeEnum.Yellow;
                case "rand":
                    return (MatchTypeEnum)_random.Next(0, cubeItemTypes.Length);
                case "t":
                    return MatchTypeEnum.Special;
                default:
                    return MatchTypeEnum.None;
            }
        }

        public static String GetItemKey(ItemTypeEnum itemType, MatchTypeEnum matchType, string state)
        {
            string itemKey = "";
            switch (itemType)
            {
                case ItemTypeEnum.CubeItem:
                    switch (matchType)
                    {
                        case MatchTypeEnum.Red:
                            itemKey = "r";
                            break;
                        case MatchTypeEnum.Green:
                            itemKey = "g";
                            break;
                        case MatchTypeEnum.Blue:
                            itemKey = "b";
                            break;
                        case MatchTypeEnum.Yellow:
                            itemKey = "y";
                            break;
                    }
                    break;
                case ItemTypeEnum.BoxItem:
                    itemKey = "bo";
                    break;
                case ItemTypeEnum.TntItem:
                    itemKey = "t";
                    break;
                case ItemTypeEnum.StoneItem:
                    itemKey = "s";
                    break;
                case ItemTypeEnum.VaseItem:
                    itemKey = "v";
                    break;
            }

            if (!string.IsNullOrEmpty(itemKey) && state != "0")
            {
                itemKey += state;
            }

            return itemKey;
        }
    }
}
