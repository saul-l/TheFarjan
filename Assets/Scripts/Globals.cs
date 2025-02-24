using System;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static Globals instance;

    public float gameTime;
    public bool paused;
    [SerializeField] private float currentGameDays;
    [SerializeField] private float currentGameTime;
    [SerializeField] private float currentGameHours;
    [SerializeField] private float currentMinutes;

    public float gameSpeed = 10f;
    float prevGameSpeed = 1f;

    private float prevGameDays = -1;

    public const float dayInMinutes = 1440f;
    private float prevTime;
    public delegate void DayChangeDelegate();
    public DayChangeDelegate dayChangeDelegate;
    public string dayString;
    private string hoursString;
    private string minutesString;

    // should turn this into a singleton

    // 1 game minute = 1 second, 1 game day = 1440 seconds, 1 game hour = 60 seconds
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        prevTime = Time.time;
    }

    public  void PauseTime()
    {
        prevGameSpeed = gameSpeed;
        gameSpeed = 0f;
        paused = true;
    }
    public  void UnPauseTime()
    {
        gameSpeed = prevGameSpeed;
        paused = false;
    }
    void Update()
    {
        gameTime += (Time.time-prevTime)*gameSpeed;
        prevTime = Time.time;
        currentGameTime = gameTime;
        currentGameDays = Mathf.Floor(gameTime / dayInMinutes);
        currentGameHours = Mathf.Floor((gameTime/60)%24);
        currentMinutes = Mathf.Floor(gameTime % 60);

        if (currentGameDays > prevGameDays && dayChangeDelegate != null)
        {
            prevGameDays = currentGameDays;
            dayChangeDelegate.Invoke();
        }

        if (currentGameHours < 10)
            hoursString = "0" + currentGameHours;
        else
            hoursString = currentGameHours.ToString();

        if (currentMinutes < 10)
            minutesString = "0" + currentMinutes;
        else
            minutesString = currentMinutes.ToString();

        dayString = "Day: " + (currentGameDays + 1.0) + "   Time: " + hoursString + ":" + minutesString;
    }
}
