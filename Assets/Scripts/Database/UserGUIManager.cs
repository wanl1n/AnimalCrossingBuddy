using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class UserGUIManager : MonoBehaviour
{
    [SerializeField]
    private string _collectedTable;
    [SerializeField]
    private string _caughtTable;

    private VisualElement _root;
    private VisualElement _collectedListParent;
    private VisualElement _caughtListParent;

    private Button _usernameLabel;
    private Button _usernameOptions;

    private VisualElement _loggedInOptions;
    private Button _logOutButton;
    private Button _deleteButton;

    private VisualElement _loggedOutOptions;
    private Button _logInButton;
    private Button _registerButton;

    private TextField _searchBarLeftText;
    public string SearchBarLeftText
    {
        get { return this._searchBarLeftText.value.Trim();  }
    }

    private TextField _searchBarRightText;
    public string SearchBarRightText
    {
        get { return this._searchBarRightText.value.Trim(); }
    }


    // Start is called before the first frame update
    private void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._collectedListParent = this._root.Q<VisualElement>("Table1");
        this._caughtListParent = this._root.Q<VisualElement>("Table2");

        this._usernameLabel = this._root.Q<Button>("Username");
        this._usernameOptions = this._root.Q<Button>("UserOptions");

        this._loggedInOptions = this._root.Q<VisualElement>("LoggedInButtonContainer");
        this._logOutButton = this._root.Q<Button>("LogOutButton");
        this._deleteButton = this._root.Q<Button>("DeleteButton");
        this._loggedOutOptions = this._root.Q<VisualElement>("LoggedOutButtonContainer");
        this._logInButton = this._root.Q<Button>("LogInButton");
        this._registerButton = this._root.Q<Button>("RegisterButton");


        this._searchBarLeftText = this._root.Q<TextField>("SearchBarLeft");
        this._searchBarRightText = this._root.Q<TextField>("SearchBarRight");

        this._usernameOptions.RegisterCallback<ClickEvent>(this.OpenUserOptions);
        this._logOutButton.RegisterCallback<ClickEvent>(this.LogOut);
        this._deleteButton.RegisterCallback<ClickEvent>(this.DeleteData);
        this._logInButton.RegisterCallback<ClickEvent>(this.LogIn);
        this._registerButton.RegisterCallback<ClickEvent>(this.Register);
        this._searchBarLeftText.RegisterValueChangedCallback(this.OnSearchBarLeftValueChanged);
        this._searchBarRightText.RegisterValueChangedCallback(this.OnSearchBarRightValueChanged);

        StartCoroutine(LoadIcons());
    }

    private void Update()
    {
        this._usernameLabel.text = "Welcome, " + DatabaseManager.GetInstance().Username + "!";
    }

    private void OpenUserOptions(EventBase evt)
    {
        if (DatabaseManager.GetInstance().LoggedIn())
        {
            this._loggedOutOptions.style.display = DisplayStyle.None;

            if (this._loggedInOptions.style.display == DisplayStyle.None)
                this._loggedInOptions.style.display = DisplayStyle.Flex;
            else
                this._loggedInOptions.style.display = DisplayStyle.None;
        }
        else
        {
            this._loggedInOptions.style.display = DisplayStyle.None;

            if (this._loggedOutOptions.style.display == DisplayStyle.None)
                this._loggedOutOptions.style.display = DisplayStyle.Flex;
            else
                this._loggedOutOptions.style.display = DisplayStyle.None;
        }
    }

    private void CloseUserOptions()
    {
        this._loggedOutOptions.style.display = DisplayStyle.None;
        this._loggedInOptions.style.display = DisplayStyle.None;
    }

    private void LogIn(EventBase evt)
    {
        LogInPopUpGUIManager.GetInstance().OpenPopUp();
        this.CloseUserOptions();
    }

    private void Register(EventBase evt)
    {
        RegisterPopUpGUIManager.GetInstance().OpenPopUp();
        this.CloseUserOptions();
    }

    private void LogOut(EventBase evt)
    {
        DatabaseManager.GetInstance().LogOut();
        this.CloseUserOptions();
    }

    private void DeleteData(EventBase evt)
    {
        Debug.Log("DELETE DATA");
        this.CloseUserOptions();
    }

    private IEnumerator LoadIcons()
    {
        List<StringModel> collectedIconLinks = new();
        List<StringModel> caughtIconLinks = new();
        
        LoadingGUIManager.GetInstance().ShowLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().GetUserColumnData("Icon Image Link", this._collectedTable.ToLower(), c => collectedIconLinks = c)); 
        yield return StartCoroutine(DatabaseManager.GetInstance().GetUserColumnData("Icon Image Link", this._caughtTable.ToLower(), c => caughtIconLinks = c)); 

        LoadingGUIManager.GetInstance().HideLoading();

        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(collectedIconLinks, this._collectedListParent, this._collectedTable));
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(caughtIconLinks, this._caughtListParent, this._caughtTable));
    }

    private IEnumerator LoadSearchedName(string table, VisualElement listParent)
    {
        List<StringModel> iconLinks = new();
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(table.ToLower(), c => iconLinks = c));
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(iconLinks, listParent, table));

        List<VisualElement> childToRemove = new List<VisualElement>();

        foreach (var child in listParent.Children())
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
            listParent.Remove(child);
        }

    }

    private void OnSearchBarLeftValueChanged(ChangeEvent<string> e)
    {
        this._collectedListParent.Clear();
        StopCoroutine(LoadIcons());
        DatabaseManager.GetInstance().StopAllCoroutines();
        StartCoroutine(LoadSearchedName(this._collectedTable, this._collectedListParent));
    }

    private void OnSearchBarRightValueChanged(ChangeEvent<string> e)
    {
        this._caughtListParent.Clear();
        StopCoroutine(LoadIcons());
        DatabaseManager.GetInstance().StopAllCoroutines();
        StartCoroutine(LoadSearchedName(this._caughtTable, this._caughtListParent));
    }

    public IEnumerator ReloadAll()
    {
        List<StringModel> stringModels = new();
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(this._collectedTable.ToLower(), c => stringModels = c));
        yield return StartCoroutine(DatabaseManager.GetInstance().GetModelData(this._caughtTable.ToLower(), c => stringModels = c));

        if (this._collectedListParent != null)
        {
            List<VisualElement> childToRemove = new List<VisualElement>();

            foreach (var child in this._collectedListParent.Children())
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
                this._collectedListParent.Remove(child);
            }
        }

        if (this._caughtListParent != null)
        {
            List<VisualElement> childToRemove = new List<VisualElement>();

            foreach (var child in this._caughtListParent.Children())
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
                this._caughtListParent.Remove(child);
            }
        }

        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(stringModels, this._collectedListParent, this._collectedTable));
        yield return StartCoroutine(DatabaseManager.GetInstance().CreatePortraits(stringModels, this._caughtListParent, this._caughtTable));
    }

}
