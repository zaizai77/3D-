using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private int gameHour, gameDay, gameMonth, gameYear;
    private Season gameSeason = Season.春天;
    private int monthInSeason = 3;  //三个月一个季节
    private DayTime dayTime = DayTime.上午;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ChangeHour();
        }
    }

    void ChangeHour()
    {
        gameHour += 4;
        if (gameHour > 22)
        {
            gameHour = 6;
            gameDay += 1;
            if (gameDay > 30)
            {
                gameDay = 1;
                gameMonth += 1;
                if (gameMonth > 12)
                {
                    gameMonth = 1;
                    gameYear += 1;
                }
            }
            EventHandler.CallGameDayEvent(gameDay, gameMonth, gameSeason);
        }
        gameSeason = (Season)((gameMonth - 1) / 3);
        EventHandler.CallGameHourEvent((DayTime)gameHour, gameDay, gameMonth, gameSeason);
    }

    private void Awake()
    {
        gameHour = 6;
        gameDay = 29;
        gameMonth = 12;
        gameSeason = Season.冬天;
        gameYear = 2024;
    }
}
