using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public TextAsset textJson;
    public GameObject prefab;
    public GameObject parent;
    public GameObject playUI;
    public GameObject levelUI;
    public GameObject endUI;
    public Image imagePlayUI;
    public Image imageEndUI;
    public TextMeshProUGUI option1;
    public TextMeshProUGUI option2;
    public TextMeshProUGUI textEndUI;
    public TextMeshProUGUI correctEndUI;
    public Data dataLevel;
    public int count = 0;
    private List<Data> listData = new List<Data>();
    public LevelController[] components;
    void Start()
    {
        PlayerPrefs.SetInt("1", 1);
        listData = JsonConvert.DeserializeObject<List<Data>>(textJson.text);
        components = new LevelController[listData.Count];
        foreach (var data in listData)
        {
            var item = Instantiate(prefab, parent.transform);
            var component = item.GetComponent<LevelController>();
            components[count] = component;
            count += 1;
            component.setData(data);
            component.eventButton.AddListener(onEvent);
        }
    }

     void Update()
    {
        enableLevel();
    }

    public void onEvent(Data data)
    {
        playUI.SetActive(true);
        levelUI.SetActive(false);
        Addressables.LoadAssetAsync<Sprite>(data.imageName).Completed += handle =>
        {
            imagePlayUI.sprite = handle.Result;
        };
        option1.text = data.option1.ToString();
        option2.text = data.option2.ToString();
        dataLevel = data;
    }

    public void checkOption(TextMeshProUGUI option)
    {
        if (option.text == dataLevel.optionCorrect)
        {
            int levelCurrent = dataLevel.level + 1;
            playUI.SetActive(false);
            endUI.SetActive(true);
             Addressables.LoadAssetAsync<Sprite>(dataLevel.imageName).Completed += handle =>
            {
                imageEndUI.sprite = handle.Result;
            };
            textEndUI.text = "You Win";
            correctEndUI.text = dataLevel.optionCorrect;
            PlayerPrefs.SetInt(levelCurrent.ToString(), 1);
            PlayerPrefs.Save();
            // components[dataLevel.level - 1].doneLevelImage.gameObject.SetActive(true);
        }
        else
        {
            playUI.SetActive(false);
            endUI.SetActive(true);
            Addressables.LoadAssetAsync<Sprite>(dataLevel.imageName).Completed += handle =>
            {
                imageEndUI.sprite = handle.Result;
            };
            textEndUI.text = "You Loss";
            correctEndUI.text = dataLevel.optionCorrect;
        }
    }

    public void enableLevel()
    {
        for (int i = 1; i <= components.Length; i++)
        {
            if (PlayerPrefs.GetInt(i.ToString()) == 1)
            {
                components[i - 1].doneLevelImage.gameObject.SetActive(false);
                components[i - 1].buttonController.interactable = true;
            }
        }
    }
}

public class Data
{
    [JsonProperty("level")]
    public int level { get; set; }
    
    [JsonProperty("image")]
    public string imageName { get; set; }
    
    [JsonProperty("option1")]
    public string option1 { get; set; }
    
    [JsonProperty("option2")]
    public string option2 { get; set; }
    
    [JsonProperty("optionCorrect")]
    public string optionCorrect { get; set; }
}
