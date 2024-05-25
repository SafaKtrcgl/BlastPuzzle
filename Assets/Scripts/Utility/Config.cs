
using UnityEngine;

public static class Config
{
    public static readonly string LevelDataPath = $"{Application.dataPath}/CaseStudyAssetsNoArea/Levels/";
    public static readonly string LevelDataPattern = "level_*.json";
    public static readonly int BlastMinimumRequiredMatch = 2;
    public static readonly int TntMinimumRequiredMatch = 5;
    public static readonly int TntTnTMinimumRequiredMatch = 2;
    public static readonly int CubeTypeCount = 4;
    public static int LevelCount;
}
