using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalStabilizer : MonoBehaviour
{
    public SignalDisplay Display;
    public SignalLog CurrentLog;

    public PowerUsageData StableData, BurstData;

    public float StabilizeDecayRate = 0.04f, StabilizeUsageRate = 6f; //mode 1, will slow decay down --- Less power
    public float DrasticStabilizeRate = 0.4f, DrasticStabilizeUsageRate = 20f, DrasticStabilizeUsageDuration = 1f; //mode 2, large burst of stability --- A lot of power 

    public bool IsStabilizing = false;

    BatteryManager PowerManager;

    private void Start()
    {
        PowerManager = BatteryManager.Instance;

        //add powerusage for prolonged things: aka delay.
        StableData = new(StabilizeUsageRate);
        //add powerusage for bursts: aka drastic stabilize.
        BurstData = new(DrasticStabilizeUsageRate, DrasticStabilizeUsageDuration);
    }

    private void Update()
    {
        //getting current log
        if (Display != null && Display.enabled)
        {
            CurrentLog = Display.CurrentLog;
        }

        //turn on stabilizing bool
        if (CurrentLog != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //if false then true
                IsStabilizing = !IsStabilizing;
            }
        }

        //stabilizer, decreases decay.
        if (IsStabilizing)
        {
            if (CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = StabilizeDecayRate;

                //adding powerusage
                if (PowerManager != null)
                {
                    if (!PowerManager.PowerUsers.Contains(StableData))
                        PowerManager.AddPowerSource(StableData);
                    else
                    {
                        Debug.Log("StableData already contained inside PowerUsers");
                    }
                }
            }
        }
        else
        {
            if (CurrentLog != null && CurrentLog.AssignedData != null)
            {
                CurrentLog.AssignedData.DecayOffset = 0;
                RemovePowerUsage();
            }
        }

        //emergency stabilizer, increases stability drastically at the cost of power
        if (CurrentLog != null && CurrentLog.AssignedData != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CurrentLog.AssignedData.Stability += DrasticStabilizeRate;

                //adding powerusage
                if (PowerManager != null)
                {
                    //should not be removed in stablizer, this gets handled by the manager
                    PowerManager.AddPowerSource(BurstData);
                    BurstData.Duration = DrasticStabilizeUsageDuration;
                }
            }
        }
        
        //turn of stabilization if current log is null
        if(CurrentLog == null && IsStabilizing)
        {
            IsStabilizing = false;
            RemovePowerUsage();
        }

        //if(batteryIsRecharging)

        //call a method that shuts of stabilization and removes power usage until battery is back up and running
    }

    void RemovePowerUsage()
    {
        //removing power usage
        if (PowerManager != null)
        {
            if (PowerManager.PowerUsers.Contains(StableData))
                PowerManager.RemovePowerSource(StableData);
        }
    }
    
    void AddPowerUsage()
    {

    }
}
