using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class SeaCreatureModel : AvailabilityModel
{
    [JsonProperty("Critterpedia Image Link")]
    public string CritterpediaImage { get; set; }

    [JsonProperty("Sell Price")]
    public int SellPrice { get; set; }

    [JsonProperty("Shadow Size")]
    public string ShadowSize { get; set; }

    [JsonProperty("Movement Speed")]
    public string MovementSpeed { get; set; }

    [JsonProperty("Total Catches")]
    public int TotalCatches { get; set; }

    public static IEnumerator GetSeaCreature(string id, string table, System.Action<SeaCreatureModel> seaCreature)
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
            SeaCreatureModel returnSeaCreature = JsonConvert.DeserializeObject<SeaCreatureModel>(result[1]);
            seaCreature(returnSeaCreature);
        }
        else { Debug.LogError("GetModelData failed. [ERROR] : " + handler.error); }
    }
}
