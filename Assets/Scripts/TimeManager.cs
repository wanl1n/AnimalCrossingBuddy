using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager Instance;

    private bool _isCustomTimeSet = false;
    public bool IsCustomTimeSet
    {
        get
        {
            return this._isCustomTimeSet;
        }
        set
        {
            this._isCustomTimeSet = value;
        }
    }


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

    private void Update()
    {
        if (this._isCustomTimeSet)
        {
            this._playerTime = this._playerTime.AddSeconds(Time.deltaTime);
        }
    }

    public void ResetDateTime()
    {
        this._playerTime = DateTime.Now;
        this._isCustomTimeSet = false;
    }

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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public static TimeManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new TimeManager();
        }
        return Instance;
    }
}
