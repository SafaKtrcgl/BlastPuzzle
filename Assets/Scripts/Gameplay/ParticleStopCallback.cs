using System;
using UnityEngine;

public class ParticleStopCallback : MonoBehaviour
{
    public Action OnParticleStopAction;

    private void OnParticleSystemStopped()
    {
        OnParticleStopAction?.Invoke();
    }
}
