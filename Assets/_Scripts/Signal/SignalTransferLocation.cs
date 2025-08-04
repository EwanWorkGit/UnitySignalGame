using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTransferLocation : MonoBehaviour
{
    public float TransferTime = 20f;
    
    List<SignalData> StoredSignals;

    public void StoreSignal(SignalData data)
    {
        StoredSignals.Add(data);
    }

    public void SellSignals()
    {
        //selling the signals
    }
}
