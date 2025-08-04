using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignalDisplay : MonoBehaviour
{
    public static SignalDisplay Instance;

    public SignalTransferLocation TransferLocation;
    public SignalLog[] Logs;

    private void Awake()
    {
        Instance = this;
    }

    public void AddSignal(SignalData data)
    {
        //assign data to the first log that does not contain data
        foreach(SignalLog log in Logs)
        {
            if(log.AssignedData == null)
            {
                log.AssignedData = data;
                log.MaxTransferTime = TransferLocation.TransferTime;
                log.AssignedData.TimeUntilTransfer = TransferLocation.TransferTime;
                break;
            }
        }
    }

    public void TransferSignal(SignalData data)
    {
        if (data != null)
        {
            TransferLocation.StoreSignal(data);
        }
        else
            Debug.Log("Data is null");
    }
}
