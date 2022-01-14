using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

namespace FCMakes.LazyPort
{
    public class ButtonInputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        // Start is called before the first frame update
        public bool GetKey;
        public bool GetKeyUp;
        public bool GetKeyDown;
        public bool invokeOnControllerKeyUp;
      

        public void OnPointerDown(PointerEventData eventData)
        {
            GetKey = true;
            GetKeyDown = true;
            Action GetKeyDownFalse = () =>
            {
                GetKeyDown = false;
            };
            base.StartCoroutine(FCServices.DoNextFrame(GetKeyDownFalse));

        }

       
        public void OnPointerUp(PointerEventData eventData)
        {
            GetKey = false;
            GetKeyUp = true;
            Action GetKeyUpFalse = () =>
            {
                GetKeyUp = false;
            };
            base.StartCoroutine(FCServices.DoNextFrame(GetKeyUpFalse));
        }

        public void FixedUpdate()
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(base.gameObject.GetComponent<RectTransform>(), Input.mousePosition) && base.gameObject.GetComponentInParent<Canvas>().enabled){

               
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    GetKey = true;
                    GetKeyDown = true;
                    Action GetKeyDownFalse = () =>
                    {
                        GetKeyDown = false;
                    };
                    FCServices.ButtonDownEffect(base.gameObject);
                    base.StartCoroutine(FCServices.DoNextFrame(GetKeyDownFalse));



                }
                if (Input.GetKeyUp(KeyCode.JoystickButton0))
                {
                    GetKey = false;
                    GetKeyUp = true;
                    Action GetKeyUpFalse = () =>
                    {
                        GetKeyUp = false;
                    };
                    FCServices.ButtonUpEffect(base.gameObject);
                    base.StartCoroutine(FCServices.DoNextFrame(GetKeyUpFalse));

                    if (invokeOnControllerKeyUp)
                    {
                        base.gameObject.GetComponent<Button>().onClick.Invoke();
                    }
                }
            }
        }
    }
}