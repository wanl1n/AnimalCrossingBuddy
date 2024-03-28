using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public enum Seasons
    {
        SPRING,
        SUMMER,
        FALL,
        WINTER
    }

    private static TimeManager _instance;

    private bool _isInSouthernHemisphere = false;
    public bool IsInSouthernHemisphere { get => _isInSouthernHemisphere; set => _isInSouthernHemisphere = value; }

    private bool _isCustomTimeSet = false;
    public bool IsCustomTimeSet { get => _isCustomTimeSet; set => _isCustomTimeSet = value; }

    private DateTime _playerTime = DateTime.Now;
    public DateTime PlayerTime
    {
        set
        {
            this._playerTime = value;
        }
        get
        {
            if (this._isCustomTimeSet)
                return this._playerTime;
            else
                return DateTime.Now;
        }
    }

    public static bool IsBetweenTwoDates(DateTime dt, DateTime start, DateTime end)
    {
        return dt >= start && dt <= end;
    }

    public void ResetDateTime()
    {
        this._playerTime = DateTime.Now;
        this._isCustomTimeSet = false;
    }

    // TODO: Take season dates from database
    // public Seasons GetCurrentSeason()
    // {
    //     if (IsInSouthernHemisphere)
    //     {
    //         DateTime SHSpringStart  = new(PlayerTime.Year, 08, 25);
    //         DateTime SHSpringEnd    = new(PlayerTime.Year, 11, 30);
    //         DateTime SHSummerStart  = new(PlayerTime.Year, 08, 25);
    //         DateTime SHSummerEnd    = new(PlayerTime.Year, 11, 30);
    //         DateTime SHFallStart    = new(PlayerTime.Year, 08, 25);
    //         DateTime SHFallEnd      = new(PlayerTime.Year, 11, 30);
    //         DateTime SHWinterStart  = new(PlayerTime.Year, 08, 25);
    //         DateTime SHWinterEnd    = new(PlayerTime.Year, 11, 30);

    //         if (IsBetweenTwoDates(PlayerTime, SHSpringStart, SHSpringEnd))
    //             return Seasons.SPRING;
    //     }
    //     else
    //     {

    //         return;
    //     }
    // }

    // date string ("26 March")
    public string GetDateString()
    {
        return PlayerTime.ToString("M MMMM");
    }

    // day string ("Tue.")
    public string GetDayString()
    {
        return PlayerTime.ToString("ddd.");
    }

    // time string ("12:00")
    public string GetTimeString()
    {
        return PlayerTime.ToString("hh:mm");
    }

    // AM / PM String
    public string GetAMPMString()
    {
        return PlayerTime.ToString("tt");
    }

    private void Update()
    {
        if (this._isCustomTimeSet)
        {
            this._playerTime = this._playerTime.AddSeconds(Time.deltaTime);
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static TimeManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new();
        }
        return _instance;
    }
}
