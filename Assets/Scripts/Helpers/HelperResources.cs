using Base;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Singleton
{
    public class HelperResources : MonoSingleton<HelperResources>
    {
        [SerializeField] private HelperResource[] helpers;

        private Dictionary<HelperEnum, HelperBase> helpersDictionary = new();

        public T GetHelper<T>(HelperEnum helperEnum) where T : HelperBase
        {
            if (helpersDictionary.ContainsKey(helperEnum))
            {
                return helpersDictionary[helperEnum] as T;
            }
            else
            {
                return CreateHelper<T>(helperEnum);
            }
        }

        private T CreateHelper<T>(HelperEnum helperEnum) where T : HelperBase
        {
            var helper = helpers.Where(x => x.HelpersEnum == helperEnum).FirstOrDefault();
            if (helper == null) return null;

            helpersDictionary.Add(helperEnum, Instantiate(helper.HelperClass, transform));

            return helpersDictionary[helperEnum] as T;
        }

        public void RemoveHelpers()
        {
            foreach (var helper in helpersDictionary.Values)
            {
                Destroy(helper.gameObject);
            }

            helpersDictionary.Clear();
        }
    }
}
