using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    public bool IsTargeting = false;

    public List<Camera> Cameras = new();
    public Camera MainCamera;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        //CURSOR LOCKSTATE
        Cursor.lockState = Instance.IsTargeting ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void GoToMainCamera()
    {
        foreach(Camera cam in Cameras)
        {
            if(cam == MainCamera)
            {
                cam.enabled = true;
            }
            else
            {
                cam.enabled = false;
            }
        }
    }

    public void SwitchToCamera(Camera camera)
    {
        foreach(Camera cam in Cameras)
        {
            if(cam == camera)
            {
                cam.enabled = true;
            }
            else
            {
                cam.enabled = false;
            }
        }
    }

}
