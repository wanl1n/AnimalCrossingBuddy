using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class MonthAvailabilityGUIManager : MonoBehaviour
{
    private VisualElement _root;

    private List<Label> _months;

    // Start is called before the first frame update
    public void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        _months = _root.Query<Label>().ToList();
    }

    public void ResetLabels()
    {
        _months.ForEach(label => label.RemoveFromClassList("month-available"));
    }

    public void SetAvailability(string[] availableMonths)
    {
        for (int i = 0; i < availableMonths.Count() || i < _months.Count; i++)
        {
            if (availableMonths[i] != "NA")
            {
                if (!_months[i].ClassListContains("month-available"))
                    _months[i].AddToClassList("month-available");
            }
        }
    }
}
