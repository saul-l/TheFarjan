using System;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static float gameTime;
    [SerializeField] private float currentGameDays;
    [SerializeField] private float currentGameTime;
    [SerializeField] private float currentGameHours;
    [SerializeField] private float currentMinutes;

    private float prevGameDays = -1;
    public float gameSpeed = 1f;
    public const float dayInSeconds = 1440f;
    private float prevTime;
    public delegate void DayChange();
    public static DayChange dayChange;
    public static string dayString;
    private string hoursString;
    private string minutesString;
    // 1 game minute = 1 second, 1 game day = 1440 seconds, 1 game hour = 60 seconds
    void Start()
    {
        prevTime = Time.time;
    }

    void Update()
    {
        gameTime += (Time.time-prevTime)*gameSpeed;
        prevTime = Time.time;
        currentGameTime = gameTime;
        currentGameDays = Mathf.Floor(gameTime / dayInSeconds);
        currentGameHours = Mathf.Floor((gameTime/60)%24);
        currentMinutes = Mathf.Floor(gameTime % 60);

        if (currentGameDays > prevGameDays)
            dayChange?.Invoke();

        prevGameDays = currentGameDays;


        if (currentGameHours < 10)
            hoursString = "0" + currentGameHours;
        else
            hoursString = currentGameHours.ToString();

        if (currentMinutes < 10)
            minutesString = "0" + currentMinutes;
        else
            minutesString = currentMinutes.ToString();

        dayString = "Day: " + (currentGameDays + 1.0) + " Time: " + hoursString + ":" + minutesString;
    }
}
