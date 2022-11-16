using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Tooltip :MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Detect if the Cursor starts to pass over the GameObject
    public string message;
    public Transform slotPos;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        //Output to console the GameObject's name and the following message
        // Debug.Log("Cursor Entering " + name + " GameObject");
        TooltipManager._instance.SetAndShowTooltip(message);
        // Debug.Log("Mouse enter");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        //Output the following message with the GameObject's name
        // Debug.Log("Cursor Exiting " + name + " GameObject");
        TooltipManager._instance.HideTooltip();
    }
    
}
