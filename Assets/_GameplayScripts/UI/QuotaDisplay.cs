using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuotaDisplay : MonoBehaviour
{
    public SignalManager SignalManager;
    public SignalTransferLocation Storage;
    public TMP_Text QuotaText, CurrentlyStoredText;

    private void Start()
    {
        SignalManager = SignalManager.Instance;
    }

    private void Update()
    {
        int quota = SignalManager.SignalQuota;
        int currentlyStored = Storage.StoredSignalsCount;

        QuotaText.text = quota.ToString();
        CurrentlyStoredText.text = currentlyStored.ToString();
    }
}
