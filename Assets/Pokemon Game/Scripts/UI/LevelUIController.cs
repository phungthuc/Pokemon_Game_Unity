using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LevelUIController : ScreenController
{
  [SerializeField] 
  private GameObject buttonPrefab;
  
  [SerializeField] 
  private GameObject parentButtonPrefab;

  public UnityEvent<Data> ButtonOnEvent;

  private List<IconLevelButton> itemList = new List<IconLevelButton>();

  public override void Show()
  {
    base.Show();
    ShowAllLevel();
  }

  public void ShowAllLevel()
  {
    // 00 -> 20 -> check element i trong list empty (if empty -> Instantiate, else set data)
    Debug.Log("Init 21 items");
    var dataList = GameConfig.Instance.DataList;
    var currentLevel = PlayerPrefs.GetInt(LevelManager.LEVEL_USER_PREFS_KEY, 0);
    for (var i = 0; i < dataList.Count; i++)
    {
      if (itemList.Count - 1 < i)
      {
        var item = Instantiate(buttonPrefab, parentButtonPrefab.transform);
        var component = item.GetComponent<IconLevelButton>();
        component.SetData(dataList[i]);
        component.ButtonClicked.RemoveListener(OnEvent);
        component.ButtonClicked.AddListener(OnEvent);
        itemList.Add(component);
        if (i > currentLevel)
        {
          component.Lock();
        }
        else
        {
          component.Unlock();
        }
      }
      else
      {
        var component = itemList[i];
        component.SetData(dataList[i]);
        component.ButtonClicked.RemoveListener(OnEvent);
        component.ButtonClicked.AddListener(OnEvent);
        if (i > currentLevel)
        {
          component.Lock();
        }
        else
        {
          component.Unlock();
        }
      }
      // var item = Instantiate(buttonPrefab, parentButtonPrefab.transform);
      // var component = item.GetComponent<IconLevelButton>();
      // component.SetData(dataList[i]);
      // component.ButtonClicked.RemoveListener(OnEvent);
      // component.ButtonClicked.AddListener(OnEvent);
      // if (i > currentLevel)
      // {
      //   component.Lock();
      // }
      // else
      // {
      //   component.Unlock();
      // }
    }
  }

  public void OnEvent(Data data)
  {
    ButtonOnEvent.Invoke(data);
  }
}
