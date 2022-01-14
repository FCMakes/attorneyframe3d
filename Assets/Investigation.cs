using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Investigation : MonoBehaviour
{
    public FCServices.Interval ClampX;
    public FCServices.Interval ClampY;
    public int InvestigateAngleId;
    public Transform RotParent;

    public void Start()
    {
        if (RotParent == null)
        {
            RotParent = base.gameObject.transform;
        }
    }
    public void Update()
    {
        
       
       

        if (GameObject.FindObjectOfType<ModifyStrings>().canInvestigate || GameObject.Find("MainUI") != null && FCServices.FindChildWithName("MainUI", "JointReasoning").activeSelf)
        {
            if (RotParent.eulerAngles.z != 0f)
            {
                RotParent.eulerAngles = new Vector3(RotParent.eulerAngles.x, RotParent.eulerAngles.y, 0f);
            }
            float fakehorizontal = 0;
            float fakevertical = 0;
            if (Input.GetKey(KeyCode.A))
            {
                fakehorizontal = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                fakehorizontal = 1;
            }
            if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().x != 0)
            {
                fakehorizontal = Gamepad.current.rightStick.ReadValue().x;
            }
            if (Input.GetKey(KeyCode.S))
            {
                fakevertical = -1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                fakevertical = 1;
            }
            if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().y != 0)
            {
                fakevertical = Gamepad.current.rightStick.ReadValue().y;
            }
            if (fakehorizontal < 0)
            {
                Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                if (diff.y > ClampY.min)
                {
                    RotParent.Rotate(0f, Mathf.Clamp(fakehorizontal, -0.25f, 0.25f) * Time.deltaTime * 80f, 0f);
                }
            }
            if (fakehorizontal > 0)
            {
                Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                if (diff.y < ClampY.max)
                {
                    RotParent.Rotate(0f, Mathf.Clamp(fakehorizontal, -0.25f, 0.25f) * Time.deltaTime * 80f, 0f);
                }
            }
            if (-fakevertical > 0)
            {

                Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                if (diff.x < ClampX.max)
                {
                    RotParent.Rotate(Mathf.Clamp(-fakevertical, -0.25f, 0.25f) * Time.deltaTime * 80f, 0f, 0f);
                }
            }
            if (-fakevertical < 0)
            {
                Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                if (diff.x > ClampX.min)
                {
                    RotParent.Rotate(Mathf.Clamp(-fakevertical, -0.25f, 0.25f) * Time.deltaTime * 80f, 0f, 0f);
                }
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                if (Input.GetAxis("Mouse X") < 0)
                {
                    Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                    if (diff.y > ClampY.min)
                    {
                        RotParent.Rotate(0f, Mathf.Clamp(Input.GetAxis("Mouse X"), -0.25f, 0.25f) * Time.deltaTime * 80f, 0f);
                    }
                }
                if (Input.GetAxis("Mouse X") > 0)
                {
                    Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                    if (diff.y < ClampY.max)
                    {
                        RotParent.Rotate(0f, Mathf.Clamp(Input.GetAxis("Mouse X"), -0.25f, 0.25f) * Time.deltaTime * 80f, 0f);
                    }
                }
                if (-Input.GetAxis("Mouse Y") > 0)
                {
                    
                    Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                    if (diff.x < ClampX.max)
                    {
                        RotParent.Rotate(Mathf.Clamp(-Input.GetAxis("Mouse Y"), -0.25f, 0.25f) * Time.deltaTime * 80f, 0f, 0f);
                    }
                }
                if (-Input.GetAxis("Mouse Y") < 0)
                {
                    Vector3 diff = FCServices.EulerToNiceRotation(RotParent.eulerAngles - base.gameObject.GetComponent<MainCameraController>().AvailableAngles[InvestigateAngleId].rotation.eulerAngles);
                    if (diff.x > ClampX.min)
                    {
                        RotParent.Rotate(Mathf.Clamp(-Input.GetAxis("Mouse Y"), -0.25f, 0.25f) * Time.deltaTime * 80f, 0f, 0f);
                    }
                }


            }
            else
            {
                if (Cursor.lockState != CursorLockMode.None)
                {
                    Cursor.lockState = CursorLockMode.None;
                }
            }


        }
    }
}
