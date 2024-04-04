using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartManager : MonoBehaviour
{
    private VisualElement _root;
    private Button _startButton;
    private Image _adminButton;


    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._startButton = this._root.Q<Button>("StartButton");
        this._adminButton = this._root.Q<Image>("AdminButton");


        this._startButton.clickable.clicked += this.OnStartButtonClick;
        this._adminButton.RegisterCallback<ClickEvent>(this.OnAdminButtonClick);

        if (!PlayerPrefs.HasKey("CreatedDatabase"))
        {
            Debug.Log("Creating Database");
            StartCoroutine(DatabaseManager.GetInstance().CreateMainDatabase());
            PlayerPrefs.SetInt("CreatedDatabase", 1);
        }
        else
        {
            Debug.Log("Already Initialized Main Database");
        }
    }

    private void OnStartButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }

    private void OnAdminButtonClick(ClickEvent e)
    {
        SceneManager.LoadScene("AdminLoginScene");
    }




}
