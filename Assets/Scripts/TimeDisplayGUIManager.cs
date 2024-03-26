using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeDisplayGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private Label _timeLabel;
    private Label _AMPMLabel;
    private Label _dayLabel;
    private Label _dateLabel;

    private VisualElement _resetButton;

    // Start is called before the first frame update
    public void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        this._timeLabel = _root.Q<Label>("TimeLabel");
        this._AMPMLabel = _root.Q<Label>("AMPMLabel");
        this._dayLabel = _root.Q<Label>("DayLabel");
        this._dateLabel = _root.Q<Label>("DateLabel");
        this._resetButton = _root.Q<VisualElement>("ResetButton");
    
        if (_timeLabel != null)
            StartCoroutine(UpdateTime());

        this._AMPMLabel.RegisterCallback<PointerDownEvent>(this.OnAMPMClick);
        this._resetButton.RegisterCallback<PointerDownEvent>(this.OnResetClick);
    }

    void OnAMPMClick(PointerDownEvent e)
    {
        int hour = TimeManager.GetInstance().PlayerTime.Hour;

        if (hour >= 0 && hour <= 11)
            hour += 12;
        else if (hour >= 12 && hour <= 23)
            hour -= 12;

        DateTime dateTime = TimeManager.GetInstance().PlayerTime;

        TimeManager.GetInstance().IsCustomTimeSet = true;

        TimeManager.GetInstance().PlayerTime =
            new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, dateTime.Minute, dateTime.Second);

        this.UpdateDisplay();
    }

    void OnResetClick(PointerDownEvent e)
    {
        TimeManager.GetInstance().IsCustomTimeSet = false;
        TimeManager.GetInstance().PlayerTime = DateTime.Now;

        this.UpdateDisplay();
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            this.UpdateDisplay();

            yield return new WaitForSeconds(5);
        }
    }

    private void UpdateDisplay()
    {
        this._timeLabel.text = TimeManager.GetInstance().GetTimeString();
        this._AMPMLabel.text = TimeManager.GetInstance().GetAMPMString();
        this._dayLabel.text = TimeManager.GetInstance().GetDayString();
        this._dateLabel.text = TimeManager.GetInstance().GetDateString();
    }
}
