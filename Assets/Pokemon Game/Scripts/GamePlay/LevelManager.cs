using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI textLevel;
    
    [SerializeField] 
    private Image imagePlayUI;
    
    [SerializeField] 
    private TextMeshProUGUI textOption1;
    
    [SerializeField] 
    private TextMeshProUGUI textOption2;

    [SerializeField] 
    private TextMeshProUGUI textCorrectOption;

    [SerializeField] 
    private Image imageCorrectOption;

    [SerializeField] 
    private Image imageUncorrectOption;

    public const string LEVEL_USER_PREFS_KEY = "level_user";
    private Data dataCurrentLevel;
    public UnityEvent eventOptionCorrect;
    public UnityEvent eventOptionUncorrect;
    
    //Comment Awake methods if want to save data level
    private void Awake()
    {
        PlayerPrefs.DeleteKey(LevelManager.LEVEL_USER_PREFS_KEY);
    }
    
    public void LoadData(Data data)
    {
        Addressables.LoadAssetAsync<Sprite>(data.ImageName).Completed += handle =>
        {
            imagePlayUI.sprite = handle.Result;
        };
        textOption1.text = data.Option1;
        textOption2.text = data.Option2;
        textLevel.text = "Level " + data.Level;
        dataCurrentLevel = null;
        dataCurrentLevel = data;
    }

    public void LoadData()
    {
        var currentLevel = PlayerPrefs.GetInt(LEVEL_USER_PREFS_KEY, 0);
        var levelDataList = GameConfig.Instance.DataList;
        if (currentLevel > levelDataList.Count)
        {
            currentLevel = levelDataList.Count - 1;
        }
        var levelData = levelDataList[currentLevel];
        Addressables.LoadAssetAsync<Sprite>(levelData.ImageName).Completed += handle =>
        {
            imagePlayUI.sprite = handle.Result;
        };
        textOption1.text = levelData.Option1;
        textOption2.text = levelData.Option2;
        textLevel.text = "Level " + levelData.Level;
        dataCurrentLevel = null;
        dataCurrentLevel = levelData;
    }
    
    public void LoadDataCurrentLevel()
    {
        Addressables.LoadAssetAsync<Sprite>(dataCurrentLevel.ImageName).Completed += handle =>
        {
            imagePlayUI.sprite = handle.Result;
        };
        textOption1.text = dataCurrentLevel.Option1;
        textOption2.text = dataCurrentLevel.Option2;
        textLevel.text = "Level " + dataCurrentLevel.Level;
    }
    
    public void LoadDataNextLevel()
    {
        int nextLevel = dataCurrentLevel.Level;
        nextLevel += 1;
        dataCurrentLevel = GameConfig.Instance.DataList[nextLevel - 1];
        Addressables.LoadAssetAsync<Sprite>(dataCurrentLevel.ImageName).Completed += handle =>
        {
            imagePlayUI.sprite = handle.Result;
        };
        textOption1.text = dataCurrentLevel.Option1;
        textOption2.text = dataCurrentLevel.Option2;
        textLevel.text = "Level " + dataCurrentLevel.Level;
    }

    public void CheckDataOption(TextMeshProUGUI textOption)
    {
        if (dataCurrentLevel != null)
        {
            if (textOption.text.Equals(dataCurrentLevel.CorrectOption))
            {
                eventOptionCorrect.Invoke();
                PlayerPrefs.SetInt(LEVEL_USER_PREFS_KEY, dataCurrentLevel.Level);
            }
            
            else
            {
                eventOptionUncorrect.Invoke();
            }
        }
    }

    public void LoadDataEndWinUI()
    {
        textCorrectOption.text = dataCurrentLevel.CorrectOption;
        Addressables.LoadAssetAsync<Sprite>(dataCurrentLevel.ImageName).Completed +=
            handle =>
            {
                imageCorrectOption.sprite = handle.Result;
            };
    }
    
    public void LoadDataEndLossUI()
    {
        Addressables.LoadAssetAsync<Sprite>(dataCurrentLevel.ImageName).Completed += handle =>
        {
            imageUncorrectOption.sprite = handle.Result;
        };
    }
    
}
