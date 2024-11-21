using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeUI : MonoBehaviour
{
    int minutes;
    int seconds;
    int milliseconds;
    private TextMeshProUGUI timeTxt;

    private void Start()
    {
        timeTxt = GetComponent<TextMeshProUGUI>();
    }


    private void Update()
    {
        minutes = Mathf.FloorToInt(GameManager.Instance.currentTIme / 60f);
        seconds = Mathf.FloorToInt(GameManager.Instance.currentTIme % 60f);
        milliseconds = Mathf.FloorToInt((GameManager.Instance.currentTIme % 1f) * 1000f);

        timeTxt.text = string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
    }
}
