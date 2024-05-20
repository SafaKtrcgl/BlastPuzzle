using Singleton;
using UnityEngine;

[CreateAssetMenu]
public class HelperResource : ScriptableObject
{
    [SerializeField] private HelpersEnum helperEnum;
    [SerializeField] private HelperBase helperClass;

    public HelpersEnum HelpersEnum => helperEnum;
    public HelperBase HelperClass => helperClass;
}
