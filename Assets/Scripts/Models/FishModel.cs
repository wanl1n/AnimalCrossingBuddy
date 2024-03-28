using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FishModel
{
    public string Id { get; set; }
    public string IconImageLink { get; set; }
    public string CritterpediaImageLink { get; set; }
    public int SellPrice { get; set; }
    public string Location { get; set; }
    public string ShadowSize { get; set; }
    public string CatchDifficulty { get; set; }
    public int TotalCatches { get; set; }
    public string Description { get; set; }
}
