using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TimePopUpGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _transparentBG;
    private VisualElement _popUpBG;

    private ScrollView _hourSlider;
    private ScrollView _minuteSlider;

    private Label[] _hourLabels = new Label[12];
    private Label _hourOffset3;
    private Label _hourOffset4;

    private Label[] _minuteLabels = new Label[60];
    private Label _minuteOffset3;
    private Label _minuteOffset4;

    private int _activeHour = -1;

    [SerializeField]
    TimeDisplayGUIManager timeDisplayGUIManager;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._transparentBG = this._root.Q<VisualElement>("TransparentBG");

        this._popUpBG = this._transparentBG.Q<VisualElement>("PopUpBG");
        this._hourSlider = this._popUpBG.Q<ScrollView>("HourSlider");
        this._minuteSlider = this._popUpBG.Q<ScrollView>("MinuteSlider");

        this._hourLabels[1] = this._hourSlider.Q<Label>("Hour1");
        this._hourLabels[2] = this._hourSlider.Q<Label>("Hour2");
        this._hourLabels[3] = this._hourSlider.Q<Label>("Hour3");
        this._hourLabels[4] = this._hourSlider.Q<Label>("Hour4");
        this._hourLabels[5] = this._hourSlider.Q<Label>("Hour5");
        this._hourLabels[6] = this._hourSlider.Q<Label>("Hour6");
        this._hourLabels[7] = this._hourSlider.Q<Label>("Hour7");
        this._hourLabels[8] = this._hourSlider.Q<Label>("Hour8");
        this._hourLabels[9] = this._hourSlider.Q<Label>("Hour9");
        this._hourLabels[10] = this._hourSlider.Q<Label>("Hour10");
        this._hourLabels[11] = this._hourSlider.Q<Label>("Hour11");
        this._hourLabels[0] = this._hourSlider.Q<Label>("Hour12");

        this._hourOffset3 = this._hourSlider.Q<Label>("Offset3");
        this._hourOffset4 = this._hourSlider.Q<Label>("Offset4");

        this._minuteLabels[0] = this._minuteSlider.Q<Label>("Minute1");

        this._transparentBG.RegisterCallback<PointerDownEvent>(this.OnTransparentBGClick);

    }

    private void Update()
    {
        this.CheckHourLabel();
       
    }

    private void CheckHourLabel()
    {
        float hourSliderTop = this._hourSlider.worldBound.yMax;
        float hourSliderBot = this._hourSlider.worldBound.yMin;

        VisualElement hourContentContainer = this._hourSlider.contentContainer;
        foreach (VisualElement child in hourContentContainer.Children())
        {
            Rect childWorldBounds = child.worldBound;
            bool isVisible = childWorldBounds.yMin < hourSliderTop && childWorldBounds.yMax > hourSliderBot;

            if (isVisible)
            {
                switch (child.name)
                {
                    case "Offset4":
                        this._activeHour = 0;
                        break;
                    case "Offset3":
                        this._activeHour = 11;
                        break;
                    case "Hour12":
                        this._activeHour = 10;
                        break;
                    case "Hour11":
                        this._activeHour = 9;
                        break;
                    case "Hour10":
                        this._activeHour = 8;
                        break;
                    case "Hour9":
                        this._activeHour = 7;
                        break;
                    case "Hour8":
                        this._activeHour = 6;
                        break;
                    case "Hour7":
                        this._activeHour = 5;
                        break;
                    case "Hour6":
                        this._activeHour = 4;
                        break;
                    case "Hour5":
                        this._activeHour = 3;
                        break;
                    case "Hour4":
                        this._activeHour = 2;
                        break;
                    case "Hour3":
                        this._activeHour = 1;
                        break;
                    case "Hour2":
                        this._activeHour = 1;
                        break;
                    case "Hour1":
                        this._activeHour = 1;
                        break;
                }
            }

            if (TimeManager.GetInstance().PlayerTime.Hour > 11)
            {
                this._activeHour += 12;
            }
        }
    }

    public void ScrollToTime()
    {
        this._hourSlider.RegisterCallback<GeometryChangedEvent>(this.UpdateHourList);
        
    }

    private void UpdateHourList(GeometryChangedEvent e)
    {
        Label moveTo = this._hourLabels[0];
        switch (TimeManager.GetInstance().PlayerTime.Hour)
        {
                case 1:
                case 13:
                    moveTo = this._hourLabels[3];
                    break;
                case 2:
                case 14:
                    moveTo = this._hourLabels[4];
                    break;
                case 3:
                case 15:
                    moveTo = this._hourLabels[5];
                    break;
                case 4:
                case 16:
                    moveTo = this._hourLabels[6];
                    break;
                case 5:
                case 17:
                    moveTo = this._hourLabels[7];
                    break;
                case 6:
                case 18:
                    moveTo = this._hourLabels[8];
                    break;
                case 7:
                case 19:
                    moveTo = this._hourLabels[9];
                    break;
                case 8:
                case 20:
                    moveTo = this._hourLabels[10];
                    break;
                case 9:
                case 21:
                    moveTo = this._hourLabels[11];
                    break;
                case 10:
                case 22:
                    moveTo = this._hourLabels[0];
                    break;
                case 11:
                case 23:
                    moveTo = this._hourOffset3;
                    break;
                case 0:
                case 12:
                    moveTo = this._hourOffset4;
                    break;
                default:
                    moveTo = this._hourLabels[1];
                    break;
            }
            this._activeHour = TimeManager.GetInstance().PlayerTime.Hour;
            if (this._activeHour > 11)
            {
                this._activeHour -= 12;
            }
            this._hourSlider.ScrollTo(moveTo);

    }

    void OnTransparentBGClick(PointerDownEvent e)
    {
        this._transparentBG.style.display = DisplayStyle.None;

        TimeManager.GetInstance().IsCustomTimeSet = true;
        DateTime dateTime = TimeManager.GetInstance().PlayerTime;

        TimeManager.GetInstance().IsCustomTimeSet = true;

        TimeManager.GetInstance().PlayerTime =
            new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this._activeHour, dateTime.Minute, dateTime.Second);

        this.timeDisplayGUIManager.UpdateDisplay();
    }
}
