using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTransferLocation : MonoBehaviour
{
    public float TransferTime = 20f;
    public int StoredSignalsCount; //for phase manager

    List<SignalData> StoredSignals = new();

    private void Update()
    {
        StoredSignalsCount = StoredSignals.Count;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Items in storage: " + StoredSignalsCount);
        }
    }

    public void StoreSignal(SignalData data)
    {
        if (data != null)
        {
            StoredSignals.Add(data);
        }
        else
            Debug.Log("Data is null");   
    }

    public void SellSignals()
    {
        //selling the signals
    }
}
