using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    public MonoBehaviour[] Components;

    public void ActivateComponents()
    {
        foreach(MonoBehaviour component in Components)
        {
            component.enabled = true;
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
