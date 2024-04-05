using Newtonsoft.Json;

public class Event
{
    [JsonProperty("date")]
    public string Date { get; set; }

    [JsonProperty("year")]
    public string Year { get; set; }

    [JsonProperty("month")]
    public string Month { get; set; }

    [JsonProperty("day")]
    public string Day { get; set; }
}