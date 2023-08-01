using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GameConfig
{
    private static GameConfig _instance;
    public static GameConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameConfig();
            }

            return _instance;
        }
        set => _instance = value;
    }
    public List<Data> DataList = new List<Data>();

}
