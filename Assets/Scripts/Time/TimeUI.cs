using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public TextMeshProUGUI dataText;
    public TextMeshProUGUI timeText;
    public Sprite[] seaonSprites;
    public Material[] skyboxes;
    private Image seaonImage;

    private void Awake()
    {
        EventHandler.GameHourEvent += OnGameHourEvent;
        EventHandler.GameDayEvent += OnGameDataEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameHourEvent -= OnGameHourEvent;
        EventHandler.GameDayEvent -= OnGameDataEvent;
    }

    private void OnGameHourEvent(DayTime dayTime, int day,int month, Season season)
    {
        timeText.text = ((int)dayTime).ToString("00") + ":" + "00";
        RenderSettings.skybox = skyboxes[((int)dayTime - 3) / 4];
    }

    private void OnGameDataEvent(int day, int month,Season season)
    {
        dataText.text = month.ToString("00") + "." + day.ToString("00");
        //seaonImage.sprite = seaonSprites[(int)season];
    }

}
