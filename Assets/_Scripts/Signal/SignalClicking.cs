using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalClicking : MonoBehaviour
{
    public float DownloadProgress = 0f, TimeUntilFinished = 3f;

    public GameObject BarRoot;
    public Image DownloadBar;
    public Camera Camera;

    private void Start()
    {
        BarRoot.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit);
        if(hit.collider != null)
        {
            if(Input.GetMouseButton(0))
            {
                if (hit.collider.gameObject.TryGetComponent(out ClickableUI Signal))
                {
                    Vector3 offset = new Vector3(0f, 0.2f);

                    BarRoot.SetActive(true);
                    BarRoot.GetComponent<RectTransform>().position = hit.point + offset;
                    DownloadProgress += Time.deltaTime / TimeUntilFinished;
                    if(DownloadProgress >= 1f)
                    {
                        Signal.SendDataToSignalManager();
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) || hit.collider == null)
            {
                DownloadProgress = 0f;
                BarRoot.SetActive(false);
            }
        }

        DownloadProgress = Mathf.Clamp01(DownloadProgress);

        if(DownloadBar != null)
        {
            DownloadBar.fillAmount = DownloadProgress;
        }
    }
}
