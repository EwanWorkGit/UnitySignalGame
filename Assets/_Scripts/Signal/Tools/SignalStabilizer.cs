using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalStabilizer : MonoBehaviour
{
    public SignalDisplay Display;
    public SignalLog CurrentLog;

    public float StabilizeDecayRate = 0.04f; //mode 1, will slow decay down --- Less power
    public float DrasticStabilizeRate = 0.4f; //mode 2, large burst of stability --- A lot of power 

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

        //stabilizer, decreases decay.
        if (IsStabilizing)
        {
            if(CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = StabilizeDecayRate;
            }
        }
        else
        {
            if (CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = 0;
            }
        }

        //emergency stabilizer, increases stability drastically at the cost of power
        if(CurrentLog != null && CurrentLog.AssignedData != null)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                CurrentLog.AssignedData.Stability += DrasticStabilizeRate;
            }
        }
    }
}
