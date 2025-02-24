using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{

    // Editor logging
#if UNITY_EDITOR
    GameLog gameLog;
    string gameLogLocation = "Scripts/ScriptableObject/GameLog.cs";

    void LogData(string data)
    {
        if (gameLog == null)
            gameLog = Resources.Load<GameLog>(gameLogLocation);

        gameLog.WriteLog(data);
    }
#endif

    private List<GameEventListener> listenerList = new List<GameEventListener>();
    public void Raise()
    {
        for (int i = listenerList.Count - 1; i >= 0; i--)
        {
            listenerList[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listenerList.Add(listener);
    }

    public void UnRegisterListener(GameEventListener listener)
    {
        listenerList.Remove(listener);
    }
}

