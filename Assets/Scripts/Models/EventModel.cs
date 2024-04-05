using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class EventModel
{
    [JsonProperty("Name")]
    public string Name { get; set; }

    [JsonProperty("Icon Image Link")]
    public string IconImage { get; set; }

    [JsonProperty("Type")]
    public string Type { get; set; }

    [JsonProperty("Display Name")]
    public string DisplayName { get; set; }

    [JsonProperty("Sort Date")]
    public string SortDate { get; set; }

    [JsonProperty("Start Time")]
    public string StartTime { get; set; }

    [JsonProperty("End Time")]
    public string EndTime { get; set; }
    
    [JsonProperty("Year")]
    public string Year { get; set; }

    [JsonProperty("Start Date")]
    public string StartDate { get; set; }

    [JsonProperty("End Date")]
    public string EndDate { get; set; }

    public static string AddOrdinal(int num)
    {
        if (num <= 0) return num.ToString();

        switch (num % 100)
        {
            case 11:
            case 12:
            case 13:
                return num + "th";
        }

        switch (num % 10)
        {
            case 1:
                return num + "st";
            case 2:
                return num + "nd";
            case 3:
                return num + "rd";
            default:
                return num + "th";
        }
    }

    public static IEnumerator GetCurrentEvents(DateTime datetime, bool isSouthernHemisphere, System.Action<List<EventModel>> events)
    {
        WWWForm form = new();

        CultureInfo.CurrentCulture = new("en-US");
        form.AddField("datetime", datetime.ToString("G"));

        form.AddField("isSouthernHemisphere", isSouthernHemisphere.ToString());

        using UnityWebRequest handler = UnityWebRequest.Post("http://localhost/sqlconnect/AnimalCrossingBuddy/getCurrentEvents.php", form);
        yield return handler.SendWebRequest();

        Debug.Log(handler.downloadHandler.text);
        string[] result = handler.downloadHandler.text.Split('\t');
        if (result[0].Contains("0"))
        {
            List<EventModel> returnEvents = new();

            for (int i = 1; i < result.Length - 1; i++)
            {
                EventModel data = JsonConvert.DeserializeObject<EventModel>(result[i]);
                returnEvents.Add(data);
            }

            // List<EventModel> removeDupes = returnEvents.Distinct().ToList(); 

            // removeDupes.ForEach(e => Debug.Log(e.Name));
            events(returnEvents);
        }
        else
        {
            Debug.LogError("GetCurrentEvents failed. [ERROR] : " + handler.error);
        }
    }
}
