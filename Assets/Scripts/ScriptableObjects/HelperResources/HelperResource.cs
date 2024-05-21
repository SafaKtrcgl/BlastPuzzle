using Singleton;
using UnityEngine;

[CreateAssetMenu]
public class HelperResource : ScriptableObject
{
    [SerializeField] private HelperEnum helperEnum;
    [SerializeField] private HelperBase helperClass;

    public HelperEnum HelpersEnum => helperEnum;
    public HelperBase HelperClass => helperClass;
}
