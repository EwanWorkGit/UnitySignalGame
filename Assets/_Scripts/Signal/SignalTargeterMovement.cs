using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTargeterMovement : MonoBehaviour
{
    public Canvas Canvas;
    public RectTransform Rect;
    public Camera Camera;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.transform.position.z - Canvas.transform.position.z);
        Vector3 worldPosition = Camera.ScreenToWorldPoint(mousePosition);
        Rect.position = worldPosition;
    }
}
