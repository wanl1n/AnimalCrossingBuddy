using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    private bool _isCustomTimeSet = false;
    public bool IsCustomTimeSet
    {
        get
        {
            return _isCustomTimeSet;
        }
    }


    private DateTime _playerTime = DateTime.Now;
    public DateTime PlayerTime
    {
        set
        {
            _playerTime = value;
        }
        get
        {
            if (_isCustomTimeSet)
                return _playerTime;
            else
                return DateTime.Now;
        }
    }

    public void ResetDateTime()
    {
        _playerTime = DateTime.Now;
        _isCustomTimeSet = false;
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
}
