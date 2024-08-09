using System.IO;
using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class LevelDataParser
    {
        public static LevelData GetLevelData(int level)
        {
            string levelFilePath = $"{Config.LevelDataPath}level_{level:D2}.json";

            if (!File.Exists(levelFilePath))
            {
                Debug.Log("No such level " + levelFilePath);
                return null;
            }

            return GetLevelData(File.ReadAllText(levelFilePath));
        }

        public static LevelData GetLevelData(string levelData)
        {
            return JsonUtility.FromJson<LevelData>(levelData);
        }

        public static string GetLevelJson(int width, int height, int moveCount, string[] grid)
        {
            var levelData = new LevelData();
            levelData.level_number = PlayerPrefsUtility.GetCurrentLevel();
            levelData.grid_width = width;
            levelData.grid_height = height;
            levelData.move_count = moveCount;
            levelData.grid = grid;

            return JsonUtility.ToJson(levelData);
        }
    }
}
