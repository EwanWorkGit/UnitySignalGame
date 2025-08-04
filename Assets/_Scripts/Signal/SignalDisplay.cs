using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SignalDisplay : MonoBehaviour
{
    public static SignalDisplay Instance;

    public Image[] Logs;

    List<SignalData> Signals = new List<SignalData>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddSignal(SignalData data)
    {
        //appropriate UI alongside adding into signals list
        Debug.Log("Display recieved a signal");
        Signals.Add(data);
    }

    private void Update()
    {
        foreach(SignalData signal in Signals)
        {
            Debug.Log(signal.DecayRate);
            signal.Stability -= signal.DecayRate * Time.deltaTime;
            signal.Stability = Mathf.Clamp01(signal.Stability);
        }


        for(int index = 0; index < Logs.Length; index++)
        {
            if(index < Signals.Count)
            {
                GameObject currentLog = Logs[index].gameObject;

                TMP_Text[] allTexts = currentLog.GetComponentsInChildren<TMP_Text>();
                List<TMP_Text> valueTexts = new();

                foreach(TMP_Text text in allTexts)
                {
                    if(text.transform.CompareTag("Value Text"))
                    {
                        valueTexts.Add(text);
                    }
                }

                if (Signals[index].Stability <= 0)
                {
                    foreach(TMP_Text text in valueTexts)
                    {
                        text.text = "";
                    }

                    Signals.Remove(Signals[index]);
                    break;
                }
                
                valueTexts[0].text = Signals[index].Name + (index + 1).ToString();
                valueTexts[1].text = (Signals[index].Stability * 100f).ToString("F0");
                valueTexts[2].text = Signals[index].Size.ToString("F2");
                

            }
        }
    }
}
