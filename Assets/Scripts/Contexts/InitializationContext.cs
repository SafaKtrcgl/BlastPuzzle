using Helper;
using Singleton;
using System.IO;

namespace Context
{
    public class InitializationContext : ContextBase
    {
        private void Start()
        {
            Config.LevelCount = Directory.GetFiles(Config.LevelDataPath, Config.LevelDataPattern).Length;

            var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);
            contextHelper.LoadMainScene();
        }
    }
}
