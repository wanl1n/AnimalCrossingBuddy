using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NavBarGUIManager : MonoBehaviour
{
    private VisualElement _root;
    private List<RadioButton> _navButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        this._navButtons.Add(this._root.Q<RadioButton>("NBVillagersButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBFishButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBBugsButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBSeasButton"));
        this._navButtons.Add(this._root.Q<RadioButton>("NBHomeButton"));

        _navButtons.Find(x => x.name == "NBHomeButton").value = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
