using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableUI : MonoBehaviour
{ 
    public void SendDataToSignalDisplay()
    {
        Debug.Log("You pressed a signal");
        SignalData data = new SignalData();
        SignalDisplay.Instance.AddSignal(data);
        Destroy(gameObject);
    }
}
