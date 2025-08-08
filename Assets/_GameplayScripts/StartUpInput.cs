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
                if (hitInfo.transform.gameObject.TryGetComponent(out StartUpObject startUp))
                {
                    if (!startUp.TurnedOn)
                        startUp.TurnOn();
                }
            }
        }
    }
}
