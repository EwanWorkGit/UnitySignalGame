using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTransferLocation : MonoBehaviour
{
    public float TransferTime = 20f;
    
    List<SignalData> StoredSignals = new();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            int count = StoredSignals.Count;
            Debug.Log("Items in storage: " + count);
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
        //action needed here to call for removal of data    
    }

    public void SellSignals()
    {
        //selling the signals
    }
}
