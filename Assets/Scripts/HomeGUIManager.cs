using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HomeGUIManager : MonoBehaviour
{
    private VisualElement _root;

    private VisualElement _navBar;
    private VisualElement _dateTimeScreen;
    private VisualElement _catchableScreen;
    private VisualElement _eventsScreen;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;

        this._navBar = this._root.Q<VisualElement>("NaviBar");
        this._dateTimeScreen = this._root.Q<VisualElement>("DateTimeScren");
        this._catchableScreen = this._root.Q<VisualElement>("CatchableScreen");
        this._eventsScreen = this._root.Q<VisualElement>("EventsScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
