using Enums;
using Helpers;
using System.IO;
using Utilities;

namespace Context
{
    public class InitializationContext : ContextBase
    {
        private void Start()
        {
            Config.LevelCount = Directory.GetFiles(Config.LevelDataPath, Config.LevelDataPattern).Length;
            
            var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);

            
            if (string.IsNullOrEmpty(PlayerPrefsUtility.GetOnGoingLevelData()))
            {
                contextHelper.LoadMainScene();
            }
            else
            {
                contextHelper.LoadGameplayScene();
            }
        }
    }
}
