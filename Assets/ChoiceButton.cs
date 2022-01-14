using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceButton : FCMakes.LazyPort.ButtonInputManager, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        
    }
    public void OnPointerExit(PointerEventData pointerEventData)
    {
       
    }



    public void Update()
    {
        if (GetKeyDown && Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            FCServices.ButtonDownEffect(base.gameObject);
        }
        if (GetKeyUp && Input.GetKeyUp(KeyCode.JoystickButton0))
        {
            FCServices.ButtonUpEffect(base.gameObject);
            System.Action invokeonclick = () =>
            {
                base.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
            };
            base.StartCoroutine(FCServices.DoNextFrame(invokeonclick));
        }

    }
}
