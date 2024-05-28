using System.IO;
using UnityEngine;

namespace Utilities
{
    [System.Serializable]
    public class LevelDataParser
    {
        public int level_number;
        public int grid_width;
        public int grid_height;
        public int move_count;
        public string[] grid;

        public static LevelDataParser GetLevelData(int level)
        {
            string levelFilePath = $"{Config.LevelDataPath}level_{level:D2}.json";

            if (!File.Exists(levelFilePath))
            {
                Debug.Log("No such level " + levelFilePath);
                return null;
            }

            return JsonUtility.FromJson<LevelDataParser>(File.ReadAllText(levelFilePath));
        }

        public static LevelDataParser GetLevelData(string levelData)
        {
            return JsonUtility.FromJson<LevelDataParser>(levelData);
        }

        public static string GetLevelJson(int width, int height, int moveCount, string[] grid)
        {
            var levelData = new LevelDataParser();
            levelData.level_number = PlayerPrefsUtility.GetCurrentLevel();
            levelData.grid_width = width;
            levelData.grid_height = height;
            levelData.move_count = moveCount;
            levelData.grid = grid;

            return JsonUtility.ToJson(levelData);
        }
    }
}
