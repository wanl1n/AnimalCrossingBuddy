using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class StringModel
{
    [JsonProperty("Id")]
    public string Id { get; set; }

    [JsonProperty("Data")]
    public string Data { get; set; }

}
