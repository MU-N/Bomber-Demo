using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeValue = 90;
    [SerializeField] TMP_Text timer;

    float mintes;
    float seconds;
    void Update()
    {
        if (timeValue > 0)
            timeValue -= Time.deltaTime;
        else
            timeValue += timeValue;
        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        else if(timeToDisplay>0)
        {
            timeToDisplay += 1;
        }
        mintes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timer.text = string.Format("{0:00}:{1:00}",mintes,seconds);
    }
}
