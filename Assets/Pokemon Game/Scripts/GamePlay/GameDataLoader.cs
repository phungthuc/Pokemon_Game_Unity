using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameDataLoader : MonoBehaviour
{
    [SerializeField] 
    private TextAsset textJson;

    public UnityEvent Loaded;

     private void Start()
    {
        Load();
        Loaded.Invoke();
    }

    public void Load()
    {
        GameConfig.Instance.DataList = JsonConvert.DeserializeObject<List<Data>>(textJson.text);
    }
    
}
