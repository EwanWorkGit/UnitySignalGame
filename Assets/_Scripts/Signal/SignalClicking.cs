using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalClicking : MonoBehaviour
{
    public float DownloadProgress = 0f, TimeUntilFinished = 3f, HideDistance = 0.1f;

    public GameObject BarRoot;
    public Image DownloadBar;
    public Camera Camera;

    public ClickableUI[] Signals;
    public RectTransform TargeterRect;

    private void Start()
    {
        BarRoot.SetActive(false);
    }

    private void Update()
    {
        Signals = FindObjectsOfType<ClickableUI>();

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
                        Signal.SendDataToSignalDisplay();
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

        foreach(ClickableUI signal in Signals)
        {
            if(hit.collider != null)
            {
                Image signalVFX = signal.gameObject.GetComponent<Image>();
                //get distance between targeter and signal
                float distanceFromMouse = Vector3.Distance(signal.transform.position, TargeterRect.position);

                if(distanceFromMouse > HideDistance)
                {
                    signalVFX.enabled = false;
                }
                else
                {
                    signalVFX.enabled = true;
                }
            }
            
        }

        if(DownloadBar != null)
        {
            DownloadBar.fillAmount = DownloadProgress;
        }
    }
}
