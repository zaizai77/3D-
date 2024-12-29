using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<DayTime,int,int, Season> GameHourEvent;

    public static void CallGameHourEvent(DayTime dayTime,int day,int month,Season season)
    {
        GameHourEvent?.Invoke(dayTime,day,month, season);
    }

    public static event Action<int, int,Season> GameDayEvent;

    public static void CallGameDayEvent(int day,int month,Season season)
    {
        GameDayEvent?.Invoke(day, month,season);
    }

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnLoadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadEvent;

    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadEvent?.Invoke();
    }
}
    
