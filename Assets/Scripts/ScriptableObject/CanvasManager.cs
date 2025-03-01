using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CanvasManager", menuName = "Scriptable Objects/CanvasManager")]
public class CanvasManager : ScriptableObject
{
    [NonSerialized] List<Canvas> canvasList = new List<Canvas>();
    [NonSerialized] List<Canvas> prevOpenList = new List<Canvas>();

    public void AddCanvasToList (Canvas canvas)
    {
        canvasList.Add(canvas);
    }
    public void EnableCanvas(Canvas enabledCanvas)
    {
        prevOpenList.Clear();
        foreach (Canvas canvas in canvasList)
        {
            if (canvas != enabledCanvas)
            {
                if (canvas.enabled)
                    prevOpenList.Add(canvas);
                canvas.enabled = false;
            }
            else
            { 
                canvas.enabled = true;
            }
        }
    }

    public void DisableCanvas(Canvas disabledCanvas)
    {
        foreach (Canvas canvas in canvasList)
        {
            if (canvas == disabledCanvas)
                canvas.enabled = false;

            else if(prevOpenList.Contains(canvas))
                canvas.enabled = true;
        }
    }
}
