using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalStabilizer : MonoBehaviour
{
    public SignalDisplay Display;
    public SignalLog CurrentLog;
    public ScreenClicking ClickHandler;
    public ScreenToggle Screen;

    public PowerUsageData StableData, BurstData;

    public float StabilizeDecayRate = 0.04f, StabilizeUsageRate = 6f; //mode 1, will slow decay down --- Less power
    public float DrasticStabilizeRate = 0.4f, DrasticStabilizeUsageRate = 20f, DrasticStabilizeUsageDuration = 1f; //mode 2, large burst of stability --- A lot of power 

    public bool IsStabilizing = false;

    BatteryManager PowerManager;

    private void Start()
    {
        PowerManager = BatteryManager.Instance;

        //null checks for managers / handlers
        if (PowerManager == null)
            Debug.Log("Power manager is null!");
        if (Display == null)
            Debug.Log("Display is not assigned!");
        if (ClickHandler == null)
            Debug.Log("ClickHandler is not assigned!");
        if (Screen == null)
            Debug.Log("ScreenToggle is not assigned!");

        //add powerusage for prolonged things: aka delay.
        StableData = new(StabilizeUsageRate);
        //add powerusage for bursts: aka drastic stabilize.
        BurstData = new(DrasticStabilizeUsageRate, DrasticStabilizeUsageDuration);
    }

    private void Update()
    {
        bool currentLogExistsAndHasData = CurrentLog != null && CurrentLog.AssignedData != null;
        bool insideCurrentScreen = ClickHandler.InsideScreen && ClickHandler.CurrentScreen == Screen;

        //getting current log, is above recharge because signal display needs the info to work
        if (Display != null && Display.enabled)
        {
            CurrentLog = Display.CurrentLog;
        }

        //shuts of stabilization and power usage, no more code runs after this
        if (PowerManager.OnRechargePeriod)
        {
            IsStabilizing = false;
            RemovePowerUsage();
            UnStablizeAllLogs();

            return;
        }

        

        //toggle stabilizing bool
        if (currentLogExistsAndHasData)
        {
            if (Input.GetMouseButtonDown(0) && insideCurrentScreen)
            {
                IsStabilizing = !IsStabilizing;
            }
        }

        //stabilizer, decreases decay.
        if (IsStabilizing)
        {
            if (currentLogExistsAndHasData)
            {
                CurrentLog.AssignedData.DecayOffset = StabilizeDecayRate;

                if (!PowerManager.PowerUsers.Contains(StableData))
                        PowerManager.AddPowerSource(StableData);
            }
            else
            {
                IsStabilizing = false;
                RemovePowerUsage();
            }
        }
        else
        {
            //if we're not stabilizing its safe to loop through all logs to make sure all of them are not stabilized
            UnStablizeAllLogs();
            RemovePowerUsage();
        }

        //emergency stabilizer, increases stability drastically at the cost of power
        if (currentLogExistsAndHasData)
        {
            if (Input.GetKeyDown(KeyCode.E) && insideCurrentScreen)
            {
                CurrentLog.AssignedData.Stability += DrasticStabilizeRate;

                //should not be removed in stablizer, this gets handled by the manager
                PowerManager.AddPowerSource(BurstData);
                BurstData.Duration = DrasticStabilizeUsageDuration;
            }
        }
    }

    void RemovePowerUsage()
    {
        if (PowerManager.PowerUsers.Contains(StableData))
            PowerManager.RemovePowerSource(StableData);
    }
    void UnStablizeAllLogs()
    {
        foreach (SignalLog log in Display.Logs)
        {
            if (log != null && log.AssignedData != null)
            {
                log.AssignedData.DecayOffset = 0;
            }
        }
    }
}
