using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ArrowImitateOnClick : ImitateOnClick
{
    // Start is called before the first frame update
   public override void Update()
    {
        if (base.gameObject.GetComponentInParent<Canvas>().enabled && base.gameObject.GetComponent<Button>().interactable && base.gameObject.activeInHierarchy && !isCovered())
        {
            if (Input.GetKeyDown(forimitation) || Input.GetKeyDown(button2) && button2 != KeyCode.None)
            {
                FCServices.ButtonDownEffect(base.gameObject);
            }
            if (Input.GetKeyUp(forimitation) || Input.GetKeyUp(button2) && button2 != KeyCode.None)
            {
                FCServices.ButtonUpEffect(base.gameObject);
                if (invokeOnClick)
                {
                    base.StartCoroutine(this.ImitateInvokeDo());
                }


            }

            if (Gamepad.current != null)
            {
                if (forimitation == KeyCode.LeftArrow)
                {
                    if (Gamepad.current.dpad.left.wasPressedThisFrame)
                    {
                        FCServices.ButtonDownEffect(base.gameObject);
                    }
                    if (Gamepad.current.dpad.left.wasReleasedThisFrame)
                    {
                        FCServices.ButtonUpEffect(base.gameObject);
                        if (invokeOnClick)
                        {
                            base.StartCoroutine(this.ImitateInvokeDo());
                        }
                    }
                }
                if (forimitation == KeyCode.RightArrow)
                {
                    if (Gamepad.current.dpad.right.wasPressedThisFrame)
                    {
                        FCServices.ButtonDownEffect(base.gameObject);
                    }
                    if (Gamepad.current.dpad.right.wasReleasedThisFrame)
                    {
                        FCServices.ButtonUpEffect(base.gameObject);
                        if (invokeOnClick)
                        {
                            base.StartCoroutine(this.ImitateInvokeDo());
                        }
                    }
                }
                if (forimitation == KeyCode.UpArrow)
                {

                    if (Gamepad.current.dpad.up.wasPressedThisFrame)
                    {
                        FCServices.ButtonDownEffect(base.gameObject);
                    }
                    if (Gamepad.current.dpad.up.wasReleasedThisFrame)
                    {
                        FCServices.ButtonUpEffect(base.gameObject);
                        if (invokeOnClick)
                        {
                            base.StartCoroutine(this.ImitateInvokeDo());
                        }
                    }
                }
                if (forimitation == KeyCode.DownArrow)
                {

                    if (Gamepad.current.dpad.down.wasPressedThisFrame)
                    {
                        FCServices.ButtonDownEffect(base.gameObject);
                    }
                    if (Gamepad.current.dpad.down.wasReleasedThisFrame)
                    {
                        FCServices.ButtonUpEffect(base.gameObject);
                        if (invokeOnClick)
                        {
                            base.StartCoroutine(this.ImitateInvokeDo());
                        }
                    }
                }
            }

        }



    }

}
