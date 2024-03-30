using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using NUnit.Framework.Constraints;

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

     public Seasons GetCurrentSeason()
    {
        DateTime SpringStart = new DateTime(this.PlayerTime.Year, 2, 25);
        DateTime SpringEnd = new DateTime(this.PlayerTime.Year, 5, 31);

        DateTime SummerStart = new DateTime(this.PlayerTime.Year, 6, 1);
        DateTime SummerEnd = new DateTime(this.PlayerTime.Year, 8, 31);

        DateTime AutumnStart = new DateTime(this.PlayerTime.Year, 9, 1);
        DateTime AutumnEnd = new DateTime(this.PlayerTime.Year, 11, 25);

        DateTime WinterStart = new DateTime(this.PlayerTime.Year, 11, 26);
        DateTime WinterEnd = new DateTime(this.PlayerTime.Year, 2, 24);

        if (IsInSouthernHemisphere)
        {
            SpringStart = new DateTime(this.PlayerTime.Year, 8, 25);
            SpringEnd = new DateTime(this.PlayerTime.Year, 11, 30);

            SummerStart = new DateTime(this.PlayerTime.Year, 12, 1);
            SummerEnd = new DateTime(this.PlayerTime.Year, 2, 29);

            AutumnStart = new DateTime(this.PlayerTime.Year, 3, 1);
            AutumnEnd = new DateTime(this.PlayerTime.Year, 5, 25);

            WinterStart = new DateTime(this.PlayerTime.Year, 5, 26);
            WinterStart = new DateTime(this.PlayerTime.Year, 8, 24);
        }

        if (IsBetweenTwoDates(this.PlayerTime, SpringStart, SpringEnd))
            return Seasons.SPRING;
        else if (IsBetweenTwoDates(this.PlayerTime, SummerStart, SummerEnd))
            return Seasons.SUMMER;
        else if (IsBetweenTwoDates(this.PlayerTime, AutumnStart, AutumnEnd))
            return Seasons.FALL;
        else
            return Seasons.WINTER;

    }

    // date string ("26 March")
    public string GetDateString()
    {
        return PlayerTime.ToString("d MMMM");
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

    public string GetMDString()
    {
        return PlayerTime.ToString("M/d");
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
