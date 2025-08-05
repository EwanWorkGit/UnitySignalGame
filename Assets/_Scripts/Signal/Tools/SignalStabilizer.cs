using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalStabilizer : MonoBehaviour
{
    public SignalDisplay Display;
    public SignalLog CurrentLog;

    public float StabilizeDecayRate; //mode 1 will slow decay down --- Less power
    public bool IsStabilizing = false;

    private void Update()
    {
        //getting current log
        if(Display != null && Display.enabled)
        {
            CurrentLog = Display.CurrentLog;
        }
        else
        {
            CurrentLog = null;
        }

        //turn on stabilizing bool
        if(CurrentLog != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                //if false then true
                IsStabilizing = !IsStabilizing;
            }
        }

        
        if(IsStabilizing)
        {
            if(CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = StabilizeDecayRate;
                Debug.Log(CurrentLog.AssignedData.DecayRate);
            }
        }
        else
        {
            if (CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = 0;
                Debug.Log(CurrentLog.AssignedData.DecayRate);
            }
        }
        
    }
}
