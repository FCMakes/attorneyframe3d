using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class SaveLoadController : MonoBehaviour
{
    public void Toggle()
    {
    

            if (base.gameObject.GetComponent<Canvas>().enabled)
            {

                base.gameObject.GetComponents<AudioSource>()[1].Play();
                base.gameObject.GetComponent<Canvas>().enabled = false;
                base.gameObject.GetComponent<CanvasScaler>().enabled = false;
            }
            else
            {
                base.gameObject.GetComponents<AudioSource>()[0].Play();
                base.gameObject.GetComponent<Canvas>().enabled = true;
                base.gameObject.GetComponent<CanvasScaler>().enabled = true;
                FCServices.FindChildWithName(base.gameObject, "Scroll View").GetComponent<ScrollRect>().content.transform.localPosition = Vector3.zero;
            }
        
    }

    public void ShowConfirmation(Button slot)
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        foreach (Transform child in FCServices.FindChildWithArray(base.gameObject, "Confirm", "Slot").transform)
        {
            child.gameObject.GetComponent<Text>().text = FCServices.FindChildWithName(slot.gameObject, child.gameObject.name).GetComponent<Text>().text;
            child.gameObject.GetComponent<Text>().fontSize = FCServices.FindChildWithName(slot.gameObject, child.gameObject.name).GetComponent<Text>().fontSize;
        }
        FCServices.FindChildWithName(base.gameObject, "Confirm").SetActive(true);


    }

    public void ShowReturnToTile()
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        FCServices.FindChildWithName(base.gameObject, "ToTitle").SetActive(true);
    }


    public void CloseConfirmation()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        FCServices.FindChildWithName(base.gameObject, "Confirm").SetActive(false);
    }

    public void CloseReturnToTitle()
    {
        FCServices.FindChildWithName(base.gameObject, "ToTitle").SetActive(false);
    }

    public void CloseRTTSound()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        FCServices.FindChildWithName(base.gameObject, "ToTitle").SetActive(false);
    }

    public void CloseConfirmationNoSound()
    {
        FCServices.FindChildWithName(base.gameObject, "Confirm").SetActive(false);
    }
    public void LoadFromSlot(int slotnumber)
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "temp.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "temp.fcs"));
        }
        StreamWriter sw = new StreamWriter(Path.Combine(Application.persistentDataPath, "temp.fcs"));
        sw.WriteLine(slotnumber);
        sw.Close();
       
        string[] MainFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs"));
        string[] splitline0 = MainFCS[0].Split(char.Parse(@"/"));
        
        UnityEngine.SceneManagement.SceneManager.LoadScene(AssetBundleController.GetBundleFromName(splitline0[0].ToLower()).GetAllScenePaths()[int.Parse(splitline0[1])]);
    }

    public void SaveConfirmation()
    {
        base.gameObject.GetComponents<AudioSource>()[3].Play();
        string slotholder = FCServices.FindChildWithArray(base.gameObject, "Confirm", "Slot", "SlotNumber").GetComponent<Text>().text;
        GameObject.FindObjectOfType<ModifyStrings>().Save(int.Parse(slotholder));
        Invoke("CloseConfirmationNoSound", 1f);

        foreach (SaveLoadButton slb in GameObject.FindObjectsOfType<SaveLoadButton>())
        {
            slb.UpdateInfo();
        }



    }

    public void BeforeGoingToTitle()
    {
        CloseReturnToTitle();
        Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("FadeEffect4"));

      
        GameObject.FindObjectOfType<MusicController>().gameObject.GetComponent<AudioSource>().DOFade(0f, 1f);
        PlayerPrefs.DeleteKey("CurrentSlot");
        PlayerPrefs.Save();
        Invoke("LoadTitleScreen", 1.5f);
    }

    public void LoadTitleScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void BeforeLoad()
    {
        CloseConfirmationNoSound();
        Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("FadeEffect4"));
        



    }

    public IEnumerator DoLoad(int slotnumber, float time)
    {
        yield return new WaitForSeconds(time);
        LoadFromSlot(slotnumber);
        yield break;
    }

    public void ToTitleConfirmation()
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        Invoke("BeforeGoingToTitle", 1f);
    }

    public void LoadConfirmation()
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        string slotholder = FCServices.FindChildWithArray(base.gameObject, "Confirm", "Slot", "SlotNumber").GetComponent<Text>().text;
        Invoke("BeforeLoad", 1f);
        base.StartCoroutine(this.DoLoad(int.Parse(slotholder), 2.5f));

    }


    public void Update()
    {
        if (FCServices.FindChildWithName(base.gameObject, "Confirm") && !FCServices.FindChildWithName(base.gameObject, "Confirm").activeSelf && FCServices.FindChildWithName(base.gameObject, "ToTitle") && !FCServices.FindChildWithName(base.gameObject, "ToTitle").activeSelf || FCServices.FindChildWithName(base.gameObject, "Confirm") && !FCServices.FindChildWithName(base.gameObject, "Confirm").activeSelf && !FCServices.FindChildWithName(base.gameObject, "ToTitle"))
        {
            float fakevertical = 0;
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
            if (fakevertical != 0)
            {
                FCServices.FindChildWithName(base.gameObject, "Scroll View").GetComponent<ScrollRect>().content.transform.position += new Vector3(0f, 250f, 0f) * fakevertical * Time.deltaTime;
            }

        }
    }


}
