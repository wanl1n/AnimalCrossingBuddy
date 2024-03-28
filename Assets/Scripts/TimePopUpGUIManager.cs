using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
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

    [SerializeField]
    private int _activeHour = -1;
    [SerializeField] 
    private int _activeMin = -1;

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

        this._minuteLabels[0] = this._minuteSlider.Q<Label>("Minute0");
        this._minuteLabels[1] = this._minuteSlider.Q<Label>("Minute1");
        this._minuteLabels[2] = this._minuteSlider.Q<Label>("Minute2");
        this._minuteLabels[3] = this._minuteSlider.Q<Label>("Minute3");
        this._minuteLabels[4] = this._minuteSlider.Q<Label>("Minute4");
        this._minuteLabels[5] = this._minuteSlider.Q<Label>("Minute5");
        this._minuteLabels[6] = this._minuteSlider.Q<Label>("Minute6");
        this._minuteLabels[7] = this._minuteSlider.Q<Label>("Minute7");
        this._minuteLabels[8] = this._minuteSlider.Q<Label>("Minute8");
        this._minuteLabels[9] = this._minuteSlider.Q<Label>("Minute9");
        this._minuteLabels[10] = this._minuteSlider.Q<Label>("Minute10");
        this._minuteLabels[11] = this._minuteSlider.Q<Label>("Minute11");
        this._minuteLabels[12] = this._minuteSlider.Q<Label>("Minute12");
        this._minuteLabels[13] = this._minuteSlider.Q<Label>("Minute13");
        this._minuteLabels[14] = this._minuteSlider.Q<Label>("Minute14");
        this._minuteLabels[15] = this._minuteSlider.Q<Label>("Minute15");
        this._minuteLabels[16] = this._minuteSlider.Q<Label>("Minute16");
        this._minuteLabels[17] = this._minuteSlider.Q<Label>("Minute17");
        this._minuteLabels[18] = this._minuteSlider.Q<Label>("Minute18");
        this._minuteLabels[19] = this._minuteSlider.Q<Label>("Minute19");
        this._minuteLabels[20] = this._minuteSlider.Q<Label>("Minute20");
        this._minuteLabels[21] = this._minuteSlider.Q<Label>("Minute21");
        this._minuteLabels[22] = this._minuteSlider.Q<Label>("Minute22");
        this._minuteLabels[23] = this._minuteSlider.Q<Label>("Minute23");
        this._minuteLabels[24] = this._minuteSlider.Q<Label>("Minute24");
        this._minuteLabels[25] = this._minuteSlider.Q<Label>("Minute25");
        this._minuteLabels[26] = this._minuteSlider.Q<Label>("Minute26");
        this._minuteLabels[27] = this._minuteSlider.Q<Label>("Minute27");
        this._minuteLabels[28] = this._minuteSlider.Q<Label>("Minute28");
        this._minuteLabels[29] = this._minuteSlider.Q<Label>("Minute29");
        this._minuteLabels[30] = this._minuteSlider.Q<Label>("Minute30");
        this._minuteLabels[31] = this._minuteSlider.Q<Label>("Minute31");
        this._minuteLabels[32] = this._minuteSlider.Q<Label>("Minute32");
        this._minuteLabels[33] = this._minuteSlider.Q<Label>("Minute33");
        this._minuteLabels[34] = this._minuteSlider.Q<Label>("Minute34");
        this._minuteLabels[35] = this._minuteSlider.Q<Label>("Minute35");
        this._minuteLabels[36] = this._minuteSlider.Q<Label>("Minute36");
        this._minuteLabels[37] = this._minuteSlider.Q<Label>("Minute37");
        this._minuteLabels[38] = this._minuteSlider.Q<Label>("Minute38");
        this._minuteLabels[39] = this._minuteSlider.Q<Label>("Minute39");
        this._minuteLabels[40] = this._minuteSlider.Q<Label>("Minute40");
        this._minuteLabels[41] = this._minuteSlider.Q<Label>("Minute41");
        this._minuteLabels[42] = this._minuteSlider.Q<Label>("Minute42");
        this._minuteLabels[43] = this._minuteSlider.Q<Label>("Minute43");
        this._minuteLabels[44] = this._minuteSlider.Q<Label>("Minute44");
        this._minuteLabels[45] = this._minuteSlider.Q<Label>("Minute45");
        this._minuteLabels[46] = this._minuteSlider.Q<Label>("Minute46");
        this._minuteLabels[47] = this._minuteSlider.Q<Label>("Minute47");
        this._minuteLabels[48] = this._minuteSlider.Q<Label>("Minute48");
        this._minuteLabels[49] = this._minuteSlider.Q<Label>("Minute49");
        this._minuteLabels[50] = this._minuteSlider.Q<Label>("Minute50");
        this._minuteLabels[51] = this._minuteSlider.Q<Label>("Minute51");
        this._minuteLabels[52] = this._minuteSlider.Q<Label>("Minute52");
        this._minuteLabels[53] = this._minuteSlider.Q<Label>("Minute53");
        this._minuteLabels[54] = this._minuteSlider.Q<Label>("Minute54");
        this._minuteLabels[55] = this._minuteSlider.Q<Label>("Minute55");
        this._minuteLabels[56] = this._minuteSlider.Q<Label>("Minute56");
        this._minuteLabels[57] = this._minuteSlider.Q<Label>("Minute57");
        this._minuteLabels[58] = this._minuteSlider.Q<Label>("Minute58");
        this._minuteLabels[59] = this._minuteSlider.Q<Label>("Minute59");

        this._minuteOffset3 = this._minuteSlider.Q<Label>("Offset3");
        this._minuteOffset4 = this._minuteSlider.Q<Label>("Offset4");

        this._transparentBG.RegisterCallback<PointerDownEvent>(this.OnTransparentBGClick);

    }

    private void Update()
    {
        this.CheckHourLabel();
        this.CheckMinuteLabel();
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
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 12;
                        }
                        break;
                    case "Offset3":
                        this._activeHour = 11;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 23;
                        }
                        break;
                    case "Hour12":
                        this._activeHour = 10;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 22;
                        }
                        break;
                    case "Hour11":
                        this._activeHour = 9;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 21;
                        }
                        break;
                    case "Hour10":
                        this._activeHour = 8;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 20;
                        }
                        break;
                    case "Hour9":
                        this._activeHour = 7;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 19;
                        }
                        break;
                    case "Hour8":
                        this._activeHour = 6;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 18;
                        }
                        break;
                    case "Hour7":
                        this._activeHour = 5;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 17;
                        }
                        break;
                    case "Hour6":
                        this._activeHour = 4;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 16;
                        }
                        break;
                    case "Hour5":
                        this._activeHour = 3;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 15;
                        }
                        break;
                    case "Hour4":
                        this._activeHour = 2;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 14;
                        }
                        break;
                    case "Hour3":
                    case "Hour2":
                    case "Hour1":
                        this._activeHour = 1;
                        if (TimeManager.GetInstance().PlayerTime.Hour > 11)
                        {
                            this._activeHour = 13;
                        }
                        break;
                }
            }
        }
    }

    private void CheckMinuteLabel()
    {
        float minSliderTop = this._minuteSlider.worldBound.yMax;
        float minSliderBot = this._minuteSlider.worldBound.yMin;

        VisualElement hourContentContainer = this._minuteSlider.contentContainer;
        foreach (VisualElement child in hourContentContainer.Children())
        {
            Rect childWorldBounds = child.worldBound;
            bool isVisible = childWorldBounds.yMin < minSliderTop && childWorldBounds.yMax > minSliderBot;

            if (isVisible)
            {
                switch (child.name)
                {
                    case "Offset4":
                        this._activeMin = 59;
                        break;
                    case "Offset3":
                        this._activeMin = 58;
                        break;
                    default:
                        string min = new String(child.name.Where(Char.IsDigit).ToArray());
                        this._activeMin = Int32.Parse(min);
                        this._activeMin -= 2;
                        break;
                }
            }
        }

    }

    public void ScrollToTime()
    {
        this._hourSlider.RegisterCallback<GeometryChangedEvent>(this.UpdateHourList);
        this._minuteSlider.RegisterCallback<GeometryChangedEvent>(this.UpdateMinuteList);
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

    private void UpdateMinuteList(GeometryChangedEvent e)
    {
        Label moveTo = this._minuteLabels[0];
        switch (TimeManager.GetInstance().PlayerTime.Minute)
        {
            case 58:
                moveTo = this._minuteOffset3;
                break;
            case 59:
                moveTo = this._minuteOffset4;
                break;
            default:
                moveTo = this._minuteLabels[TimeManager.GetInstance().PlayerTime.Minute + 2];
                break;
        }
        this._activeMin = TimeManager.GetInstance().PlayerTime.Minute;
        this._minuteSlider.ScrollTo(moveTo);
    }

    void OnTransparentBGClick(PointerDownEvent e)
    {
        this._transparentBG.style.display = DisplayStyle.None;

        TimeManager.GetInstance().IsCustomTimeSet = true;

        TimeManager.GetInstance().IsCustomTimeSet = true;

        DateTime dateTime = TimeManager.GetInstance().PlayerTime;
        TimeManager.GetInstance().PlayerTime =
            new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, this._activeHour, dateTime.Minute, dateTime.Second);
        dateTime = TimeManager.GetInstance().PlayerTime;
        TimeManager.GetInstance().PlayerTime =
            new System.DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, this._activeMin, dateTime.Second);

        this.timeDisplayGUIManager.UpdateDisplay();
    }
}
