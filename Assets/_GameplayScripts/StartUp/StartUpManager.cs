using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpManager : MonoBehaviour
{
    public StartUpObject[] StartUpObjects;

    public bool StartUpComplete = false;

    private void Update()
    {
        if(!StartUpComplete)
        {
            foreach (StartUpObject startUp in StartUpObjects)
            {
                if (!startUp.TurnedOn)
                    return;
            }

            StartUpComplete = true;
        }
    }
}
