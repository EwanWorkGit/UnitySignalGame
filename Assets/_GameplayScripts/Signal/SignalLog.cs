using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignalLog : MonoBehaviour
{
    //data assigned by display
    public SignalData AssignedData = null;
    //assigned in editor
    public TMP_Text NameText, SizeText, StabilityText;
    public Image TransferFill;
    public Image ImageSelf;
    //assigned by display
    public float MaxTransferTime;

    private void Start()
    {
        ImageSelf = GetComponent<Image>();

        if (ImageSelf == null)
            Debug.Log("Log image is null!");
    }

    private void Update()
    {
        if(AssignedData != null)
        {
            //text assignment for UI
            NameText.text = AssignedData.Name;
            SizeText.text = AssignedData.Size.ToString("F2");
            StabilityText.text = (AssignedData.Stability * 100f).ToString("F0");

            //decay over time and transfer time decrease
            AssignedData.Stability -= Time.deltaTime * AssignedData.DecayRate;
            AssignedData.Stability = Mathf.Clamp01(AssignedData.Stability);
            AssignedData.TimeUntilTransfer -= Time.deltaTime;
            TransferFill.fillAmount = 1f - (AssignedData.TimeUntilTransfer / MaxTransferTime);

            //if signal decays away
            if(AssignedData.Stability <= 0)
            {
                ClearData();
            }

            //if signal transfers
            if(AssignedData != null && AssignedData.TimeUntilTransfer <= 0)
            {
                //ask signaldisplay to transfer the data
                Debug.Log("Transfering data");
                SignalDisplay.Instance.TransferSignal(AssignedData);
                ClearData();
            }
        }
    }

    void ClearData()
    {
        NameText.text = "";
        SizeText.text = "";
        StabilityText.text = "";
        TransferFill.fillAmount = 0;

        AssignedData = null;
    }
}
