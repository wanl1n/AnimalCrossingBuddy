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

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getIdNameColumnData.php", form);
        yield return handler.SendWebRequest();

        // Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            List<StringModel> strings = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                StringModel data = JsonConvert.DeserializeObject<StringModel>(result[i]);
                strings.Add(data);
            }

            callback(strings);
        }
        else { Debug.LogError("GetColumnData failed. [ERROR] : " + handler.error); }
    }

    public IEnumerator DownloadTexture(string url, System.Action<Texture2D> callback = null)
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
                name = link.Id + "\t" + link.Name
            };
            newIcon.AddToClassList(className);
            newIcon.style.backgroundImage = new StyleBackground(icon);

            newIcon.RegisterCallback<ClickEvent, string>(Clicked, table);

            if (parent != null)
            {
                bool alreadyAdded = false;
                foreach (var child in parent.Children())
                {
                    if (child.name == newIcon.name)
                    {
                        alreadyAdded = true;
                        break;
                    }
                }
                if (!alreadyAdded)
                {
                   parent.Add(newIcon);
                }
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
        GameObject iconPopUpManager = GameObject.FindGameObjectWithTag("Icon Manager");

        switch (table)
        {
            case "Fish":
                FishModel fish = new();
                yield return StartCoroutine(FishModel.GetFish(id, table, c => fish = c));
                // show the popup
                if (iconPopUpManager != null)
                {
                    StartCoroutine(iconPopUpManager.GetComponent<IconPopUpGUIManager>().LoadFishData(fish));
                }
                break;
            case "Insects":
                InsectModel insect = new();
                yield return StartCoroutine(InsectModel.GetInsect(id, table, c => insect = c));
                // show the popup
                if (iconPopUpManager != null)
                {
                    StartCoroutine(iconPopUpManager.GetComponent<IconPopUpGUIManager>().LoadInsectData(insect));
                }
                break;
            case "Sea_creatures":
                SeaCreatureModel seaCreature = new();
                yield return StartCoroutine(SeaCreatureModel.GetSeaCreature(id, table, c => seaCreature = c));
                // show the popup
                if (iconPopUpManager != null)
                {
                    StartCoroutine(iconPopUpManager.GetComponent<IconPopUpGUIManager>().LoadSeaCreatureData(seaCreature));
                }
                break;
            case "Villagers":
                VillagerModel villager = new();
                yield return StartCoroutine(VillagerModel.GetVillager(id, table, c => villager = c));
                // show the popup
                if (iconPopUpManager != null)
                {
                    StartCoroutine(iconPopUpManager.GetComponent<IconPopUpGUIManager>().LoadVillagerData(villager));
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
                
                List<StringModel> villagerStringModels = new List<StringModel>();

                foreach (var model in villagerModels)
                {   
                    if (parent != null)
                    {
                        villagerStringModels.Add(new StringModel(model.Id, model.Name, model.PhotoImage));
                    }

                }

                if (parent != null)
                {
                    List<VisualElement> childToRemove = new List<VisualElement>(); 

                    foreach (var child in parent.Children())
                    {
                        bool inStringModel = false;
                        foreach (var model in villagerStringModels)
                        {
                            string modelIdName = model.Id + "\t" + model.Name;
                            if (child.name == modelIdName)
                            {
                                inStringModel = true;
                                break;
                            }
                        }
                        if (!inStringModel)
                        {
                            childToRemove.Add(child);
                        }
                    }

                    foreach (var child in childToRemove)
                    {
                        parent.Remove(child);  
                    }
                }

                yield return StartCoroutine(CreatePortraits(villagerStringModels, parent, table, "nowPortraits"));
                break;

            case "Fish":
            case "Insects":
            case "Sea_creatures":
                List<StringModel> stringModels = new List<StringModel>();
                yield return StartCoroutine(this.GetAvailableModelData(table, c => stringModels = c));

                if (parent != null)
                {
                    List<VisualElement> childToRemove = new List<VisualElement>();

                    foreach (var child in parent.Children())
                    {
                        bool inStringModel = false;
                        foreach (var model in stringModels)
                        {
                            string modelIdName = model.Id + "\t" + model.Name;
                            if (child.name == modelIdName)
                            {
                                inStringModel = true;
                                break;
                            }
                        }
                        if (!inStringModel)
                        {
                            childToRemove.Add(child);
                        }
                    }

                    foreach (var child in childToRemove)
                    {
                        parent.Remove(child);
                    }
                }

                yield return StartCoroutine(CreatePortraits(stringModels, parent, table));

                break;
        }

    }

    private IEnumerator GetAvailableModelData(string table, System.Action<List<StringModel>> models)
    {
        WWWForm form = new();

        form.AddField("table", table.ToLower());

        string currentMonth = "";

        if (TimeManager.GetInstance().IsInSouthernHemisphere)
        {
            currentMonth = "SH ";
        }
        else
        {
            currentMonth = "NH ";
        }

        currentMonth += TimeManager.GetInstance().PlayerTime.ToString("MMM");

        form.AddField("currentMonth", currentMonth);

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getAvailableModelData.php", form);
        yield return handler.SendWebRequest();

        //Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0].Contains("0"))
        {
            List<StringModel> strings = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                AvailabilityModel data = JsonConvert.DeserializeObject<AvailabilityModel>(result[i]);

                string timeAvailability = (string)data.GetType().GetProperty(currentMonth.Replace(" ", "")).GetValue(data);

                if (data.isAvailableNow(timeAvailability))
                    strings.Add(new StringModel(data.Id, data.Name, data.IconImage));

            }

            models(strings);


        }
        else 
        { 
            Debug.LogError("GetAvailableData failed. [ERROR] : " + handler.error); 
        }
    }

    public IEnumerator GetModelData(string table, System.Action<List<StringModel>> models)
    {
        GameObject contentListManager = GameObject.FindGameObjectWithTag("Scene Document");
        if (contentListManager != null)
        {
            WWWForm form = new();

            string name = contentListManager.GetComponent<ContentListGUIManager>().SearchBarText;

            form.AddField("table", table.ToLower());

            form.AddField("condition", "name LIKE \"%" + name + "%\"");

            using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getModelData.php", form);
            yield return handler.SendWebRequest();

            string[] result = handler.downloadHandler.text.Split('\t');


            Debug.Log(handler.downloadHandler.text);

            if (result[0].Contains("0"))
            {
                List<StringModel> strings = new();

                for (int i = 1; i < result.Length; i++)
                {
                    BaseModel model = JsonConvert.DeserializeObject<BaseModel>(result[i]);

                    if (model != null)
                        strings.Add(new StringModel(model.Id, model.Name, model.IconImage));
                }

                models(strings);
            }
            else
            {
                Debug.LogError("GetModelData failed. [ERROR] : " + handler.error);
            }

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
