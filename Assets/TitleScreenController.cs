using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject csb;

   public void Start()
    {
        if (AssetBundleController.bundles == null)
        {
            AssetBundleController.GenerateBundlesList();
        }
        foreach (string casename in AssetBundleController.casenames)
        {
            GameObject btn = Instantiate(csb, GameObject.Find("Cases").GetComponentInChildren<ScrollRect>().content.gameObject.transform);
            btn.GetComponentInChildren<Text>().text = casename;
        }

        Cursor.visible = true;
        Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("FadeEffect3"));
        if (File.Exists(Path.Combine(Application.persistentDataPath, "temp.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "temp.fcs"));
        }

        if (PlayerPrefs.HasKey("SEVolume"))
        {
            Debug.Log("has se volume key");
            foreach (AudioSource audiosource in FCServices.GetAllObjectsOfTypeInScene<AudioSource>())
            {

                if (GameObject.FindObjectOfType<MusicController>() != null && audiosource != GameObject.FindObjectOfType<MusicController>().gameObject.GetComponent<AudioSource>())
                {
                    audiosource.volume = PlayerPrefs.GetFloat("SEVolume");
                }
                else
                {
                    if (PlayerPrefs.HasKey("BGMVolume"))
                    {
                        audiosource.volume = PlayerPrefs.GetFloat("BGMVolume");
                    }
                }

            }
        }


        if (!Application.isEditor)
        {

            foreach (GameObject prefab in PrefabController.Instance().Prefabs)
            {
                if (prefab.GetComponents<AudioSource>().Length > 0)
                {
                    foreach (AudioSource au in prefab.GetComponents<AudioSource>())
                    {
                        au.volume = PlayerPrefs.GetFloat("SEVolume");
                    }
                }
                if (prefab.GetComponentsInChildren<AudioSource>().Length > 0)
                {
                    foreach (AudioSource au in prefab.GetComponentsInChildren<AudioSource>())
                    {
                        au.volume = PlayerPrefs.GetFloat("SEVolume");
                    }
                }
            }

            
        }
        foreach (Camera camera in GameObject.FindObjectsOfType<Camera>())
        {
            if (!camera.gameObject.GetComponent<CameraResolution>())
            {
                camera.gameObject.AddComponent<CameraResolution>();
            }

        }

        foreach (Canvas canvas in GameObject.FindObjectsOfType<Canvas>())
        {
            if (!canvas.gameObject.GetComponent<AspectRatioFitter>())
            {
                canvas.gameObject.AddComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            }
            else
            {
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.WidthControlsHeight;
            }

        }

    }

    public void Update()
    {
      
        if (FCServices.anyKeyUp() && FCServices.FindChildWithName(base.gameObject, "PressAnyKey").activeSelf)
        {
            Action a = () =>
            {
                GameObject.Find("ChoiceButtons").GetComponent<Canvas>().enabled = true;
                GameObject.Find("Blurred").GetComponent<Canvas>().enabled = true;

                base.gameObject.GetComponents<AudioSource>()[0].Play();
                base.gameObject.GetComponent<Animator>().SetBool("Started", true);
               
                if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot0")))
                {
                    FCServices.FindChildWithArray("ChoiceButtons", "Choices", "Choice2").SetActive(true);
                }
                else
                {
                    FCServices.FindChildWithArray("ChoiceButtons", "Choices", "Choice2").SetActive(false);
                }
            };
            base.StartCoroutine(FCServices.DoNextFrame(a));
          
        }

        if (Application.platform != RuntimePlatform.Android) { CursorUpdate(); }

        if (GameObject.Find("Cases").GetComponent<Canvas>())
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
                FCServices.FindChildWithName("Cases", "Scroll View").GetComponent<ScrollRect>().content.transform.position += new Vector3(0f, 250f, 0f) * fakevertical * Time.deltaTime;
            }
        }

    }

    

    public void CursorUpdate()
    {

        Vector2 position = CursorControl.GetPosition();
        float d = 1f;
        if (Gamepad.current != null && !GameObject.FindObjectOfType<OptionsController>().gameObject.GetComponent<Canvas>().enabled)
        {
            CursorControl.SetPosition(position + new Vector2(Gamepad.current.leftStick.ReadValue().x, Gamepad.current.leftStick.ReadValue().y * -1f) * Time.deltaTime * 250f * d * ((float)Screen.width / 695f));
        }


    }



    public void OpenLoadMenu()
    {
        GameObject.FindObjectOfType<SaveLoadController>().Toggle();
    }
    public void InvokeLoadGame()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        OpenLoadMenu();

    }

    public void InvokeShowOptions()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        Action open = () =>
        {
            GameObject.FindObjectOfType<OptionsController>().Show();
        };
        base.StartCoroutine(FCServices.DelayedAction(open, 1f));
    }

    public void InvokeNewGame()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        NewGame();
    }

    public void StartGame()
    {
        
        if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot0"))){
            Directory.Delete(Path.Combine(Application.persistentDataPath, "Slot0"), true);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        GameObject.Find("ChoiceButtons").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Cases").GetComponent<Canvas>().enabled = true;
        if (AssetBundleController.casenames.Count > 0)
        {
            FCServices.FindChildWithName("Cases", "Text").SetActive(false);
        }

    }

    public void BackToOptions()
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        GameObject.Find("ChoiceButtons").GetComponent<Canvas>().enabled = true;
        GameObject.Find("Cases").GetComponent<Canvas>().enabled = false;
    }

}
