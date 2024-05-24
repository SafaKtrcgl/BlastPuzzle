using UnityEngine;

namespace Context
{
    public abstract class ContextBase : MonoBehaviour
    {
        [SerializeField] private Canvas dynamicCanvas;
        public Canvas DynamicCanvas => dynamicCanvas;
    }
}