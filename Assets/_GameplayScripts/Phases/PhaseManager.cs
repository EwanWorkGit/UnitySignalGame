using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Startup, Gameplay, Shutdown }

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager Instance;

    public StartUpObject[] StartUpObjects;
    public SignalTransferLocation Storage;

    public Phase CurrentPhase = Phase.Startup;

    public void Awake()
    {
        Instance = this;

        if (Storage == null)
            Debug.Log("Storage is not assigned!");
    }

    private void Update()
    {
        if(CurrentPhase == Phase.Startup)
        {
            foreach (StartUpObject startUp in StartUpObjects)
            {
                if (!startUp.TurnedOn)
                    return;
            }

                CurrentPhase = Phase.Gameplay;
        }

        if(CurrentPhase == Phase.Gameplay)
        {
            if(Storage.StoredSignalsCount >= SignalManager.Instance.SignalQuota)
            {
                CurrentPhase = Phase.Shutdown;
            }
        }
    }
}
