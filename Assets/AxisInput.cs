using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxisInput : MonoBehaviour
{
    public bool holdingDownStick1H;
    public bool stick1HDown;
    public bool stick1HUp;

    [System.Serializable]
    public class AxisInfo
    {
        public string name;
        public bool Get;
        public bool GetDown;
        public bool GetUp;


    }

    public List<AxisInfo> axes;

    public AxisInfo GetAxisByName(string name)
    {
        foreach (AxisInfo ai in axes)
        {
            if (ai.name == name)
            {
                return ai;
            }
        }
        return null;

    }

    public float GetHorizontal3()
    {
        if (Gamepad.current != null)
        {
            return Gamepad.current.dpad.ReadValue().x;
        }
        return 0;
    }
    public float GetVertical3()
    {
        if (Gamepad.current != null)
        {
            return Gamepad.current.dpad.ReadValue().y;
        }
        return 0;
    }
    public void Update()
    {
        


        foreach (AxisInfo ai in axes)
        {
            if (Input.GetAxis(ai.name) != 0)
            {
                if (ai.Get && ai.GetDown)
                {
                    ai.GetDown = false;
                }

                if (!ai.Get)
                {
                    ai.Get = true;
                    ai.GetDown = true;
                }

            }

            if (Input.GetAxis(ai.name) == 0)
            {
                if (!ai.Get && ai.GetUp)
                {
                    ai.GetUp = false;
                }

                if (ai.Get)
                {
                    ai.Get = false;
                    ai.GetUp = true;
                }

            }



        }



        if (Input.GetAxis("Horizontal") != 0)
        {
            if (holdingDownStick1H && stick1HDown)
            {
                stick1HDown = false;
            }

            if (!holdingDownStick1H)
            {
                holdingDownStick1H = true;
                stick1HDown = true;
               


            }
          


        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            if (!holdingDownStick1H && stick1HUp)
            {
                stick1HUp = false;
            }

            if (holdingDownStick1H)
            {
                holdingDownStick1H = false;
                stick1HUp = true;



            }



        }

    }


}
