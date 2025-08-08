using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpInput : MonoBehaviour
{
    private void Update()
    {
        Ray ray = CameraManager.Instance.MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo);

        if(hitInfo.collider != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if (hitInfo.transform.TryGetComponent(out StartUpObject startUp))
                {
                    if (PhaseManager.Instance.CurrentPhase == Phase.Startup)
                    {
                        if(!startUp.TurnedOn)
                        {
                            startUp.TurnOn();
                        }
                    }
                    else if(PhaseManager.Instance.CurrentPhase == Phase.Shutdown && !ScreenClicking.Instance.InsideScreen)
                    {
                        if (startUp.TurnedOn)
                        {
                            startUp.TurnOff();

                            //check so that signal manager is turned off
                            if(hitInfo.transform.root.TryGetComponent<SignalManager>(out _))
                            {
                                if (SignalManager.Instance.IsActive)
                                    SignalManager.Instance.IsActive = false;
                            }
                        }
                    }
                }
                    
            }
        }
    }
}
