using UnityEngine;

namespace Utilities
{
    public class PlayerPrefsUtility
    {
        private const string _currentLevelKey = "CurrentCompletedLevel";
        private const string _onGoingLevelDataKey = "OnGoingLevelData";

        public static int GetCurrentLevel()
        {
            return PlayerPrefs.GetInt(_currentLevelKey, 1);
        }

        public static void SetCurrentLevel(int level)
        {
            PlayerPrefs.SetInt(_currentLevelKey, level);
        }

        public static string GetOnGoingLevelData()
        {
            return PlayerPrefs.GetString(_onGoingLevelDataKey, null);
        }

        public static void SetOnGoingLevelData(string levelData)
        {
            PlayerPrefs.SetString(_onGoingLevelDataKey, levelData);
        }
    }
}
