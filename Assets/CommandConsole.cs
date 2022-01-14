using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandConsole : MonoBehaviour
{
    public InputField console;

    // Update is called once per frame
    public void Start()
    {
        console = base.gameObject.GetComponentInChildren<InputField>();
    }

    public void Toggle() { 

        if (base.gameObject.GetComponent<Canvas>().enabled)
        {
            base.gameObject.GetComponent<Canvas>().enabled = false;
            console.DeactivateInputField();

        }
        else
        {
            base.gameObject.GetComponent<Canvas>().enabled = true;
            console.text = "";
            console.ActivateInputField();
        }
    }

    void Update()
    {
        if (GameObject.Find("SecretConsoleButton") != null)
        {
            if (GameObject.Find("SecretConsoleButton").GetComponent<Button>().interactable && GameObject.FindObjectOfType<Typewright>().gameObject.GetComponent<Text>().text == "")
            {
                GameObject.Find("SecretConsoleButton").GetComponent<Button>().interactable = false;
            }
            if (!GameObject.Find("SecretConsoleButton").GetComponent<Button>().interactable && GameObject.FindObjectOfType<Typewright>().gameObject.GetComponent<Text>().text != "")
            {
                GameObject.Find("SecretConsoleButton").GetComponent<Button>().interactable = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || base.gameObject.GetComponentInChildren<FCMakes.LazyPort.ButtonInputManager>().GetKeyDown)
        {
            if (console.text.Contains("LoadFRM"))
            {
                GameObject.FindObjectOfType<ModifyStrings>().LoadFromFRM(int.Parse(console.text.Split(char.Parse(" "))[1]));
            }
            if (console.text.Contains("LoadScene"))
            {
               UnityEngine.SceneManagement.SceneManager.LoadScene(console.text.Split(char.Parse(" "))[1]);
            }
            if (console.text.Contains("LoadSceneFromBundle"))
            {
                

                AssetBundle toloadfrom;
                toloadfrom = AssetBundleController.GetBundleFromName(console.text.Split(char.Parse(" "))[1].ToLower());
                string[] scenePath = toloadfrom.GetAllScenePaths();
                Debug.Log(scenePath[0]);
                UnityEngine.SceneManagement.SceneManager.LoadScene(scenePath[0]);

            }
          


                if (console.text.Contains("SelectAngle"))
            {
               string input = console.text.Split(char.Parse(" "))[1];
                GameObject.FindObjectOfType<MainCameraController>().SelectAngle(input);
            }
            if (console.text.Contains("TweenAngle"))
            {
                string input = console.text.Split(char.Parse(" "))[1];
                string input2 = console.text.Split(char.Parse(" "))[2];
                GameObject.FindObjectOfType<MainCameraController>().TweenAngle(input, int.Parse(input2));
            }
            if (console.text.Contains("Save"))
            {
                string input = console.text.Split(char.Parse(" "))[1];
                GameObject.FindObjectOfType<ModifyStrings>().Save(int.Parse(input));
            }
            if (console.text.Contains("Load") && !console.text.Contains("FRM") && !console.text.Contains("Scene"))
            {
                string input = console.text.Split(char.Parse(" "))[1];
                GameObject.FindObjectOfType<ModifyStrings>().Load(int.Parse(input));
            }
            if (console.text.Contains("SetAnimation"))
            {
                string input = console.text.Split(char.Parse(" "))[1].Replace("_", " ");
                string input2 = "is" + console.text.Split(char.Parse(" "))[2];

                FCServices.AllAnimatorBoolsToFalse(GameObject.FindObjectOfType<ModifyStrings>().FindCharacter(input).GetComponent<Animator>());
                GameObject.FindObjectOfType<ModifyStrings>().FindCharacter(input).GetComponent<Animator>().SetBool(input2, true);
                
                
            }
            if (console.text == "ClearSlotKey")
            {
                PlayerPrefs.DeleteKey("SlotToLoad");
                PlayerPrefs.Save();
            }

            if (console.text.Contains("PlayerPrefsSetFloat"))
            {
                PlayerPrefs.SetFloat(console.text.Split(char.Parse(" "))[1], float.Parse(console.text.Split(char.Parse(" "))[2]));
                PlayerPrefs.Save();
            }
            if (console.text.Contains("Press"))
            {
                FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().onClick.Invoke();
            }
            if (console.text.Contains("Present"))
            {
                FCServices.FindChildWithName("CEButtons", "Present").GetComponent<Button>().onClick.Invoke();
            }
            Toggle();
        }
       
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Toggle();
        }
    }
}
