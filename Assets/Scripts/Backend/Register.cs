using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour {
    public static Register Instance;

    public void StartRegisterUser() 
    {
        if (this.VerifyInformation())
            StartCoroutine(RegisterUser());
        else
            Debug.Log("Username or Password Invalid.");
    }

    private IEnumerator RegisterUser() {
        WWWForm form = new WWWForm();

        form.AddField("username", RegisterPopUpGUIManager.GetInstance().Username);
        form.AddField("password", RegisterPopUpGUIManager.GetInstance().Password);

        WWW handler = new("http://localhost/sqlconnect/AnimalCrossingBuddy/accounts/register.php", form);

        yield return handler;
        if(handler.text == "0")
        {
            Debug.Log("User Successfully Added.");
            RegisterPopUpGUIManager.GetInstance().ClosePopUp();
        }
        else
            Debug.LogError("Failed to Add User. [ERROR] : " + handler.text);
    }

    private bool VerifyInformation() {
        string username = RegisterPopUpGUIManager.GetInstance().Username;
        string password = RegisterPopUpGUIManager.GetInstance().Password;

        Debug.Log("Verifying Username : " + username);
        Debug.Log("Verifying Password : " + password);

        if ((username.Length >= 5 && username.Length <= 20) &&
            password.Length >= 5)
            return true;
        else
            return false;
    }

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public static Register GetInstance() {
        if(Instance == null) {
            Instance = new Register();
        }
        return Instance;
    }
}
