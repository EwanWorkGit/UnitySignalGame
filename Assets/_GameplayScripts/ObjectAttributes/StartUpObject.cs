using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUpObject : MonoBehaviour
{
    public Image ScreenImage = null;

    public bool TurnedOn = false;

    private void Start()
    {
        TurnOff();
    }

    public void TurnOn()
    {
        TurnedOn = true;
        if(ScreenImage != null)
        {
            ScreenImage.gameObject.SetActive(true);
        }
    }

    //only happens at start, not by player
    public void TurnOff()
    {
        TurnedOn = false;
        if(ScreenImage != null)
        {
            ScreenImage.gameObject.SetActive(false);
        }
    }
}
