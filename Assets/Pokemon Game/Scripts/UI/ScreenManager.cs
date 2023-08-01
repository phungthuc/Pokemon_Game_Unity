using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] 
    private List<ScreenController> screenControllerList = new List<ScreenController>();
    public void ShowScreen(string screenName)
    {
        HideAllScreen();
        var screen = FindScreenByName(screenName);
        if (screen == null)
        {
            Debug.LogError("Can't find screen has name " + screenName);
            return;
        }
        
        screen.Show();
    }

    public void HideAllScreen()
    {
        foreach (var screenController in screenControllerList)
        {
            screenController.Hide();
        }
    }

    private ScreenController FindScreenByName(string screenName)
    {
        return screenControllerList.Find(item => item.ScreenName.Equals(screenName));
    }
}
