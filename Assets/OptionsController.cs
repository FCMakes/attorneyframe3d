using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public GameObject BGM;
    public GameObject SE;
    public GameObject TextSkip;
    public GameObject ScreenShake;
    public GameObject Vibration;

    public void Show()
    {
        GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
        base.gameObject.GetComponent<Canvas>().enabled = true;
        Start();
    }
    public void Hide()
    {
        base.gameObject.GetComponent<Canvas>().enabled = false;
        base.gameObject.GetComponent<AudioSource>().Play();

    }


    public void Start()
    {
        BGM.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        SE.GetComponentInChildren<Slider>().value = PlayerPrefs.GetFloat("SEVolume", 1f);
        ButtonsUpdate();
    }

    public void DoDefault()
    {
        BGM.GetComponentInChildren<Slider>().value = 0.5f;
        SE.GetComponentInChildren<Slider>().value = 1f;
        PlayerPrefs.SetFloat("BGMVolume", 0.5f);
        PlayerPrefs.SetFloat("SEVolume", 1f);
        PlayerPrefs.SetInt("TextSkip", 0);
        PlayerPrefs.SetInt("ScreenShake", 1);
        PlayerPrefs.SetInt("Vibration", 1);
        PlayerPrefs.Save();
        ButtonsUpdate();
        GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
    }
    public void Update()
    {
        if (BGM.GetComponentInChildren<Slider>().value == 0 && BGM.GetComponentInChildren<Animator>().GetBool("soundOn") == true)
        {
            BGM.GetComponentInChildren<Animator>().SetBool("soundOn", false);
        }
        if (BGM.GetComponentInChildren<Slider>().value > 0 && BGM.GetComponentInChildren<Animator>().GetBool("soundOn") == false)
        {
            BGM.GetComponentInChildren<Animator>().SetBool("soundOn", true);
        }
        if (SE.GetComponentInChildren<Slider>().value == 0 && SE.GetComponentInChildren<Animator>().GetBool("soundOn") == true)
        {
            SE.GetComponentInChildren<Animator>().SetBool("soundOn", false);
        }
        if (SE.GetComponentInChildren<Slider>().value > 0 && SE.GetComponentInChildren<Animator>().GetBool("soundOn") == false)
        {
            SE.GetComponentInChildren<Animator>().SetBool("soundOn", true);
        }

        if (base.gameObject.GetComponent<Canvas>().enabled)
        {

            if (Input.GetKeyDown(KeyCode.JoystickButton3) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (PlayerPrefs.GetInt("TextSkip", 0) == 0)
                {
                    SetButtonValue1("TextSkip");
                }
                else
                {
                    SetButtonValue0("TextSkip");
                }
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (PlayerPrefs.GetInt("ScreenShake", 0) == 0)
                {
                    SetButtonValue1("ScreenShake");
                }
                else
                {
                    SetButtonValue0("ScreenShake");
                }
            }
            if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (PlayerPrefs.GetInt("Vibration", 0) == 0)
                {
                    SetButtonValue1("Vibration");
                }
                else
                {
                    SetButtonValue0("Vibration");
                }
            }

        }
    }

    public void ButtonsUpdate()
    {
        if (PlayerPrefs.GetInt("TextSkip", 0) == 1)
        {
            FCServices.FindChildWithName(TextSkip, "Button (1)").GetComponent<Animator>().SetBool("isOn", true);
            FCServices.FindChildWithName(TextSkip, "Button").GetComponent<Animator>().SetBool("isOn", false);
        }
        else
        {
            FCServices.FindChildWithName(TextSkip, "Button (1)").GetComponent<Animator>().SetBool("isOn", false);
            FCServices.FindChildWithName(TextSkip, "Button").GetComponent<Animator>().SetBool("isOn", true);
        }
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
        {
            FCServices.FindChildWithName(ScreenShake, "Button (1)").GetComponent<Animator>().SetBool("isOn", true);
            FCServices.FindChildWithName(ScreenShake, "Button").GetComponent<Animator>().SetBool("isOn", false);
        }
        else
        {
            FCServices.FindChildWithName(ScreenShake, "Button (1)").GetComponent<Animator>().SetBool("isOn", false);
            FCServices.FindChildWithName(ScreenShake, "Button").GetComponent<Animator>().SetBool("isOn", true);
        }
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {
            FCServices.FindChildWithName(Vibration, "Button (1)").GetComponent<Animator>().SetBool("isOn", true);
            FCServices.FindChildWithName(Vibration, "Button").GetComponent<Animator>().SetBool("isOn", false);
        }
        else
        {
            FCServices.FindChildWithName(Vibration, "Button (1)").GetComponent<Animator>().SetBool("isOn", false);
            FCServices.FindChildWithName(Vibration, "Button").GetComponent<Animator>().SetBool("isOn", true);
        }
    }
    public void BGMValueChange()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGM.GetComponentInChildren<Slider>().value);
        PlayerPrefs.Save();
        Debug.Log("Saved");
        if (GameObject.FindObjectOfType<MusicController>() != null)
        {
            GameObject.FindObjectOfType<MusicController>().gameObject.GetComponent<AudioSource>().volume = BGM.GetComponentInChildren<Slider>().value;
        }
    }

    public void SetButtonValue0(string prefname)
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        PlayerPrefs.SetInt(prefname, 0);
        PlayerPrefs.Save();
        ButtonsUpdate();
    }
    public void SetButtonValue1(string prefname)
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        PlayerPrefs.SetInt(prefname, 1);
        PlayerPrefs.Save();
        ButtonsUpdate();
    }
    public void SEValueChange()
    {
        PlayerPrefs.SetFloat("SEVolume", SE.GetComponentInChildren<Slider>().value);
        PlayerPrefs.Save();
        Debug.Log("Saved");
        foreach (AudioSource audiosource in FCServices.GetAllObjectsOfTypeInScene<AudioSource>())
        {

            if (GameObject.FindObjectOfType<MusicController>() != null && audiosource != GameObject.FindObjectOfType<MusicController>().gameObject.GetComponent<AudioSource>() || GameObject.FindObjectOfType<MusicController>() == null)
            {
                audiosource.volume = SE.GetComponentInChildren<Slider>().value;
            }
        }
        if (!Application.isEditor)
        {
            GameObject.FindObjectOfType<ModifyStrings>().UpdatePrefabSEVolume();
        }
        }


    public void ToggleSE()
    {
        if (SE.GetComponentInChildren<Slider>().value == 0)
        {
            SE.GetComponentInChildren<Slider>().value = 1;
        }
        else
        {
            SE.GetComponentInChildren<Slider>().value = 0;
        }
    }

    public void ToggleBGM()
    {
        if (BGM.GetComponentInChildren<Slider>().value == 0)
        {
            BGM.GetComponentInChildren<Slider>().value = 1;
        }
        else
        {
            BGM.GetComponentInChildren<Slider>().value = 0;
        }

    }

}
