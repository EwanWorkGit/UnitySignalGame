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
    public SignalLog CurrentLog = null;

    public Material Default, Stabilizing;

    public int Index = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");

        //increase index
        if (scroll < -0.01f)
        {
            if (Index + 1 < Logs.Length)
            {
                Index++;
            }
            else
            {
                Index = 0;
            }
        }
        //decrease index
        if (scroll > 0.01f)
        {
            if (Index - 1 >= 0)
            {
                Index--;
            }
            else
            {
                Index = Logs.Length - 1;
            }
        }

        //set current log
        CurrentLog = Logs[Index];

        //color current log
        foreach(SignalLog log in Logs)
        {
            if(log == CurrentLog)
            {
                log.gameObject.GetComponent<Image>().material = Stabilizing;
            }
            else
            {
                log.gameObject.GetComponent<Image>().material = Default;
            }
        }
    }

    public void AddSignal(SignalData data)
    {
        //assign data to the first log that does not contain data
        foreach(SignalLog log in Logs)
        {
            if(log.AssignedData == null)
            {
                log.AssignedData = data;
                log.MaxTransferTime = TransferLocation.TransferTime * log.AssignedData.Size;
                log.AssignedData.TimeUntilTransfer = log.MaxTransferTime;
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
