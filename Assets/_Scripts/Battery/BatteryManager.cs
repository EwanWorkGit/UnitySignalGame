using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BatteryManager : MonoBehaviour
{
    public static BatteryManager Instance;

    public List<PowerUsageData> PowerUsers = new();
    public TMP_Text PowerText;

    public float BasePowerUsage = -5f; //base should be an increase in power
    public float TotalPowerUsage;

    public float PowerStored = 100f;
    public float MaxPowerStorage = 100f;

    public float EnableThreshold = 50f;

    public bool OnRechargePeriod = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PowerStored = MaxPowerStorage;
    }

    private void Update()
    {
        //calcualates power usage based on number of users and their usage
        CalculatePowerPerSecond();

        //decreases power by powerusage per second
        PowerStored -= Time.deltaTime * TotalPowerUsage;

        //clamp power stored between 0 and max power
        PowerStored = Mathf.Clamp(PowerStored, 0, MaxPowerStorage);

        //start recharge period
        if(PowerStored <= 0 && !OnRechargePeriod)
        {
            OnRechargePeriod = true;
        }

        //end recharge period
        if(PowerStored >= EnableThreshold && OnRechargePeriod)
        {
            OnRechargePeriod = false;
        }

        if(PowerText != null)
            PowerText.text = PowerStored.ToString("F0") + "%";
        else
        {
            Debug.Log("Power Text Is Null!");
        }
    }

    void CalculatePowerPerSecond()
    {
        TotalPowerUsage = 0;

        List<PowerUsageData> toBeRemoved = new();
        foreach(PowerUsageData powerUsage in PowerUsers)
        {
            //decrease burst's duration
            if(powerUsage.Duration.HasValue)
            {
                powerUsage.Duration -= Time.deltaTime;

                //check if burst's duration has run out
                if (powerUsage.Duration.Value <= 0)
                {
                    toBeRemoved.Add(powerUsage);
                }
            }

            TotalPowerUsage += powerUsage.UsagePerSecond;
        }

        //removes data
        foreach(PowerUsageData powerUsage in toBeRemoved)
        {
            RemovePowerSource(powerUsage);
        }

        TotalPowerUsage += BasePowerUsage;
    }

    public void AddPowerSource(PowerUsageData usageData)
    {
        PowerUsers.Add(usageData);
    }

    public void RemovePowerSource(PowerUsageData usageData)
    {
        PowerUsers.Remove(usageData);
    }
}
