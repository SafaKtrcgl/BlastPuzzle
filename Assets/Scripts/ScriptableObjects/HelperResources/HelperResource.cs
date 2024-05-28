using Enums;
using Helpers;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu]
    public class HelperResource : ScriptableObject
    {
        [SerializeField] private HelperEnum helperEnum;
        [SerializeField] private HelperBase helperClass;

        public HelperEnum HelpersEnum => helperEnum;
        public HelperBase HelperClass => helperClass;
    }
}
