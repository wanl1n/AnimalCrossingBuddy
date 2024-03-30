using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public IEnumerator CreatePortraits(List<StringModel> urls, VisualElement parent, string table, string className = "portraits")
    {


        foreach (var link in urls)
        {
            Texture2D icon = new(0, 0);

            yield return StartCoroutine(DownloadTexture(link.Data, (tex) => icon = tex));

            VisualElement newIcon = new()
            {
                name = link.Id
            };
            newIcon.AddToClassList(className);
            newIcon.style.backgroundImage = new StyleBackground(icon);

            newIcon.RegisterCallback<ClickEvent, string>(Clicked, table);

            if (parent != null)
            {
                parent.Add(newIcon);

                if (parent.name.Contains("CurrentList"))
                    Debug.Log(newIcon.name);
            }
            
        }
    }

    private void Clicked(ClickEvent evt, string table)
    {
        VisualElement target = evt.target as VisualElement;
        StartCoroutine(StartShowModelData(target.name, table));
    }

    private IEnumerator StartShowModelData(string id, string table)
    {
        // get data 
        // query using evt.target name as the Id, and table parameter
        BaseModel model = new();
        GameObject contentListManager = GameObject.FindGameObjectWithTag("Scene Document");

        switch (table)
        {
            case "Fish":
                FishModel fish = new();
                yield return StartCoroutine(FishModel.GetFish(id, table, c => fish = c));
                // show the popup
                if (contentListManager != null)
                {
                    StartCoroutine(contentListManager.GetComponent<ContentListGUIManager>().LoadFishData(fish));
                }
                break;
            case "Insects":
                InsectModel insect = new();
                yield return StartCoroutine(InsectModel.GetInsect(id, table, c => insect = c));
                // show the popup
                if (contentListManager != null)
                {
                    StartCoroutine(contentListManager.GetComponent<ContentListGUIManager>().LoadInsectData(insect));
                }
                break;
            case "Sea_creatures":
                SeaCreatureModel seaCreature = new();
                yield return StartCoroutine(SeaCreatureModel.GetSeaCreature(id, table, c => seaCreature = c));
                // show the popup
                if (contentListManager != null)
                {
                    StartCoroutine(contentListManager.GetComponent<ContentListGUIManager>().LoadSeaCreatureData(seaCreature));
                }
                break;
            case "Villagers":
                VillagerModel villager = new();
                yield return StartCoroutine(VillagerModel.GetVillager(id, table, c => villager = c));
                // show the popup
                if (contentListManager != null)
                {
                    StartCoroutine(contentListManager.GetComponent<ContentListGUIManager>().LoadVillagerData(villager));
                }
                break;
        }

        

    }

    public IEnumerator CreateNowPortraits(VisualElement parent, string table)
    {
        switch (table)
        {
            case "Villagers":
                List<VillagerModel> villagerModels = new List<VillagerModel>();
                yield return StartCoroutine(VillagerModel.GetVillagerBirthday(TimeManager.GetInstance().GetMDString(), v => villagerModels = v));
                
                List<StringModel> urls = new List<StringModel>();

                foreach (var model in villagerModels)
                {   
                    if (parent != null)
                    {
                        urls.Add(new StringModel(model.Id, model.PhotoImage));
                    }

                }

                yield return StartCoroutine(CreatePortraits(urls, parent, table, "nowPortraits"));


                break;
        }

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
