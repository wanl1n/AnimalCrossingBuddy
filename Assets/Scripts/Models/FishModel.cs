using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class FishModel : AvailabilityModel
{
    [JsonProperty("Critterpedia Image Link")]
    public string CritterpediaImage { get; set; }

    [JsonProperty("Sell Price")]
    public int SellPrice { get; set; }

    [JsonProperty("Location")]
    public string Location { get; set; }

    [JsonProperty("Shadow Size")]
    public string ShadowSize { get; set; }

    [JsonProperty("Catch Difficulty")]
    public string CatchDifficulty { get; set; }

    [JsonProperty("Total Catches")]
    public int TotalCatches { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }

    public static IEnumerator GetFish(string id, string table, System.Action<FishModel> fish)
    {
        WWWForm form = new();

        string[] idOnly = id.Split('\t');

        form.AddField("table", table.ToLower());

        form.AddField("condition", "Id = " + idOnly[0]);

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getModelData.php", form);
        yield return handler.SendWebRequest();

        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0].Contains("0"))
        {
            FishModel returnFish = JsonConvert.DeserializeObject<FishModel>(result[1]);
            fish(returnFish);
        }
        else 
        { 
            Debug.LogError("GetModelData failed. [ERROR] : " + handler.error); 
        }
    }

    
}
