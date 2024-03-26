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


    // Start is called before the first frame update
    public void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        _timeLabel = _root.Q<Label>("TimeLabel");
        _AMPMLabel = _root.Q<Label>("AMPMLabel");
        _dayLabel = _root.Q<Label>("DayLabel");
        _dateLabel = _root.Q<Label>("DateLabel");
    
        if (_timeLabel != null)
            StartCoroutine(UpdateTime());
    }

    private IEnumerator UpdateTime()
    {
        while (true)
        {
            _timeLabel.text = TimeManager.Instance.GetTimeString();
            _AMPMLabel.text = TimeManager.Instance.GetAMPMString();
            _dayLabel.text = TimeManager.Instance.GetDayString();
            _dateLabel.text = TimeManager.Instance.GetDateString();

            yield return new WaitForSeconds(5);
        }
    }
}
