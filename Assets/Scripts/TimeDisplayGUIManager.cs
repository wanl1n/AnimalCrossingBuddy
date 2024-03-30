using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TimeDisplayGUIManager : MonoBehaviour
{

    private VisualElement _root;

    private Image _springIcon;
    private Image _summerIcon;
    private Image _autumnIcon;
    private Image _winterIcon;

    private Label _timeLabel;
    private Label _AMPMLabel;
    private Label _dayLabel;
    private Label _dateLabel;

    private VisualElement _resetButton;

    [SerializeField]
    private GameObject _timePopup;

    [SerializeField]
    private GameObject _datePopup;

    // Start is called before the first frame update
    public void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;

        this._springIcon = this._root.Q<Image>("SpringIcon");
        this._summerIcon = this._root.Q<Image>("SummerIcon");
        this._autumnIcon = this._root.Q<Image>("AutumnIcon");
        this._winterIcon = this._root.Q<Image>("WinterIcon");

        this._timeLabel = this._root.Q<Label>("TimeLabel");
        this._AMPMLabel = this._root.Q<Label>("AMPMLabel");
        this._dayLabel = this._root.Q<Label>("DayLabel");
        this._dateLabel = this._root.Q<Label>("DateLabel");
        this._resetButton = this._root.Q<VisualElement>("ResetButton");

    
        if (this._timeLabel != null)
        {
            StartCoroutine(UpdateTime());
            this._timeLabel.RegisterCallback<PointerDownEvent>(this.OnTimeClick);
        }

        if (this._AMPMLabel != null)
            this._AMPMLabel.RegisterCallback<PointerDownEvent>(this.OnAMPMClick);

        if (this._dateLabel != null)
            this._dateLabel.RegisterCallback<PointerDownEvent>(this.OnDateClick);

        if (this._resetButton != null)
            this._resetButton.RegisterCallback<PointerDownEvent>(this.OnResetClick);

        if (this._springIcon != null && this._summerIcon != null && this._autumnIcon != null && this._winterIcon != null)
            this.UpdateSeasons();
    }

    void OnTimeClick(PointerDownEvent e)
    {
        VisualElement popupRoot = this._timePopup.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Image popUpBG = transparentBG.Q<Image>("PopUpBG");
        popUpBG.style.display = DisplayStyle.Flex;

        this._timePopup.GetComponent<TimePopUpGUIManager>().ScrollToTime();
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

    void OnDateClick(PointerDownEvent e) 
    {
        VisualElement popupRoot = this._datePopup.GetComponent<UIDocument>().rootVisualElement;
        VisualElement transparentBG = popupRoot.Q<VisualElement>("TransparentBG");
        transparentBG.style.display = DisplayStyle.Flex;

        Image popUpBG = transparentBG.Q<Image>("PopUpBG");
        popUpBG.style.display = DisplayStyle.Flex;

        this._datePopup.GetComponent<DatePopUpGUIManager>().ScrollToDate();
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

    public void UpdateDisplay()
    {
        this._timeLabel.text = TimeManager.GetInstance().GetTimeString();
        this._AMPMLabel.text = TimeManager.GetInstance().GetAMPMString();
        this._dayLabel.text = TimeManager.GetInstance().GetDayString();
        this._dateLabel.text = TimeManager.GetInstance().GetDateString();

        this.UpdateSeasons();

        GameObject sceneDocument = GameObject.FindGameObjectWithTag("Scene Document");
        ContentListGUIManager contentListGUIManager = sceneDocument.GetComponent<ContentListGUIManager>();
        if (contentListGUIManager != null)
        {
            StartCoroutine(contentListGUIManager.LoadNowPortrait());
        }
    }

    private void UpdateSeasons()
    {
        if (this._springIcon != null && this._summerIcon != null && this._autumnIcon != null && this._winterIcon != null)
        {

            switch (TimeManager.GetInstance().GetCurrentSeason())
            {
                case TimeManager.Seasons.SPRING:
                    this._springIcon.style.display = DisplayStyle.Flex;
                    this._summerIcon.style.display = DisplayStyle.None;
                    this._autumnIcon.style.display = DisplayStyle.None;
                    this._winterIcon.style.display = DisplayStyle.None;
                    break;

                case TimeManager.Seasons.SUMMER:
                    this._springIcon.style.display = DisplayStyle.None;
                    this._summerIcon.style.display = DisplayStyle.Flex;
                    this._autumnIcon.style.display = DisplayStyle.None;
                    this._winterIcon.style.display = DisplayStyle.None;
                    break;

                case TimeManager.Seasons.FALL:
                    this._springIcon.style.display = DisplayStyle.None;
                    this._summerIcon.style.display = DisplayStyle.None;
                    this._autumnIcon.style.display = DisplayStyle.Flex;
                    this._winterIcon.style.display = DisplayStyle.None;
                    break;

                case TimeManager.Seasons.WINTER:
                    this._springIcon.style.display = DisplayStyle.None;
                    this._summerIcon.style.display = DisplayStyle.None;
                    this._autumnIcon.style.display = DisplayStyle.None;
                    this._winterIcon.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}
