using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalData
{
    public string Name = "Anom#";
    public float Stability = Random.Range(0.7f, 1f); //should be between one and some lower value. do not start to close to zero however
    public float Size = Random.Range(0.5f, 1f);
    public float TimeUntilTransfer;
    public float DecayOffset = 0;
    public float DecayRate => Mathf.Lerp(0.01f, 0.05f, Size) - DecayOffset;
}
