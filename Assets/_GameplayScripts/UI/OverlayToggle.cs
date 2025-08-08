using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayToggle : MonoBehaviour
{
    public GameObject Overlay;

    private void Update()
    {
        Overlay.SetActive(!CameraManager.Instance.IsTargeting);
    }
}
