using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class DatabaseManager : MonoBehaviour
{
    private static DatabaseManager _instance;

    public IEnumerator GetColumnData(string column, string table, System.Action<List<StringModel>> callback)
    {

        WWWForm form = new();
        form.AddField("column", column);
        form.AddField("table", table);

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getIdAndColumnData.php", form);
        yield return handler.SendWebRequest();

        Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            List<StringModel> urls = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                StringModel link = JsonConvert.DeserializeObject<StringModel>(result[i]);
                urls.Add(link);
                // Debug.Log(result[i]);
            }

            callback(urls);
        }
        else { Debug.LogError("GetColumnData failed. [ERROR] : " + handler.error); }
    }

    public IEnumerator DownloadTexture(string url, System.Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("[ERROR]: DownloadTexture request failed!", this);
        }
        else
        {
            DownloadHandlerTexture response = (DownloadHandlerTexture)request.downloadHandler;
            callback(response.texture);
        }
    }

    public IEnumerator CreatePortraits(List<StringModel> urls, VisualElement parent)
    {
        // List<VisualElement> list = new();

        foreach (var link in urls)
        {
            Texture2D icon = new(0, 0);

            yield return StartCoroutine(DownloadTexture(link.Data, (tex) => icon = tex));

            VisualElement newIcon = new()
            {
                name = link.Id
            };
            newIcon.AddToClassList("portraits");
            newIcon.style.backgroundImage = new StyleBackground(icon);

            newIcon.RegisterCallback<ClickEvent, VisualElement>(Clicked, newIcon);
            // list.Add(newIcon);
            parent.Add(newIcon);
        }

        // callback(list);
    }

    private void Clicked(ClickEvent evt, VisualElement elementClicked)
    {
        // get data 
        // show the popup
        
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public static DatabaseManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new();
        }
        return _instance;
    }
}
