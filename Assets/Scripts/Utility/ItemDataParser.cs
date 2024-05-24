using Enums;
using System;

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
                return (MatchTypeEnum) _random.Next(0, cubeItemTypes.Length);
            case "t":
                return MatchTypeEnum.Special;
            default:
                return MatchTypeEnum.None;
        }
    }
}
