using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AdminHomeGUIManager : MonoBehaviour
{
    private VisualElement _root;

    private Image _logOutButton;

    private Button _resetDatabase;


    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        
        this._logOutButton = this._root.Q<Image>("LogOutButton");

        this._resetDatabase = this._root.Q<Button>("ResetDatabase");

        this._logOutButton.RegisterCallback<ClickEvent>(this.OnLogOutButtonClick);

        this._resetDatabase.clickable.clicked += this.OnResetDatabaseClicked;
    }

    private void OnLogOutButtonClick(ClickEvent e)
    {
        SceneManager.LoadScene("StartScene");
    }

    private void OnResetDatabaseClicked()
    {
        StartCoroutine(DatabaseManager.GetInstance().CreateMainDatabase());
    }
}
