using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class StringModel
{
    [JsonProperty("Id")]
    public string Id { get; set; }

    [JsonProperty("Name")] 
    public string Name { get; set; }

    [JsonProperty("Data")]
    public string Data { get; set; }

    public StringModel(string Id, string Name, string Data)
    {
        this.Id = Id;
        this.Name = Name;
        this.Data = Data;
    }

}
