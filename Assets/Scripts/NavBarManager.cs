using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NavBarManager : MonoBehaviour
{
    private VisualElement _root;
    private List<Button> _navButtons = new();

    // Start is called before the first frame update
    void Start()
    {
        this._root = GetComponent<UIDocument>().rootVisualElement;
        this._navButtons.Add(this._root.Q<Button>("NBVillagersButton"));
        this._navButtons.Add(this._root.Q<Button>("NBFishButton"));
        this._navButtons.Add(this._root.Q<Button>("NBBugsButton"));
        this._navButtons.Add(this._root.Q<Button>("NBSeasButton"));
        this._navButtons.Add(this._root.Q<Button>("NBMenuButton"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
