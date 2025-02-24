using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLog", menuName = "Scriptable Objects/GameLog")]
public class GameLog : ScriptableObject
{
    public List<string> log;

    public void WriteLog(string logString)
    {
        log.Append(logString);
    }
}
