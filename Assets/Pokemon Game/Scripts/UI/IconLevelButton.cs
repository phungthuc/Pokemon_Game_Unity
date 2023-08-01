using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class IconLevelButton : MonoBehaviour
{
   public UnityEvent<Data> ButtonClicked;
   
   [SerializeField] 
   private TextMeshProUGUI textLevel;
   
   [SerializeField] 
   private Image imageLevel;
   
   [SerializeField] 
   private Button buttonLevel;

   public Data dataHandle;
   public void SetData(Data data)
   {
      dataHandle = data;
      textLevel.text = "Level " + data.Level;
   }
   public void OnClick()
   {
      ButtonClicked.Invoke(dataHandle);
   }

   public void Unlock()
   {
      imageLevel.gameObject.SetActive(false);
      buttonLevel.interactable = true;
   }

   public void Lock()
   {
      imageLevel.gameObject.SetActive(true);
      buttonLevel.interactable = false;
   }
}
