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

        // Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            List<StringModel> strings = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                StringModel idAndData = JsonConvert.DeserializeObject<StringModel>(result[i]);
                strings.Add(idAndData);
                // Debug.Log(result[i]);
            }

            callback(strings);
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

    public IEnumerator CreatePortraits(List<StringModel> urls, VisualElement parent, string table)
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

            // newIcon.RegisterCallback<MouseOverEvent>(Hover, newIcon);
            newIcon.RegisterCallback<ClickEvent, string>(Clicked, table);
            parent.Add(newIcon);
        }
    }

    private void Clicked(ClickEvent evt, string table)
    {
        VisualElement target = evt.target as VisualElement;
        StartCoroutine(StartGetModelData(target.name, table));
    }

    private IEnumerator StartGetModelData(string id, string table)
    {
        // get data 
        // query using evt.target name as the Id, and table parameter
        switch (table)
        {
            case "fish":
                FishModel fish;
                yield return StartCoroutine(FishModel.GetFish(id, table, c => fish = c));
                break;
            case "insect":
                InsectModel insect;
                yield return StartCoroutine(InsectModel.GetInsect(id, table, c => insect = c));
                break;
            case "sea_creatures":
                SeaCreatureModel seaCreature;
                yield return StartCoroutine(SeaCreatureModel.GetSeaCreature(id, table, c => seaCreature = c));
                break;
            case "Villager":
                VillagerModel villager;
                yield return StartCoroutine(VillagerModel.GetVillager(id, table, c => villager = c));
                break;
        }

        
        // show the popup
        // evt.target is the visualelement that was clicked
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
