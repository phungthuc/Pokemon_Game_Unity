using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI textLevel;
    public UnityEvent<Data> eventButton;
    public Image doneLevelImage;
    public Button buttonController;
    public Data dataHandle;
    void Start()
    {
        
    }

    public void setData(Data data)
    {
        textLevel.text = "Level " + data.level.ToString();
        dataHandle = data;
    }

    public void onClick()
    {
        eventButton.Invoke(dataHandle);
    }
}
