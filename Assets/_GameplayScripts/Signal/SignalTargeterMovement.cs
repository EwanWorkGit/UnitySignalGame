using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalTargeterMovement : MonoBehaviour
{
    public Canvas Canvas;
    public RectTransform TargeterRect;
    public Image TargeterImage;
    public Camera Camera;

    public float TargeterSpeed = 0.5f;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.transform.position.z - Canvas.transform.position.z);
        Vector3 worldPosition = Camera.ScreenToWorldPoint(mousePosition);
        TargeterRect.position = Vector3.MoveTowards(TargeterRect.position, worldPosition, TargeterSpeed * Time.deltaTime);
    }
}
