using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;

    public CharacterController Controller;

    private void LateUpdate()
    {
        if(!CameraManager.Instance.IsTargeting)
        {
            float sensitivity = Speed * Time.deltaTime;
            Vector2 inputs = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * sensitivity;

            Vector3 right = transform.right;
            Vector3 forward = transform.forward;
            right.y = 0;
            forward.y = 0;

            Vector3 movement = right * inputs.x + forward * inputs.y;

            Controller.Move(movement);
        }
    }
}
