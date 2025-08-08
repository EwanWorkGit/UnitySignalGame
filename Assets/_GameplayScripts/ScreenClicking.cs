using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenClicking : MonoBehaviour
{
    public static ScreenClicking Instance;

    public ScreenToggle CurrentScreen = null;
    public PhaseManager PhaseManager;

    public bool InsideScreen = false;

    int MouseButtonIndex = 1;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PhaseManager = PhaseManager.Instance;

        if (PhaseManager == null)
            Debug.Log("PhaseManager is null");
    }

    void Update()
    {
        if(PhaseManager.CurrentPhase == Phase.Gameplay || PhaseManager.CurrentPhase == Phase.Shutdown && CurrentScreen.GetComponent<StartUpObject>().TurnedOn)
        {
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

                    //return incase you enter as to not also trigger an exit at the same time
                    return;
                }
            }

            if (Input.GetMouseButtonDown(MouseButtonIndex) && InsideScreen)
            {
                DefaultScreen();
                InsideScreen = false;
            }
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
