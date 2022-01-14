using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveController : MonoBehaviour
{
    public float rotspeed;
    public float movementSpeed;

    public void OnDisable()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if (base.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            base.gameObject.GetComponent<AudioSource>().Stop();
        }
    }

    public Vector3 deduceRightNiceRotation(Vector3 original)
    {

        if (original.z == 180f)
        {
            if (original.x > 0)
            {
                float actualx = 180f - original.x;
                return new Vector3(actualx, 0f, 0f);
            }
            if (original.x < 0)
            {
                float actualx = -180f + (-original.x);
                return new Vector3(actualx, 0f, 0f);
            }
        }
        return original;

    }


    public void Update()
    {
       

        if (!GameObject.FindObjectOfType<ModifyStrings>().isAnyMenusOpen())
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if (Input.GetAxis("Mouse X") != 0f)
            {
                base.gameObject.transform.Rotate(0, rotspeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0);
            }



            if (Gamepad.current != null && Gamepad.current.rightStick.x.ReadValue() != 0f)
            {
                base.gameObject.transform.Rotate(0, rotspeed * Gamepad.current.rightStick.x.ReadValue() * Time.deltaTime, 0);
            }

            if (Input.GetAxis("Mouse Y") != 0f)
            {
                if (-Input.GetAxis("Mouse Y") < 0 && this.deduceRightNiceRotation(FCServices.GetNiceRotation(base.gameObject.transform.GetChild(1).gameObject)).x > -90f || -Input.GetAxis("Mouse Y") > 0 && this.deduceRightNiceRotation(FCServices.GetNiceRotation(base.gameObject.transform.GetChild(1).gameObject)).x < 90f)
                {

                    base.gameObject.transform.GetChild(1).Rotate(rotspeed * -Input.GetAxis("Mouse Y") * Time.deltaTime, 0, 0);
                }

            }
            if (Gamepad.current != null && Gamepad.current.rightStick.y.ReadValue() != 0f)
            {
                if (-Gamepad.current.rightStick.y.ReadValue() < 0 && this.deduceRightNiceRotation(FCServices.GetNiceRotation(base.gameObject.transform.GetChild(1).gameObject)).x > -90f || -Gamepad.current.rightStick.y.ReadValue() > 0 && this.deduceRightNiceRotation(FCServices.GetNiceRotation(base.gameObject.transform.GetChild(1).gameObject)).x < 90f)
                {

                    base.gameObject.transform.GetChild(1).Rotate(rotspeed * -Gamepad.current.rightStick.y.ReadValue() * Time.deltaTime, 0, 0);
                }

            }


            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.JoystickButton4))
            {
                movementSpeed = 25f;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.JoystickButton4))
            {
                movementSpeed = 12.5f;
            }


            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && base.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                base.gameObject.GetComponent<AudioSource>().Stop();
            }

            if (Input.GetAxis("Vertical") != 0)
            {
                if (!base.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    base.gameObject.GetComponent<AudioSource>().Play();
                }
                Debug.DrawRay(base.gameObject.transform.position, Vector3.Normalize(base.gameObject.transform.forward * Input.GetAxis("Vertical")));
                if (!Physics.Raycast(base.gameObject.transform.position, Vector3.Normalize(base.gameObject.transform.forward * Input.GetAxis("Vertical")), 2f))
                {
                    transform.position += transform.TransformDirection(Vector3.forward) * Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
                }

            }
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (!base.gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    base.gameObject.GetComponent<AudioSource>().Play();
                }
                Debug.DrawRay(base.gameObject.transform.position, Vector3.Normalize(base.gameObject.transform.right * Input.GetAxis("Horizontal")));
                if (!Physics.Raycast(base.gameObject.transform.position, Vector3.Normalize(base.gameObject.transform.right * Input.GetAxis("Horizontal")), 2f))
                {
                    transform.position += transform.TransformDirection(Vector3.right) * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
                }
            }

            GameObject.FindObjectOfType<ModifyStrings>().Save(0);

        }
        else
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            if (base.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                base.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
        
    }
}
