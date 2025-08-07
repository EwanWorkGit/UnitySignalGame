using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClicking : MonoBehaviour
{
    public ScreenToggle CurrentScreen = null;

    public bool InsideScreen = false;

    int MouseButtonIndex = 1;

    void Update()
    {
        //make input need maincamera and exit not need it

        Ray ray = CameraManager.Instance.MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo);
            
        if (hitInfo.collider != null)
        {
            //entering a screen
            if (Input.GetMouseButtonDown(MouseButtonIndex) && !InsideScreen)
            {
                TargetScreen(hitInfo);
                ActivateScreenComponents(hitInfo);
                InsideScreen = true;

                return;
            }
        }

        if (Input.GetMouseButtonDown(MouseButtonIndex) && InsideScreen)
        {
            DefaultScreen();
            InsideScreen = false;
        }
    }

    void TargetScreen(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.TryGetComponent(out Targetable targetable))
        {
            Camera cameraToSwitchTo = targetable.GetCamera();
            CameraManager.Instance.SwitchToCamera(cameraToSwitchTo);

            //so that player cant move or rotate when looking at a targetable object
            CameraManager.Instance.IsTargeting = true;
        }
    }

    void ActivateScreenComponents(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.TryGetComponent(out ScreenToggle screen))
        {
            screen.ActivateComponents();
            CurrentScreen = screen;
        }
    }

    //untargets and deactivates components
    void DefaultScreen()
    {
        if (CurrentScreen != null)
        {
            CurrentScreen.DisableComponents();
        }

        CameraManager.Instance.GoToMainCamera();
        CameraManager.Instance.IsTargeting = false;
    }
}
