using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

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
    
    [JsonProperty("Date Varies By Year")]
    public string DateVariesByYear { get; set; }
    
    [JsonProperty("Start Time")]
    public string StartTime { get; set; }
    
    [JsonProperty("End Time")]
    public string EndTime { get; set; }
    
    [JsonProperty("Start Date")]
    public string StartDate { get; set; }
    
    [JsonProperty("End Date")]
    public string EndDate { get; set; }
}
