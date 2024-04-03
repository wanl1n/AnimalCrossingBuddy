using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AdminLoginManager : MonoBehaviour
{
    private VisualElement _root;
    private Button _backButton;
    private Button _loginButton;
    private TextField _adminCode;

    // Start is called before the first frame update
    void Start()
    {
        this._root = this.GetComponent<UIDocument>().rootVisualElement;
        this._backButton = this._root.Q<Button>("BackButton");
        this._loginButton = this._root.Q<Button>("LoginButton");
        this._adminCode = this._root.Q<TextField>("AdminAccessCode");

        this._backButton.clickable.clicked += this.OnBackButtonClick;
        this._loginButton.clickable.clicked += this.OnLoginButtonClick;
    }

    private void OnBackButtonClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    private void OnLoginButtonClick()
    {
        StartCoroutine(this.LoginUser());
    }

    private IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();

        form.AddField("accessCode", this._adminCode.value.Trim());

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/adminLogin.php", form))
        {
            yield return handler.SendWebRequest();

            string result = handler.downloadHandler.text;

            Debug.Log(result);

            if (result.Contains("0"))
            {
                Debug.Log("User successfully logged in.");
            }
            else 
            {
                this._adminCode.value = "ACCESS DENIED!!!";
            }
        }
    }
}
