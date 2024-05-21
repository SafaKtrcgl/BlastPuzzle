using Helper;
using Singleton;

namespace Context
{
    public class InitializationContext : ContextBase
    {
        private void Start()
        {
            //TODO: Check game information
            var contextHelper = HelperResources.Instance.GetHelper<ContextHelper>(HelperEnum.ContextHelper);
            contextHelper.LoadMainScene();
        }
    }
}
