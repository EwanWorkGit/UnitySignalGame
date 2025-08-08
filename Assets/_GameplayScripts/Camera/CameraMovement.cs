using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float RotationSpeed = 5f;

    float RotationX, RotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //INPUTS AND MOVEMENT

        if(!CameraManager.Instance.IsTargeting)
        {
            float sensitivity = RotationSpeed * Time.deltaTime;
            Vector2 inputs = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensitivity;

            RotationX -= inputs.y;
            RotationY += inputs.x;

            RotationX = Mathf.Clamp(RotationX, -90f, 90f);

            transform.localRotation = Quaternion.Euler(RotationX, 0, 0);
            transform.root.rotation = Quaternion.Euler(0, RotationY, 0);
        }
    }
}
