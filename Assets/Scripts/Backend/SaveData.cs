using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    public void StartSaveData()
    {
        //if (this.VerifyInformation())
        //    StartCoroutine(this.SaveUserData());
        //else
        //    Debug.Log("Username Invalid.");
    }

    //private IEnumerator SaveUserData()
    //{
        //WWWForm form = new WWWForm();

        //form.AddField("username", DatabaseManager.GetInstance().Username);

        //WWW handler = new("http://localhost/sqlconnect/savedata.php", form);

        //yield return handler;

        //string[] result = handler.text.Split('\t');

        //if (result[0] == "0")
        //{
        //    Debug.Log("User score successfully incremented. New Score: " + result[1]);
        //    DatabaseManager.GetInstance().Score = int.Parse(result[1]);
        //}
        //else { Debug.LogError("Failed to add score. [ERROR] : " + handler.text); }
    //}

    private bool VerifyInformation()
    {
        string username = DatabaseManager.GetInstance().Username;

        if (username.Length > 0)
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

    public static SaveData GetInstance()
    {
        if (Instance == null)
        {
            Instance = new SaveData();
        }
        return Instance;
    }
}