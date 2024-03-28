using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class FishModel
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

    [JsonProperty("Shadow Size")]
    public string ShadowSize { get; set; }

    [JsonProperty("Catch Difficulty")]
    public string CatchDifficulty { get; set; }

    [JsonProperty("Total Catches")]
    public int TotalCatches { get; set; }

    [JsonProperty("NH Jan")]
    public string NHJan { get; set; }

    [JsonProperty("NH Feb")]
    public string NHFeb { get; set; }

    [JsonProperty("NH Mar")]
    public string NHMar { get; set; }

    [JsonProperty("NH Apr")]
    public string NHApr { get; set; }

    [JsonProperty("NH May")]
    public string NHMay { get; set; }

    [JsonProperty("NH Jun")]
    public string NHJun { get; set; }

    [JsonProperty("NH Jul")]
    public string NHJul { get; set; }

    [JsonProperty("NH Aug")]
    public string NHAug { get; set; }

    [JsonProperty("NH Sep")]
    public string NHSep { get; set; }

    [JsonProperty("NH Oct")]
    public string NHOct { get; set; }

    [JsonProperty("NH Nov")]
    public string NHNov { get; set; }

    [JsonProperty("NH Dec")]
    public string NHDec { get; set; }

    [JsonProperty("SH Jan")]
    public string SHJan { get; set; }

    [JsonProperty("SH Feb")]
    public string SHFeb { get; set; }

    [JsonProperty("SH Mar")]
    public string SHMar { get; set; }

    [JsonProperty("SH Apr")]
    public string SHApr { get; set; }

    [JsonProperty("SH May")]
    public string SHMay { get; set; }

    [JsonProperty("SH Jun")]
    public string SHJun { get; set; }

    [JsonProperty("SH Jul")]
    public string SHJul { get; set; }

    [JsonProperty("SH Aug")]
    public string SHAug { get; set; }

    [JsonProperty("SH Sep")]
    public string SHSep { get; set; }

    [JsonProperty("SH Oct")]
    public string SHOct { get; set; }

    [JsonProperty("SH Nov")]
    public string SHNov { get; set; }

    [JsonProperty("SH Dec")]
    public string SHDec { get; set; }

    [JsonProperty("Description")]
    public string Description { get; set; }
}
