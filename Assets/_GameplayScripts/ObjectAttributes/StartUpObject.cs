using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUpObject : MonoBehaviour
{
    public Image ScreenImage = null;

    public bool TurnedOn = false;

    public void TurnOn()
    {
        TurnedOn = true;
        if(ScreenImage != null)
        {
            ScreenImage.enabled = true;
        }
    }

    public void TurnOff()
    {
        TurnedOn = false;
        if(ScreenImage != null)
        {
            ScreenImage.enabled = false;
        }
    }
}
