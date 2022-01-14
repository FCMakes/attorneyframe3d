using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject Player;

    public void Update()
    {
        if (Player.GetComponent<MoveController>().enabled)
        {
            if (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                base.gameObject.GetComponent<Canvas>().enabled = !base.gameObject.GetComponent<Canvas>().enabled;
            }

            FCServices.FindChildWithName(base.gameObject, "Player").transform.position = FCServices.FindChildWithName(base.gameObject, "Camera").GetComponent<Camera>().WorldToScreenPoint(Player.transform.position);
            FCServices.FindChildWithName(base.gameObject, "Player").transform.eulerAngles = new Vector3(0f, 0f, (-Player.transform.eulerAngles.y));
        }
    }

}
