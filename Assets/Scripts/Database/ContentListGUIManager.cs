using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class ContentListGUIManager : MonoBehaviour
{
    [SerializeField]
    private string _table;

    private VisualElement _root;
    private VisualElement _listParent;

    private VisualElement _currentParent;
    private Label _currentText;

    private TextField _searchBarText;
    public string SearchBarText
    {
        get { return this._searchBarText.value.Trim();  }
    }


    // Start is called before the first frame update
    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._listParent = this._root.Q<VisualElement>(className: "catchable-list");
        
        this._currentParent = this._root.Q<VisualElement>("CurrentList");
        this._currentText = this._root.Q<Label>("CurrentText");

        this._searchBarText = this._root.Q<TextField>("SearchBar");

        this._searchBarText.RegisterValueChangedCallback(this.OnSearchBarValueChanged);

        StartCoroutine(LoadIcons());
    }

    private IEnumerator LoadIcons()
    {
        List<StringModel> iconLinks = new();
        
        LoadingGUIManager.GetInstance().ShowLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetColumnData("Icon Image Link", this._table.ToLower(), c => iconLinks = c)); 

        if (this._currentParent != null) 
            yield return StartCoroutine(this.LoadNowPortrait());
        LoadingGUIManager.GetInstance().HideLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, this._listParent, this._table));
    }

    public IEnumerator LoadNowPortrait()
    {
        yield return StartCoroutine(DatabaseManager.GetInstance().CreateNowPortraits(this._currentParent, this._table));
        if (this._currentParent.childCount != 0)
        {   
            this._currentText.style.display = DisplayStyle.None;
        }
        else
        {
            this._currentText.style.display = DisplayStyle.Flex;
        }
    }

    private IEnumerator LoadSearchedName()
    {
        List<StringModel> iconLinks = new();
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(this._table.ToLower(), c => iconLinks = c));
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, this._listParent, this._table));

        List<VisualElement> childToRemove = new List<VisualElement>();

        foreach (var child in this._listParent.Children())
        {
            bool inStringModel = false;
            foreach (var model in iconLinks)
            {
                string modelIdName = model.Id + "\t" + model.Name;
                if (child.name == modelIdName)
                {
                    inStringModel = true;
                    break;
                }
            }
            if (!inStringModel)
            {
                childToRemove.Add(child);
            }
        }

        foreach (var child in childToRemove)
        {
            this._listParent.Remove(child);
        }

    }

    private void OnSearchBarValueChanged(ChangeEvent<string> e)
    {
        this._listParent.Clear();
        StopCoroutine(LoadIcons());
        DatabaseManager.GetInstance().StopAllCoroutines();
        StartCoroutine(LoadSearchedName());
    }

    public IEnumerator ReloadAll()
    {
        this._listParent.Clear();

        List<StringModel> stringModels = new();
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(this._table.ToLower(), c => stringModels = c));

        if (this._listParent != null)
        {
            List<VisualElement> childToRemove = new List<VisualElement>();

            foreach (var child in this._listParent.Children())
            {
                bool inStringModel = false;
                foreach (var model in stringModels)
                {
                    string modelIdName = model.Id + "\t" + model.Name;
                    if (child.name == modelIdName)
                    {
                        inStringModel = true;
                        break;
                    }
                }
                if (!inStringModel)
                {
                    childToRemove.Add(child);
                }
            }

            foreach (var child in childToRemove)
            {
                this._listParent.Remove(child);
            }
        }


        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(stringModels, this._listParent, this._table));

    }

}
