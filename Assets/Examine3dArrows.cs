using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Examine3dArrows : FCMakes.LazyPort.ButtonInputManager {

    public Vector3 originalpos;
    public Vector3 pressedpos;

    public void Start()
    {
        originalpos = base.gameObject.GetComponent<RectTransform>().anchoredPosition;
        pressedpos = originalpos + base.gameObject.GetComponent<RectTransform>().right * 16.9f;
    }

    public void Update()
    {
        if (this.GetKey || this.AltGetKey())
        {
            base.gameObject.GetComponent<RectTransform>().anchoredPosition = pressedpos;
        }
        if (this.GetKeyUp || this.AltGetKeyUp())
        {
            base.gameObject.GetComponent<RectTransform>().anchoredPosition = originalpos;
        }
    }
    public bool AltGetKey()
    {
        if (base.gameObject.transform.GetSiblingIndex() == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Gamepad.current != null && Gamepad.current.dpad.left.isPressed)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 1)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Gamepad.current != null && Gamepad.current.dpad.right.isPressed)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 2)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Gamepad.current != null && Gamepad.current.dpad.down.isPressed)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 3)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Gamepad.current != null && Gamepad.current.dpad.up.isPressed)
            {
                return true;
            }

        }
        return false;
    }
    public bool AltGetKeyDown()
    {
        if (base.gameObject.transform.GetSiblingIndex() == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Gamepad.current != null && Gamepad.current.dpad.left.wasPressedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 1)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Gamepad.current != null && Gamepad.current.dpad.right.wasPressedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 2)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Gamepad.current != null && Gamepad.current.dpad.down.wasPressedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 3)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                return true;
            }

        }
        return false;
    }
    public bool AltGetKeyUp()
    {
        if (base.gameObject.transform.GetSiblingIndex() == 0)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Gamepad.current != null && Gamepad.current.dpad.left.wasReleasedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 1)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) || Gamepad.current != null && Gamepad.current.dpad.right.wasReleasedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 2)
        {
            if (Input.GetKeyUp(KeyCode.DownArrow) || Gamepad.current != null && Gamepad.current.dpad.down.wasReleasedThisFrame)
            {
                return true;
            }

        }
        if (base.gameObject.transform.GetSiblingIndex() == 3)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow) || Gamepad.current != null && Gamepad.current.dpad.up.wasReleasedThisFrame)
            {
                return true;
            }

        }
        return false;
    }
}
