using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;


public class SaveLoadButton : FCMakes.LazyPort.ButtonInputManager
{
    public void Start()
    {
        UpdateInfo();
       
    }

    public void Update()
    {
        if (!GameObject.FindObjectOfType<OptionsController>().gameObject.GetComponent<Canvas>().enabled && !GameObject.FindObjectOfType<InstructionsController>().gameObject.GetComponent<Canvas>().enabled && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "Confirm").activeSelf && FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle") != null && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle").activeSelf || !GameObject.FindObjectOfType<OptionsController>().gameObject.GetComponent<Canvas>().enabled && !GameObject.FindObjectOfType<InstructionsController>().gameObject.GetComponent<Canvas>().enabled && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "Confirm").activeSelf && FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle") == null)
        {

            if (GetKeyDown && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "Confirm").activeSelf && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle") || GetKeyDown && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "Confirm").activeSelf && FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle") && !FCServices.FindChildWithName(GameObject.FindObjectOfType<SaveLoadController>().gameObject, "ToTitle"))
            {
                if (GameObject.FindObjectOfType<SaveLoadController>().gameObject.name == "Load" && base.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text == "No data.")
                {
                    GameObject.FindObjectOfType<SaveLoadController>().gameObject.GetComponents<AudioSource>()[4].Play();
                }
                else
                {
                    GameObject.FindObjectOfType<SaveLoadController>().ShowConfirmation(base.gameObject.GetComponent<Button>());
                }

            }

            if (GetKeyUp && GameObject.FindObjectOfType<SaveLoadController>().gameObject.name == "Save")
            {
                GameObject.FindObjectOfType<SaveLoadController>().ShowConfirmation(base.gameObject.GetComponent<Button>());
            }
        }
    }

    public void UpdateInfo()
    {
        int slotnumber = int.Parse(FCServices.FindChildWithName(base.gameObject, "SlotNumber").GetComponent<Text>().text);
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber))){

            string[] MainFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs"));

            base.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "Fanmade Episode";
            base.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = MainFCS[0].Split(char.Parse(@"/"))[0];
            if (MainFCS[0].Split(char.Parse(@"/")).Length > 2)
            {
                base.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = MainFCS[0].Split(char.Parse(@"/"))[2];
            }
            if (PlayerPrefs.HasKey("SaveTime" + slotnumber))
            {
                base.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = PlayerPrefs.GetString("SaveTime" + slotnumber);

            }
            else
            {
                base.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = "";
            }

        }
        else
        {
            base.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
            base.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "No data.";
            base.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "";
            base.gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = "";
        }

    }
}
