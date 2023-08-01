using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data 
{
    [JsonProperty("level")] 
    public int Level { get; set; }
    
    [JsonProperty("image")] 
    public string ImageName { get; set; }
    
    [JsonProperty("option1")] 
    public string Option1 { get; set; }
    
    [JsonProperty("option2")] 
    public string Option2 { get; set; }
    
    [JsonProperty("optionCorrect")] 
    public string CorrectOption { get; set; }
}
