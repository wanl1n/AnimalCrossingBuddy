using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    private static Login Instance;
    
    public void StartLoginUser()
    {
        if (this.VerifyInformation())
            StartCoroutine(this.LoginUser());
        else
            Debug.Log("Username or Password Invalid.");
    }

    private IEnumerator LoginUser()
    {
        WWWForm form = new WWWForm();

        form.AddField("username", LogInPopUpGUIManager.GetInstance().Username);
        form.AddField("password", LogInPopUpGUIManager.GetInstance().Password);

        using (UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/accounts/login.php", form))
        {
            yield return handler.SendWebRequest();

            string[] result = handler.downloadHandler.text.Split('\t');

            if (result[0] == "0")
            {
                Debug.Log("User successfully logged in.");
                DatabaseManager.GetInstance().Username = LogInPopUpGUIManager.GetInstance().Username;
                LogInPopUpGUIManager.GetInstance().ClosePopUp();
                SceneManager.LoadScene("UserScene");
            }
            else { 
                Debug.LogError("Failed to login user. [ERROR] : " + handler.downloadHandler.text);
                LogInPopUpGUIManager.GetInstance().PasswordField.value = "Failed to login user!";
            }
        }
    }

    private bool VerifyInformation()
    {
        string username = LogInPopUpGUIManager.GetInstance().Username;
        string password = LogInPopUpGUIManager.GetInstance().Password;
        
        if (username.Length > 0 && password.Length > 0)
            return true;
        return false;
    }   

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public static Login GetInstance()
    {
        if (Instance == null)
        {
            Instance = new Login();
        }
        return Instance;
    }
}
