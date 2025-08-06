using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalManager : MonoBehaviour
{
    //for spawning signals

    public Canvas Canvas; //needs to be parented
    public RectTransform ImageRect;
    public GameObject SignalObjectPrefab;
    public List<ClickableUI> Signals;

    public float BaseSpawnTime = 15f;
    public float TimeUntilSpawn;
    public float TimeIncreasePerSignal;

    private void Update()
    {
        List<ClickableUI> signalsToRemove = new();

        foreach(ClickableUI signal in Signals)
        {
            if(signal == null)
            {
                signalsToRemove.Add(signal);
            }
        }

        foreach(ClickableUI signal in signalsToRemove)
        {
            Signals.Remove(signal);
        }

        TimeUntilSpawn -= Time.deltaTime;

        if(TimeUntilSpawn <= 0)
        {
            TimeUntilSpawn = RecountSpawnTime();
            SpawnSignal();
        }
    }

    void SpawnSignal()
    {
        Debug.Log("Spawning signal");

        Rect localRect = ImageRect.rect;

        //offset to prevent the images to go out of screen bounds
        float offset = 5f;

        float randomX = Random.Range(localRect.xMin + offset, localRect.xMax - offset);
        float randomY = Random.Range(localRect.yMin + offset, localRect.yMax - offset);

        Vector3 randomPosition = new Vector3(randomX, randomY, 0.01f);

        GameObject signalObject = Instantiate(SignalObjectPrefab, Canvas.transform);

        //converting local position into world position
        signalObject.transform.position = signalObject.transform.TransformPoint(randomPosition);

        Signals.Add(signalObject.GetComponent<ClickableUI>());
    }

    float RecountSpawnTime()
    {
        SignalLog[] logs = SignalDisplay.Instance.Logs;
        float newTime = BaseSpawnTime;

        foreach(SignalLog log in logs)
        {
            if (log.AssignedData != null)
                newTime += TimeIncreasePerSignal;
        }

        return newTime;
    }
}
