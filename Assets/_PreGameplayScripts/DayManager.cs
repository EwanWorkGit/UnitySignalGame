using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    public TMP_Text DayText;
    public TMP_Text SubText;

    string[] SubTexts =
    {
        "Corporate: Welcome!", "Corporate: day2", "Corporate: day3", "Corporate: day4", "Corporate: day5"
    };

    int CurrentDay = 1;

    private void Start()
    {
        SetDay(CurrentDay);
    }

    public void SetDay(int day)
    {
        if(day <= 5 && day > 0)
        {
            CurrentDay = day;
            DayText.text = $"Day {CurrentDay}";
            SubText.text = SubTexts[CurrentDay - 1]; //cause we're indexing
        }
        else
        {
            Debug.Log("Day got set out of bounds!");
        }
    }
}
