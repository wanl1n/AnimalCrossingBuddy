using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DatePopUpGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _transparentBG;
    private VisualElement _popUpBG;

    private ScrollView _monthSlider;
    private ScrollView _daySlider;

    private Label[] _monthLabels = new Label[12];
    private Label _monthOffset3;
    private Label _monthOffset4;

    private Label[] _dayLabels = new Label[31];
    private Label _dayOffset3;
    private Label _dayOffset4;

    [SerializeField]
    private int _activeMonth = -1;
    [SerializeField]
    private int _activeDay = -1;

    [SerializeField]
    TimeDisplayGUIManager timeDisplayGUIManager;

    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._transparentBG = this._root.Q<VisualElement>("TransparentBG");

        this._popUpBG = this._transparentBG.Q<VisualElement>("PopUpBG");
        this._monthSlider = this._popUpBG.Q<ScrollView>("MonthSlider");
        this._daySlider = this._popUpBG.Q<ScrollView>("DaySlider");

        this._monthLabels[0] = this._monthSlider.Q<Label>("Month1");
        this._monthLabels[1] = this._monthSlider.Q<Label>("Month2");
        this._monthLabels[2] = this._monthSlider.Q<Label>("Month3");
        this._monthLabels[3] = this._monthSlider.Q<Label>("Month4");
        this._monthLabels[4] = this._monthSlider.Q<Label>("Month5");
        this._monthLabels[5] = this._monthSlider.Q<Label>("Month6");
        this._monthLabels[6] = this._monthSlider.Q<Label>("Month7");
        this._monthLabels[7] = this._monthSlider.Q<Label>("Month8");
        this._monthLabels[8] = this._monthSlider.Q<Label>("Month9");
        this._monthLabels[9] = this._monthSlider.Q<Label>("Month10");
        this._monthLabels[10] = this._monthSlider.Q<Label>("Month11");
        this._monthLabels[11] = this._monthSlider.Q<Label>("Month12");

        this._monthOffset3 = this._monthSlider.Q<Label>("Offset3");
        this._monthOffset4 = this._monthSlider.Q<Label>("Offset4");

        this._dayLabels[0] = this._daySlider.Q<Label>("Day1");
        this._dayLabels[1] = this._daySlider.Q<Label>("Day2");
        this._dayLabels[2] = this._daySlider.Q<Label>("Day3");
        this._dayLabels[3] = this._daySlider.Q<Label>("Day4");
        this._dayLabels[4] = this._daySlider.Q<Label>("Day5");
        this._dayLabels[5] = this._daySlider.Q<Label>("Day6");
        this._dayLabels[6] = this._daySlider.Q<Label>("Day7");
        this._dayLabels[7] = this._daySlider.Q<Label>("Day8");
        this._dayLabels[8] = this._daySlider.Q<Label>("Day9");
        this._dayLabels[9] = this._daySlider.Q<Label>("Day10");
        this._dayLabels[10] = this._daySlider.Q<Label>("Day11");
        this._dayLabels[11] = this._daySlider.Q<Label>("Day12");
        this._dayLabels[12] = this._daySlider.Q<Label>("Day13");
        this._dayLabels[13] = this._daySlider.Q<Label>("Day14");
        this._dayLabels[14] = this._daySlider.Q<Label>("Day15");
        this._dayLabels[15] = this._daySlider.Q<Label>("Day16");
        this._dayLabels[16] = this._daySlider.Q<Label>("Day17");
        this._dayLabels[17] = this._daySlider.Q<Label>("Day18");
        this._dayLabels[18] = this._daySlider.Q<Label>("Day19");
        this._dayLabels[19] = this._daySlider.Q<Label>("Day20");
        this._dayLabels[20] = this._daySlider.Q<Label>("Day21");
        this._dayLabels[21] = this._daySlider.Q<Label>("Day22");
        this._dayLabels[22] = this._daySlider.Q<Label>("Day23");
        this._dayLabels[23] = this._daySlider.Q<Label>("Day24");
        this._dayLabels[24] = this._daySlider.Q<Label>("Day25");
        this._dayLabels[25] = this._daySlider.Q<Label>("Day26");
        this._dayLabels[26] = this._daySlider.Q<Label>("Day27");
        this._dayLabels[27] = this._daySlider.Q<Label>("Day28");
        this._dayLabels[28] = this._daySlider.Q<Label>("Day29");
        this._dayLabels[29] = this._daySlider.Q<Label>("Day30");
        this._dayLabels[30] = this._daySlider.Q<Label>("Day31");

        this._dayOffset3 = this._daySlider.Q<Label>("Offset3");
        this._dayOffset4 = this._daySlider.Q<Label>("Offset4");

        this._transparentBG.RegisterCallback<PointerDownEvent>(this.OnTransparentBGClick);
    }

    private void Update()
    {
        this.CheckMonthLabel();
        this.CheckDayLabel();
    }

    private void CheckMonthLabel()
    {
        float monthSliderTop = this._monthSlider.worldBound.yMax;
        float monthSliderBot = this._monthSlider.worldBound.yMin;

        VisualElement monthContentContainer = this._monthSlider.contentContainer;
        foreach (VisualElement child in monthContentContainer.Children())
        {
            Rect childWorldBounds = child.worldBound;
            bool isVisible = childWorldBounds.yMin < monthSliderTop && childWorldBounds.yMax > monthSliderBot;

            if (isVisible)
            {
                switch (child.name)
                {
                    case "Offset4":
                        this._activeMonth = 12;
                        break;
                    case "Offset3":
                        this._activeMonth = 11;
                        break;
                    default:
                        string month = new String(child.name.Where(Char.IsDigit).ToArray());
                        this._activeMonth = Int32.Parse(month);
                        this._activeMonth -= 2;
                        break;
                }
            }
        }
    }

    private void CheckDayLabel()
    {
        float daySliderTop = this._daySlider.worldBound.yMax;
        float daySliderBot = this._daySlider.worldBound.yMin;

        VisualElement dayContentContainer = this._daySlider.contentContainer;
        foreach (VisualElement child in dayContentContainer.Children())
        {
            Rect childWorldBounds = child.worldBound;
            bool isVisible = childWorldBounds.yMin < daySliderTop && childWorldBounds.yMax > daySliderBot;

            if (isVisible)
            {
                switch (child.name)
                {
                    case "Offset4":
                        this._activeDay = 31;
                        break;
                    case "Offset3":
                        this._activeDay = 30;
                        break;
                    default:
                        string day = new String(child.name.Where(Char.IsDigit).ToArray());
                        this._activeDay = Int32.Parse(day);
                        this._activeDay -= 2;
                        break;
                }
            }
        }
    }

    public void ScrollToDate()
    {
        this._monthSlider.RegisterCallback<GeometryChangedEvent>(this.UpdateMonthList);
        this._daySlider.RegisterCallback<GeometryChangedEvent>(this.UpdateDayList);
    }

    private void UpdateMonthList(GeometryChangedEvent e)
    {
        Label moveTo = this._monthLabels[0];
        switch (TimeManager.GetInstance().PlayerTime.Month) 
        {
            case 12:
                moveTo = this._monthOffset4;
                break;
            case 11:
                moveTo = this._monthOffset3;
                break;
            default:
                moveTo = this._monthLabels[TimeManager.GetInstance().PlayerTime.Month + 1];
                break;

        }
        
        this._monthSlider.ScrollTo(moveTo);
        this._activeMonth = TimeManager.GetInstance().PlayerTime.Month;
    }

    private void UpdateDayList(GeometryChangedEvent e)
    {
        Label moveTo = this._dayLabels[0];
        switch (TimeManager.GetInstance().PlayerTime.Day)
        {
            case 31:
                moveTo = this._dayOffset4;
                break;
            case 30:
                moveTo = this._dayOffset3;
                break;
            default:
                moveTo = this._dayLabels[TimeManager.GetInstance().PlayerTime.Day + 1];
                break;
        }

        this._daySlider.ScrollTo(moveTo);
        this._activeDay = TimeManager.GetInstance().PlayerTime.Day;

    }

    private void CheckDay()
    {
        switch (this._activeMonth)
        {
            case 4:
            case 6:
            case 9:
            case 11:
                if (this._activeDay >= 30)
                    this._activeDay = 30;
                break;

            case 2:
                if (this._activeDay >= 29)
                    this._activeDay = 29;
                break;
        }
    }

    private void OnTransparentBGClick(PointerDownEvent e)
    {
        this._transparentBG.style.display = DisplayStyle.None;

        TimeManager.GetInstance().IsCustomTimeSet = true;

        this.CheckDay();
        DateTime dateTime = TimeManager.GetInstance().PlayerTime;
        
        dateTime = TimeManager.GetInstance().PlayerTime;
        TimeManager.GetInstance().PlayerTime =
            new System.DateTime(dateTime.Year, this._activeMonth, this._activeDay, dateTime.Hour, dateTime.Minute, dateTime.Second);


        this.timeDisplayGUIManager.UpdateDisplay();
    }

}
