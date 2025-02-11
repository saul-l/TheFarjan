using System;
using UnityEngine;

public class Globals : MonoBehaviour
{
    public static float gameTime;
    [SerializeField] private float currentGameDays;
    [SerializeField] private float currentGameTime;
    [SerializeField] private float currentGameHours;
    [SerializeField] private float currentMinutes;
    public float gameSpeed = 1f;
    private float prevTime;
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
        currentGameDays = Mathf.Floor(gameTime / 1440);
        currentGameHours = Mathf.Floor((gameTime/60)%24);
        currentMinutes = Mathf.Floor(gameTime % 60);
    }
}
