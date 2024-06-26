using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class VillagerModel : BaseModel
{
    
    [JsonProperty("Photo Image Link")]
    public string PhotoImage { get; set; }

    [JsonProperty("Species")]
    public string Species { get; set; }

    [JsonProperty("Gender")]
    public string Gender { get; set; }

    [JsonProperty("Personality")]
    public string Personality { get; set; }

    [JsonProperty("Subtype")]
    public string Subtype { get; set; }

    [JsonProperty("Hobby")]
    public string Hobby { get; set; }

    [JsonProperty("Birthday")]
    public string Birthday { get; set; }

    [JsonProperty("Catchphrase")]
    public string Catchphrase { get; set; }

    [JsonProperty("Favorite Song")]
    public string FavoriteSong { get; set; }

    [JsonProperty("Favorite Saying")]
    public string FavoriteSaying { get; set; }

    [JsonProperty("Name Color")]
    public string NameColor { get; set; }

    [JsonProperty("Bubble Color")]
    public string BubbleColor { get; set; }

    public static IEnumerator GetVillager(string id, string table, System.Action<VillagerModel> villager)
    {
        WWWForm form = new();

        string[] nameOnly = id.Split('\t');

        form.AddField("table", table.ToLower());

        form.AddField("condition", "Name = \"" + nameOnly[1] + "\"");

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getModelData.php", form);
        yield return handler.SendWebRequest();

        Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0].Contains("0"))
        {
            VillagerModel returnVillager = JsonConvert.DeserializeObject<VillagerModel>(result[1]);
            villager(returnVillager);
        }
        else { Debug.LogError("GetModelData failed. [ERROR] : " + handler.error); }
    }
    public static IEnumerator GetVillagerBirthday(string birthday, System.Action<List<VillagerModel>> villagers)
    {
        WWWForm form = new();

        form.AddField("birthday", birthday);

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getBirthdayData.php", form);
        yield return handler.SendWebRequest();

        string[] result = handler.downloadHandler.text.Split('\t');

        if (result[0].Contains("0"))
        {
            List<VillagerModel> returnedVillagers = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                VillagerModel returnedVillager = JsonConvert.DeserializeObject<VillagerModel>(result[i]);
                returnedVillagers.Add(returnedVillager);
            }

            villagers(returnedVillagers);
        }
    }
}
