using System.IO;
using UnityEngine;

namespace Utility
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
    }
}
