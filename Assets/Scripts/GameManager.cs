using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Character_Status playerStatus;

    List<IEndGameObserve> endGameObserves = new List<IEndGameObserve>();

    public void RigisterPlayer(Character_Status player)
    {
        playerStatus = player;
    }

    public void AddObserver(IEndGameObserve observe)
    {
        endGameObserves.Add(observe);
    }

    public void RemoveObesrver(IEndGameObserve observe)
    {
        endGameObserves.Remove(observe);
    }

    public void NotifyObserVers()
    {
        foreach (var observer in endGameObserves)
        {
            observer.EndNotify();
        }
    }
}
