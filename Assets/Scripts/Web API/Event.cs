using Newtonsoft.Json;

public class Event
{
    [JsonProperty("event")]
    public string Name { get; set; }

    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("url")]
    public string URL { get; set; }
}