using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class InsectModel : AvailabilityModel
{
    [JsonProperty("Id")]
    public string Id { get; set; }

    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Icon Image Link")]
    public string IconImage { get; set; }

    [JsonProperty("Critterpedia Image Link")]
    public string CritterpediaImage { get; set; }

    [JsonProperty("Sell Price")]
    public int SellPrice { get; set; }

    [JsonProperty("Location")]
    public string Location { get; set; }

    [JsonProperty("Weather")]
    public string Weather { get; set; }

    [JsonProperty("Total Catches")]
    public int TotalCatches { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }

    public static IEnumerator GetInsect(string id, string table, System.Action<InsectModel> insect)
    {
        WWWForm form = new();

        form.AddField("id", id);
        form.AddField("table", table);

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getModelData.php", form);
        yield return handler.SendWebRequest();

        // Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0] == "0")
        {
            InsectModel returnInsect = JsonConvert.DeserializeObject<InsectModel>(result[1]);
            insect(returnInsect);
        }
        else { Debug.LogError("GetModelData failed. [ERROR] : " + handler.error); }
    }
}
