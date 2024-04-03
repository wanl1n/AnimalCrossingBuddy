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
    }

    void OnStartButtonClick()
    {
        SceneManager.LoadScene("HomeScene");
    }    
}
