using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandbookController : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameObject.FindObjectOfType<HandbookController>().gameObject.GetComponent<Canvas>().enabled || Input.GetKeyDown(KeyCode.JoystickButton2) && GameObject.FindObjectOfType<HandbookController>().gameObject.GetComponent<Canvas>().enabled)
        { 
            if (GameObject.FindObjectOfType<ModifyStrings>().watch.IsRunning)
            {
                GameObject.FindObjectOfType<ModifyStrings>().watch.Stop();
            }
            Debug.Log("pressed e");
            TruthBulletsController tbc = GameObject.FindObjectOfType<TruthBulletsController>();
            GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
            if (tbc.ActiveBullet == GameObject.FindObjectOfType<ModifyStrings>().lines[1])
            {
                base.StartCoroutine(GameObject.FindObjectOfType<ModifyStrings>().LoadFRMDelayed(0.25f, int.Parse(GameObject.FindObjectOfType<ModifyStrings>().lines[2])));
                Debug.Log("yes");
            }
            else
            {
                base.StartCoroutine(GameObject.FindObjectOfType<ModifyStrings>().LoadFRMDelayed(0.25f, int.Parse(GameObject.FindObjectOfType<ModifyStrings>().lines[3])));
            }
            GameObject.FindObjectOfType<HandbookController>().gameObject.GetComponent<Canvas>().enabled = false;
            FCServices.FindChildWithName(GameObject.Find("MainUI"), "BG").SetActive(true);
        }
    }

    public void OpenTruthBullets()
    {
        foreach (Transform child in base.gameObject.transform)
        {
            if (child.name == "TruthBullets" || child.name == "BG")
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        GameObject.FindObjectOfType<TruthBulletsController>().SetActiveBullet("AttorneyBadge");
    }
}
