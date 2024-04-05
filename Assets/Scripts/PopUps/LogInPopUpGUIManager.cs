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
    public TextField PasswordField { get { return _passwordField; } }
    private Button _confirmButton;
    private Button _closeButton;
    private Button _fineTextButton;

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

        this._closeButton = this._root.Q<Button>("CloseButton");
        this._confirmButton = this._root.Q<Button>("ConfirmButton");
        this._fineTextButton = this._root.Q<Button>("FineTextButton");

        //this._transparentBG.RegisterCallback<ClickEvent>(this.ClosePopUp);
        this._closeButton.RegisterCallback<ClickEvent>(ClosePopUp);
        this._confirmButton.RegisterCallback<ClickEvent>(OnLoginClick);
        this._fineTextButton.RegisterCallback<ClickEvent>(OnRegisterClick);
    }

    private void OnLoginClick(EventBase evt)
    {
        Debug.Log("LOGIN");
        Login.GetInstance().StartLoginUser();
    }
    private void OnRegisterClick(EventBase evt)
    {
        Debug.Log("REGISTER");
        RegisterPopUpGUIManager.GetInstance().OpenPopUp();
        this.ClosePopUp();
    }

    public void ClosePopUp(EventBase evt)
    {
        this.ClosePopUp();
    }

    public void ClosePopUp()
    {
        this._transparentBG.style.display = DisplayStyle.None;
    }
    public void OpenPopUp(EventBase evt)
    {
        this.OpenPopUp();
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
