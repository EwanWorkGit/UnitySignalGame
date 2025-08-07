using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClicking : MonoBehaviour
{
    public CameraMovement CamMovement;
    public PlayerMovement PlayerMovement;

    ScreenToggle CurrentScreen = null;

    void Update()
    {
        if(CameraManager.Instance.MainCamera.enabled)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
            Physics.Raycast(ray.origin, ray.direction, out RaycastHit hitInfo);
            if (hitInfo.collider != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hitInfo.transform.gameObject.TryGetComponent(out Targetable targetable))
                    {
                        Camera cameraToSwitchTo = targetable.GetCamera();
                        CameraManager.Instance.SwitchToCamera(cameraToSwitchTo);

                        //so that player cant move or rotate when looking at a targetable object
                        CameraManager.Instance.IsTargeting = true;
                    }

                    if(hitInfo.transform.gameObject.TryGetComponent(out ScreenToggle screen))
                    {
                        screen.ActivateComponents();
                        CurrentScreen = screen;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if(CurrentScreen != null)
            {
                CurrentScreen.DisableComponents();
            }

            CameraManager.Instance.GoToMainCamera();
            CameraManager.Instance.IsTargeting = false;
        }
    }
}
