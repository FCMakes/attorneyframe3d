using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SliderControllerSupport : MonoBehaviour
{
    public GameObject Handle;
    public string AxisName;
    public void Update()
    {
        if (base.gameObject.GetComponentInParent<Canvas>().enabled)
        {

            if (AxisName == "Horizontal")
            {
                if (Gamepad.current != null && Gamepad.current.leftStick.left.isPressed || Input.GetKey(KeyCode.A))
                {
                    base.gameObject.GetComponent<Slider>().value -= 0.025f;
                }
                if (Gamepad.current != null && Gamepad.current.leftStick.right.isPressed || Input.GetKey(KeyCode.D))
                {
                    base.gameObject.GetComponent<Slider>().value += 0.025f;
                }
            }
            if (AxisName == "Horizontal2")
            {
                if (Gamepad.current != null && Gamepad.current.rightStick.left.isPressed || Input.GetKey(KeyCode.LeftArrow))
                {
                    base.gameObject.GetComponent<Slider>().value -= 0.025f;
                }
                if (Gamepad.current != null && Gamepad.current.rightStick.right.isPressed || Input.GetKey(KeyCode.RightArrow))
                {
                    base.gameObject.GetComponent<Slider>().value += 0.025f;
                }
            }


        }
       


    }
}
