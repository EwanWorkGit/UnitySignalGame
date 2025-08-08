using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    public MonoBehaviour[] Components;
    public MonoBehaviour[] Managers;

    public void ActivateComponents()
    {
        foreach(MonoBehaviour component in Components)
        {
            component.enabled = true;
        }

        foreach(MonoBehaviour component in Managers)
        {
            if (component is SignalManager)
            {
                SignalManager signalSpawner = component.GetComponent<SignalManager>();
                if (!signalSpawner.IsActive)
                    signalSpawner.IsActive = true;
            }
        }
    }
    public void DisableComponents()
    {
        foreach(MonoBehaviour component in Components)
        {
            component.enabled = false;
        }
    }
}
