using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUsageData
{
    public float UsagePerSecond;
    public float? Duration;

    public PowerUsageData(float usagePerSecond)
    {
        UsagePerSecond = usagePerSecond;
    }

    public PowerUsageData(float usagePerSecond, float duration)
    {
        UsagePerSecond = usagePerSecond;
        Duration = duration;
    }
}
