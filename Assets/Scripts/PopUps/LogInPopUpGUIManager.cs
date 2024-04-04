using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LogInPopUpGUIManager : MonoBehaviour
{
    private static LogInPopUpGUIManager Instance;

    private VisualElement _root;
    private VisualElement _transparentBG;
    private VisualElement _popUpBG;

    private TextField _usernameField;
    public string Username { get { return _usernameField.value; } }

    private TextField _passwordField;
    public string Password { get { return _passwordField.value; } }

    private Button _logInButton;
    private Button _cancelButton;
    private Button _registerButton;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._transparentBG = this._root.Q<VisualElement>("TransparentBG");
        this._popUpBG = this._transparentBG.Q<VisualElement>("PopUpBG");

        this._usernameField = this._root.Q<TextField>("Username");
        this._passwordField = this._root.Q<TextField>("Password");

        this._logInButton = this._root.Q<Button>("LogInButton");
        this._cancelButton = this._root.Q<Button>("CancelButton");
        this._registerButton = this._root.Q<Button>("RegisterButton");

        this._transparentBG.RegisterCallback<ClickEvent>(this.ClosePopUp);
        this._logInButton.RegisterCallback<ClickEvent>(OnLoginClick);
        this._cancelButton.RegisterCallback<ClickEvent>(OnCancelClick);
        this._registerButton.RegisterCallback<ClickEvent>(OnRegisterClick);
    }

    private void OnLoginClick(EventBase evt)
    {
        Debug.Log("LOGIN");
        Login.GetInstance().StartLoginUser();
    }

    private void OnCancelClick(EventBase evt)
    {
        Debug.Log("CANCEL LOGIN");
        this.ClosePopUp(evt);
    }

    private void OnRegisterClick(EventBase evt)
    {
        Debug.Log("REGISTER");

    }

    public void ClosePopUp(EventBase evt)
    {
        this.ClosePopUp();
    }

    public void ClosePopUp()
    {
        this._transparentBG.style.display = DisplayStyle.None;
    }

    public void OpenPopUp()
    {
        this._transparentBG.style.display = DisplayStyle.Flex;
    }

    public static LogInPopUpGUIManager GetInstance()
    {
        if (Instance == null)
        {
            Instance = new LogInPopUpGUIManager();
        }
        return Instance;
    }
}
