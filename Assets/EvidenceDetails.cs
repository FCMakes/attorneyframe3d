using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceDetails : FCMakes.LazyPort.ButtonInputManager {

    public void Update()
    {
        if (!GameObject.FindObjectOfType<ModifyStrings>().tw.isTyping && !FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "Details").activeSelf && !GameObject.FindObjectOfType<ModifyStrings>().lines[0].Contains("EvidenceEnter"))
        {
            if (GetKeyUp)
            {
                GameObject.FindObjectOfType<Record>().OpenDetails(base.gameObject.transform.parent.gameObject.name);
            }
            if (Input.GetKeyDown(KeyCode.Return) && base.gameObject.activeInHierarchy || Input.GetKeyDown(KeyCode.JoystickButton0) && base.gameObject.activeInHierarchy)
            {
                FCServices.ButtonDownEffect(base.gameObject);
            }
            if (Input.GetKeyUp(KeyCode.Return) && base.gameObject.activeInHierarchy || Input.GetKeyUp(KeyCode.JoystickButton0) && base.gameObject.activeInHierarchy)
            {
                FCServices.ButtonUpEffect(base.gameObject);
                GameObject.FindObjectOfType<Record>().OpenDetails(base.gameObject.transform.parent.gameObject.name);
            }
        }
       

    }


}
