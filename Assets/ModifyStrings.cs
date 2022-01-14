using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using DG.Tweening;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;


public class ModifyStrings : MonoBehaviour
{
    public bool isGameOver;
    public string CaseName;
    public string StoryName;
    public string PartNameCosmetic;
    public int StartFrame;
    public Typewright tw;
    public Text nametag;
    public int TextId;
    public AudioSource M;
    public AudioSource F;

    public Color Green;
    public AudioSource Music;
    public GameObject CE;
    public GameObject CEPrefab;
    public GameObject CESE;
    public Image NameBox;
    public Image EmptyBox;
    public bool isInCE;
    public int Wit;

    public GameObject HoldItBubblePrefab;
    public GameObject HoldItBubble;
    public Color Blue;
    public Animator Fader;


    public int Next;
    public Canvas Choice;
    public Text Choice1Holder;
    public Text Choice2Holder;
    public Text Choice3Holder;
    public int c1;
    public int c2;
    public int c3;
    public string[] lines;
    public int CurrentFRM;
    public GameObject WrightHoldItPrefab;
    public GameObject WrightObjectionPrefab;
    public GameObject EdgeworthObjectionPrefab;
    public GameObject WrightTakeThatPrefab;
    public GameObject Testimony;
    public Color Orange;
    public GameObject TestimonyMessage;
    public GameObject TMPrefab;

    public GameObject EvidenceSlider;
    public AudioSource RegularBlip;
    public GameObject CrossExaminationPrefab;
    public string[] CrossExaminationData;
    public GameObject ReactionBubble;
    public GameObject CurrentReaction;
    public GameObject BlackPsycheLocks3;
    public GameObject CurrentPsycheLocks;
    public bool canInvestigate;
    public GameObject ObjectObserve;
    public List<GameObject> ObserveCircles;
    public int GameOverFRM;
    public Texture2D Cursor;
    public bool isExamining3d;
    public GameObject ExaminedObject;
    public MainCameraController.CameraAngle angleBeforeExamine;
    public Texture2D RedCursor;
    public Texture2D CursorWithCheckmark;
    public Vector3 previousMousePosition;
    public int previousCEStatement;

    public System.Diagnostics.Stopwatch watch;





    public Color DarkerGreen;

    public AxisInput axisInput;
    public bool isTalkingToCharacter;
    public bool isPresentingToCharacter;
    public InteractableCharacterController talkingto;
    public GameObject EvidencePoppedUp;
    public Button SaveBTN;
    public Button CourtRecordBTN;
    public Button ExitRoomBTN;
    public GameObject WrightFront;
    public GameObject WrightSide;
    public List<bool> CEpressed;
    public Button PressButton;
    public Button PresentButton;

    public float pendingcamerachange;
    public GameObject PictureSpawned;


    public float rumble;

    public void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("CurrentSlot");
        PlayerPrefs.Save();
    }
    public void Start()
    {

        if (AssetBundleController.bundles == null)
        {
            AssetBundleController.GenerateBundlesList();
        }

        NameBox = FCServices.GetComponentFromGameObject<Image>("TextBoxBGName");
        EmptyBox = FCServices.GetComponentFromGameObject<Image>("TextBoxBGNoName");

        QualitySettings.SetQualityLevel(4);

        Force169Resolution();

        foreach (Button button in FCServices.GetAllObjectsOfTypeInScene<Button>())
        {
            Navigation nonnav = new Navigation();
            nonnav.mode = Navigation.Mode.None;
            button.navigation = nonnav;
        }
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            Debug.Log("has se volume key");
            foreach (AudioSource audiosource in FCServices.GetAllObjectsOfTypeInScene<AudioSource>())
            {

                if (audiosource != GameObject.FindObjectOfType<MusicController>().gameObject.GetComponent<AudioSource>())
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
            UpdatePrefabSEVolume();

        }

        if (GameObject.FindObjectOfType<AxisInput>())
        {
            axisInput = GameObject.FindObjectOfType<AxisInput>();
        }
        else
        {

            axisInput = base.gameObject.AddComponent<AxisInput>();
            base.gameObject.GetComponent<AxisInput>().axes = new List<AxisInput.AxisInfo>();


        }

        System.Globalization.CultureInfo.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        watch = new System.Diagnostics.Stopwatch();


        tw.silent = true;
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "temp.fcs")))
        {
            FCServices.FindChildWithName(GameObject.Find("FadeEffect"), "Panel").GetComponent<Animator>().SetTrigger("In");
            NameBox.enabled = false;
            EmptyBox.enabled = false;
            nametag.text = "";
            tw.gameObject.GetComponent<Text>().text = "";
            lines = new string[] { " ", "", "", "", "", StartFrame.ToString(), "", "", "", "", "" };
            if (Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot0")))
            {
                GameObject.FindObjectOfType<Record>().SyncCollectedEvidence(0);
            }
        }
        else
        {
            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("FadeEffect3"));

            int slotn = int.Parse(File.ReadAllLines(Path.Combine(Application.persistentDataPath, "temp.fcs"))[0]);


            Load(slotn);
            File.Delete(Path.Combine(Application.persistentDataPath, "temp.fcs"));

            GameObject.FindObjectOfType<Record>().SyncCollectedEvidence(slotn);



        }



        ObserveCircles = new List<GameObject>();




    }

    public void UpdatePrefabSEVolume()
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
            WrightHoldItPrefab.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEVolume");
            WrightObjectionPrefab.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEVolume");
            WrightTakeThatPrefab.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEVolume");
            EdgeworthObjectionPrefab.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SEVolume");
        }
    }
    public void Force169Resolution()
    {
        foreach (Camera camera in FCServices.GetAllObjectsOfTypeInScene<Camera>())
        {
            if (!camera.gameObject.GetComponent<CameraResolution>())
            {
                camera.gameObject.AddComponent<CameraResolution>();
            }

        }

        foreach (Canvas canvas in FCServices.GetAllObjectsOfTypeInScene<Canvas>())
        {
            canvas.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(800f, 450f);
            canvas.gameObject.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            if (!canvas.gameObject.GetComponent<AspectRatioFitter>())
            {
                canvas.gameObject.AddComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            }
            else
            {
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
            }

        }

        if (!Application.isEditor)
        {
            foreach (GameObject prefab in PrefabController.Instance().Prefabs)
            {
                if (prefab.GetComponent<Canvas>())
                {
                    Canvas canvas = prefab.GetComponent<Canvas>();
                    canvas.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(800f, 450f);
                    canvas.gameObject.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
                    if (!canvas.gameObject.GetComponent<AspectRatioFitter>())
                    {
                        canvas.gameObject.AddComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    }
                    else
                    {
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    }
                }
            }
            GameObject[] shouts = new GameObject[] { WrightHoldItPrefab, WrightObjectionPrefab, WrightTakeThatPrefab, EdgeworthObjectionPrefab };
            foreach (GameObject prefab in shouts)
            {
                if (prefab.GetComponent<Canvas>())
                {
                    Canvas canvas = prefab.GetComponent<Canvas>();
                    canvas.gameObject.GetComponent<CanvasScaler>().referenceResolution = new Vector2(800f, 450f);
                    canvas.gameObject.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
                    if (!canvas.gameObject.GetComponent<AspectRatioFitter>())
                    {
                        canvas.gameObject.AddComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    }
                    else
                    {
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectRatio = 16f / 9f;
                        canvas.gameObject.GetComponent<AspectRatioFitter>().aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
                    }
                }

            }


        }


    }

    public void AddLineTOCEData(string line)
    {
        List<string> CEDataNew = new List<string>();
        CEDataNew.AddRange(CrossExaminationData);
        CEDataNew.Add(line);
        CrossExaminationData = CEDataNew.ToArray();


    }

    public void CursorUpdate()
    {


        Vector2 cursorPos = CursorControl.GetPosition();
        float multiplier = 1;



        if (Gamepad.current != null && GameObject.FindObjectOfType<SaveLoadController>().gameObject.GetComponent<Canvas>().enabled)
        {
            CursorControl.SetPosition(cursorPos + new Vector2(Gamepad.current.leftStick.ReadValue().x, Gamepad.current.leftStick.ReadValue().y * -1f) * Time.deltaTime * 250f * multiplier * (Screen.width / 695f));
        }



    }


    public int CELine()
    {
        int id = 0;
        foreach (string line in CrossExaminationData)
        {

            if (line == CurrentFRM.ToString() && id != 5)
            {
                return id;

            }
            id += 1;

        }
        return -1;

    }

    public int NextCELine()
    {

        if (previousCEStatement == CrossExaminationData.Length - 1)
        {
            Debug.Log("is last");
            return int.Parse(CrossExaminationData[10]) + 2;
        }
        else
        {
            return int.Parse(CrossExaminationData[previousCEStatement + 1]);
        }
    }

    public WitnessController FindWitness(int Id)
    {
        foreach (WitnessController wc in GameObject.FindObjectsOfType<WitnessController>())
        {
            if (wc.WitnessId == Id)
            {
                return wc;
            }

        }
        return null;
    }

    public BaseCharacterController FindCharacter(string name)
    {
        foreach (BaseCharacterController bcc in GameObject.FindObjectsOfType<BaseCharacterController>())
        {

            if (bcc.CharacterName == name)
            {
                return bcc;

            }
        }
        return null;
    }


    public void RumbleController(float strength)
    {
        if (PlayerPrefs.GetInt("Vibration", 1) == 1)
        {

            if (UnityEngine.InputSystem.Gamepad.current != null)
            {
                DOTween.To(() => rumble, x => rumble = x, strength, 0.25f);
                Action stoprumble = () =>
                {
                    DOTween.To(() => rumble, x => rumble = x, 0, 0.25f);
                };
                base.StartCoroutine(FCServices.DelayedAction(stoprumble, 0.5f));
            }
            else
            {
                if (Application.platform == RuntimePlatform.Android)
                {
#if UNITY_ANDROID
                    Handheld.Vibrate();
#endif
                }
            }
        }
    }

    public void ShakeCamera()
    {
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
        {
            if (!DOTween.IsTweening(GameObject.Find("Cameras").transform))
            {
                GameObject.Find("Cameras").transform.DOShakePosition(0.1f, 0.1f);
            }
        }

    }

    public void NextButton()
    {
        if (lines.Length > 24)
        {
            if (lines[24] == "{CrossExamination}")
            {
                FRMNext();
            }

        }

    }

    public void PreviousButton()
    {
        if (lines.Length > 24)
        {
            if (lines[24] == "{CrossExamination}")
            {
                FRMPreviousCE();
            }

        }
    }







    public void FRMPreviousCE()
    {


        if (CurrentFRM != int.Parse(CrossExaminationData[11]))
        {
            LoadFromFRM(int.Parse(CrossExaminationData[CELine() - 1]));
        }


    }

    public void PsycheLocksExit()
    {
        GameObject.Find("BG").GetComponent<Animator>().SetTrigger("PsycheLockExit");
        Destroy(CurrentPsycheLocks);
    }
    public void FRMNext()
    {
        if (GameObject.FindObjectOfType<PenaltyController>().ActiveBadges.Count > 0 || this.isGameOver)
        {
            if (lines[5] == "+1")
            {

                LoadFromFRM(CurrentFRM + 1);
            }
            else
            {
                if (lines[5] == "ByCrossExamination")
                {
                    if (CurrentFRM == int.Parse(CrossExaminationData[CrossExaminationData.Length - 1]))
                    {
                        LoadFromFRM(int.Parse(CrossExaminationData[10]));
                    }
                    else
                    {
                        LoadFromFRM(int.Parse(CrossExaminationData[CELine() + 1]));
                    }
                }
                else
                {
                    LoadFromFRM(int.Parse(lines[5]));
                }
            }
        }
        else
        {
            LoadFromFRM(GameOverFRM);
            this.isGameOver = true;
        }
    }

    public void DestroySelectionFX()
    {

        foreach (Transform child in GameObject.Find("Picture").transform)
        {
            if (child.gameObject.name != "SelectionAppears")
            {
                Destroy(child.gameObject);
            }
        }

    }

    public IEnumerator LoadFRMDelayed(float time, int id)
    {
        yield return new WaitForSeconds(time);
        LoadFromFRM(id);
        yield break;

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
        if (UnityEngine.SceneManagement.SceneManager.GetSceneByName(MainFCS[0]) != null)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(MainFCS[0]);
        }

    }


    public void Load(int slotnumber)
    {


        string[] MainFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs"));

        Action frmload = () =>
        {
            LoadFromFRM(int.Parse(MainFCS[1]));
            tw.SkipTypewriter();
        };

        base.StartCoroutine(FCServices.DoNextFrame(frmload));


        if (GameObject.FindObjectsOfType<MoveController>().Length > 0)
        {
            GameObject player = GameObject.FindObjectOfType<MoveController>().gameObject;
            string[] PlayerTransformData = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs"));
            player.transform.position = new Vector3(float.Parse(PlayerTransformData[0]), float.Parse(PlayerTransformData[1]), float.Parse(PlayerTransformData[2]));
            player.transform.rotation = new Quaternion(float.Parse(PlayerTransformData[3]), float.Parse(PlayerTransformData[4]), float.Parse(PlayerTransformData[5]), float.Parse(PlayerTransformData[6]));
            FCServices.FindChildWithName(player, "Head").transform.rotation = new Quaternion(float.Parse(PlayerTransformData[7]), float.Parse(PlayerTransformData[8]), float.Parse(PlayerTransformData[9]), float.Parse(PlayerTransformData[10]));
        }

        Vector3 camPos = new Vector3(float.Parse(MainFCS[2]), float.Parse(MainFCS[3]), float.Parse(MainFCS[4]));
        Quaternion camRot = new Quaternion(float.Parse(MainFCS[5]), float.Parse(MainFCS[6]), float.Parse(MainFCS[7]), float.Parse(MainFCS[8]));
        MainCameraController cam = GameObject.FindObjectOfType<MainCameraController>();
        cam.gameObject.transform.SetPositionAndRotation(camPos, camRot);
        if (GameObject.FindObjectsOfType<MoveController>().Length > 0 && GameObject.FindObjectOfType<MoveController>().enabled)
        {
            cam.gameObject.transform.localPosition = Vector3.zero;
        }
        if (MainFCS[9] != "NoMusic")
        {

            Music.clip = Music.gameObject.GetComponent<MusicController>().FindTrack(MainFCS[9]);
            Music.Play();

        }
        Fader.Play(int.Parse(MainFCS[10]), 0);
        FCServices.FindChildWithName(GameObject.Find("FadeEffect2"), "Panel").GetComponent<Animator>().Play(int.Parse(MainFCS[11]), 0);




        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PsycheLocks.fcs")))
        {
            string[] PsycheLocksFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PsycheLocks.fcs"));
            CurrentPsycheLocks = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(PsycheLocksFCS[0]));
            CurrentPsycheLocks.name = PsycheLocksFCS[0];
            CurrentPsycheLocks.GetComponent<Animator>().Play(int.Parse(PsycheLocksFCS[1]), 0);
            GameObject.Find("BG").GetComponent<Animator>().Play(int.Parse(PsycheLocksFCS[2]), 0);


        }
        if (GameObject.FindObjectsOfType<BaseCharacterController>().Length > 0)
        {
            foreach (BaseCharacterController chr in GameObject.FindObjectsOfType<BaseCharacterController>())
            {
                string[] ChrFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "ChrData", chr.CharacterName + ".fcs"));
                for (int i = 0; i < chr.gameObject.GetComponent<Animator>().layerCount; i++)
                {
                    chr.gameObject.GetComponent<Animator>().Play(int.Parse(ChrFCS[i]), i);

                }
                int boolnum = chr.gameObject.GetComponent<Animator>().layerCount;
                foreach (AnimatorControllerParameter param in chr.gameObject.GetComponent<Animator>().parameters)
                {
                    if (param.type == AnimatorControllerParameterType.Bool)
                    {
                        if (ChrFCS[boolnum] == "True")
                        {
                            chr.gameObject.GetComponent<Animator>().SetBool(param.name, true);
                        }
                        else
                        {
                            chr.gameObject.GetComponent<Animator>().SetBool(param.name, false);
                        }
                        boolnum += 1;

                    }
                }

            }
        }



        if (GameObject.FindObjectOfType<Record>())
        {
            string[] EvidenceFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Evidence.fcs"));
            string[] ProfilesFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Profiles.fcs"));

            foreach (GameObject evidence in GameObject.FindObjectOfType<Record>().EvidenceList)
            {
               
                if (Array.IndexOf(EvidenceFCS, evidence.name) == -1)
                {
                    GameObject.FindObjectOfType<Record>().RemoveEvidence(evidence.name);
                }
            }
            foreach (GameObject profile in GameObject.FindObjectOfType<Record>().ProfilesList)
            {
                if (Array.IndexOf(ProfilesFCS, profile.name) == -1)
                {
                    GameObject.FindObjectOfType<Record>().RemoveProfile(profile.name);
                }
            }

            foreach (string s in EvidenceFCS)
            {
                if (s != "")
                {
                    if (!FCServices.FindChildWithArray(GameObject.FindObjectOfType<Record>().gameObject, "Image", s))
                    {
                        if (PrefabController.Instance().FindPrefab(s + "Details") != null)
                        {
                            GameObject.FindObjectOfType<Record>().AddEvidence(s, s + "Button", s + "Details");
                        }
                        else
                        {
                            GameObject.FindObjectOfType<Record>().AddEvidence(s, s + "Button");
                        }





                    }

                    FCServices.FindChildWithArray(GameObject.FindObjectOfType<Record>().gameObject, "Image", s, "Text (1)").GetComponent<Text>().text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", s + ".fcs"));
                    GameObject.FindObjectOfType<Record>().Switch();
                    GameObject.FindObjectOfType<Record>().Switch();

                }

            }
            foreach (string s in ProfilesFCS)
            {
                if (s != "")
                {
                    if (!FCServices.FindChildWithArray(GameObject.FindObjectOfType<Record>().gameObject, "Image", s))
                    {

                        GameObject.FindObjectOfType<Record>().AddProfile(s, s + "Button");


                    }

                    FCServices.FindChildWithArray(GameObject.FindObjectOfType<Record>().gameObject, "Image", s, "Text (1)").GetComponent<Text>().text = File.ReadAllText(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", s + ".fcs"));


                }

            }

        }

        if (GameObject.FindObjectsOfType<InteractableCharacterController>().Length > 0)
        {
            foreach (InteractableCharacterController ichr in GameObject.FindObjectsOfType<InteractableCharacterController>())
            {
                string[] EvidenceFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Evidence" + ".fcs"));
                string[] TopicsFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Topics" + ".fcs"));
                ichr.ToPresent.Clear();
                ichr.Topics.Clear();

                for (int i = 0; i < TopicsFCS.Length / 4; i++)
                {
                    InteractableCharacterController.Topic topic = new InteractableCharacterController.Topic();
                    topic.TopicName = TopicsFCS[i * 4];
                    topic.TopicFRM = int.Parse(TopicsFCS[i * 4 + 1]);
                    if (TopicsFCS[i * 4 + 2] == "True")
                    {
                        topic.talked = true;
                    }
                    if (TopicsFCS[i * 4 + 3] == "True")
                    {
                        topic.locked = true;
                    }
                    ichr.Topics.Add(topic);

                }
                for (int i = 0; i < EvidenceFCS.Length / 4; i++)
                {
                    InteractableCharacterController.PresentableEvidence ev = new InteractableCharacterController.PresentableEvidence();
                    ev.EvidenceName = EvidenceFCS[i * 4];
                    ev.EvidenceFRM = int.Parse(EvidenceFCS[i * 4 + 1]);
                    if (EvidenceFCS[i * 4 + 2] == "True")
                    {
                        ev.talked = true;
                    }
                    if (EvidenceFCS[i * 4 + 3] == "True")
                    {
                        ev.required = true;
                    }
                    ichr.ToPresent.Add(ev);

                }



            }

        }




        if (GameObject.FindObjectOfType<DRHP>() != null)
        {
            GameObject.FindObjectOfType<DRHP>().gameObject.GetComponent<Slider>().value = float.Parse(lines[12]);
        }
        if (GameObject.FindObjectOfType<PenaltyController>() != null)
        {
            for (int i = 0; i < (5 - float.Parse(MainFCS[12])); i++)
            {
                GameObject.FindObjectOfType<PenaltyController>().ActiveBadges[0].GetComponent<Image>().enabled = false;
                GameObject.FindObjectOfType<PenaltyController>().ActiveBadges.Remove(GameObject.FindObjectOfType<PenaltyController>().ActiveBadges[0]);
            }
        }

        if (MainFCS[13] != "None")
        {
            if (EvidencePoppedUp == null)
            {
                EvidencePoppedUp = Instantiate(PrefabController.Instance().FindPrefab(MainFCS[13]));
            }

            EvidencePoppedUp.GetComponentInChildren<Animator>().Play(int.Parse(MainFCS[14]), 0);
        }

        string[] AdditionalFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "AdditionalData.fcs"));
        FCServices.FindChildWithName(GameObject.Find("FadeEffectWhite"), "Panel").GetComponent<Animator>().Play(int.Parse(AdditionalFCS[0]), 0);
        GameObject.Find("Darkness").GetComponent<Animator>().Play(int.Parse(AdditionalFCS[1]), 0);
        if (AdditionalFCS[1] == "1767186682")
        {
            GameObject.Find("Darkness").GetComponent<Animator>().SetBool("isBGVisible", false);
        }
        else
        {
            GameObject.Find("Darkness").GetComponent<Animator>().SetBool("isBGVisible", true);
        }

        if (AdditionalFCS[2] != "NoPicture")
        {
            PictureSpawned = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(AdditionalFCS[2]), GameObject.Find("Picture").transform);
        }

        if (AdditionalFCS[3] != "NotExamining")
        {
            foreach (Transform child in ExaminedObject.transform)
            {
                if (child.name == AdditionalFCS[3])
                {
                    child.gameObject.SetActive(true);
                    child.SetAsFirstSibling();
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            if (AdditionalFCS[4] != "NoAnimatorAttached")
            {
                ExaminedObject.transform.GetChild(0).gameObject.GetComponent<Animator>().Play(int.Parse(AdditionalFCS[4]), 0);
            }
            Quaternion savedRot = new Quaternion(float.Parse(AdditionalFCS[5]), float.Parse(AdditionalFCS[6]), float.Parse(AdditionalFCS[7]), float.Parse(AdditionalFCS[8]));
            ExaminedObject.transform.rotation = savedRot;
        }
        Vector2 pointerAnchorPos = new Vector2(float.Parse(AdditionalFCS[9]), float.Parse(AdditionalFCS[10]));
        GameObject.FindObjectOfType<FakeCursorController>().gameObject.GetComponent<RectTransform>().anchoredPosition = pointerAnchorPos;
        if (AdditionalFCS[11] == "True")
        {
            isGameOver = true;
        }
        else
        {
            isGameOver = false;
        }

        GameObject.FindObjectOfType<PenaltyController>().transform.GetChild(0).gameObject.GetComponent<Animator>().Play(int.Parse(AdditionalFCS[12]), 0);
        if (GameObject.FindObjectOfType<PenaltyController>().PendingPenalty() != int.Parse(AdditionalFCS[13]))
        {
            GameObject.FindObjectOfType<PenaltyController>().ShowPendingPenalty(int.Parse(AdditionalFCS[13]));
        }

        if (GameObject.FindObjectsOfType<SecondaryCameraController>().Length > 0)
        {
            foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
            {
                string[] SCD = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "SecondaryCamData", scc.name + ".fcs"));
                Vector3 camPosNew = new Vector3(float.Parse(SCD[0]), float.Parse(SCD[1]), float.Parse(SCD[2]));
                Quaternion camRotNew = new Quaternion(float.Parse(SCD[3]), float.Parse(SCD[4]), float.Parse(SCD[5]), float.Parse(SCD[6]));
                scc.gameObject.transform.SetPositionAndRotation(camPosNew, camRotNew);
            }
        }

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "MainCEData.fcs")))
        {
            CrossExaminationData = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "MainCEData.fcs"));
        }
        CEpressed = new List<bool>();
        foreach (Transform child in GameObject.Find("StatementDots").transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < CrossExaminationData.Length - 11; i++)
        {
            CEpressed.Add(false);

            Instantiate(PrefabController.Instance().FindPrefab("StatementDot"), GameObject.Find("StatementDots").transform);

        }
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "AdditionalCEData.fcs")))
        {


            string[] bools = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "AdditionalCEData.fcs"));
            previousCEStatement = int.Parse(bools[0]);
            for (int i = 0; i < CEpressed.Count; i++)
            {
                if (bools[i + 1] == "True")
                {
                    CEpressed[i] = true;
                }
                else
                {
                    CEpressed[i] = false;
                }
            }
        }


        if (GameObject.FindObjectsOfType<InvestigationObject>().Length > 0)
        {
            string[] InvestigationFCS = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Investigation.fcs"));

            for (int i = 0; i < GameObject.FindObjectsOfType<InvestigationObject>().Length; i++)
            {
                int objc = i + 1;
                if (InvestigationFCS[objc] == "True")
                {
                    GameObject.FindObjectsOfType<InvestigationObject>()[i].Investigated = true;

                }
                else
                {
                    GameObject.FindObjectsOfType<InvestigationObject>()[i].Investigated = false;

                }
            }

            if (InvestigationFCS[0] != "NoExit")
            {
                if (InvestigationFCS[0] == "True")
                {
                    GameObject.FindObjectOfType<ExitRoomObject>().canLeave = true;
                }
                else
                {
                    GameObject.FindObjectOfType<ExitRoomObject>().canLeave = false;
                }
            }




        }
        foreach (string filepath3 in Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData")))
        {
            string[] UnitData = File.ReadAllLines(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData", "Unit" + Array.IndexOf(Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData")), filepath3) + ".fcs"));

            if (UnitData[1] != "No previous messages.")
            {
                Color txclr = new Color(float.Parse(UnitData[2]), float.Parse(UnitData[3]), float.Parse(UnitData[4]));
                TextAnchor usedanchor;
                if (UnitData[5] == "Left")
                {
                    usedanchor = TextAnchor.UpperLeft;
                }
                else
                {
                    usedanchor = TextAnchor.UpperCenter;
                }
                GameObject.FindObjectOfType<BacklogController>().AddMessage(UnitData[0], UnitData[1], txclr, usedanchor);
                if (Array.IndexOf(Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData")), filepath3) != Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData")).Length - 1)
                {
                    GameObject.FindObjectOfType<BacklogController>().CommitPrevious();
                }

            }

        }
    }

    public void Save(int slotnumber)
    {

        PlayerPrefs.SetString("SaveTime" + slotnumber, DateTime.Now.ToString());
        PlayerPrefs.Save();


        string[] subdirectorynames = new string[] { "ChrData", "CourtRecord", "InteractableChrData", "CEData", "SecondaryCamData", "BacklogData" };
        if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber)))
        {
            Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber));


        }
        foreach (string subdir in subdirectorynames)
        {
            if (!Directory.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, subdir)))
            {
                Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, subdir));


            }
        }

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs"));
        }
        StreamWriter writer0 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Main.fcs"));

        string scenedata = (CaseName + @"/" + Array.IndexOf(AssetBundleController.GetBundleFromName(CaseName.ToLower()).GetAllScenePaths(), UnityEngine.SceneManagement.SceneManager.GetActiveScene().path) + @"/" + PartNameCosmetic);
        writer0.WriteLine(scenedata);
        writer0.WriteLine(CurrentFRM);
        MainCameraController cam = GameObject.FindObjectOfType<MainCameraController>();
        writer0.WriteLine(cam.gameObject.transform.position.x);
        writer0.WriteLine(cam.gameObject.transform.position.y);
        writer0.WriteLine(cam.gameObject.transform.position.z);
        writer0.WriteLine(cam.gameObject.transform.rotation.x);
        writer0.WriteLine(cam.gameObject.transform.rotation.y);
        writer0.WriteLine(cam.gameObject.transform.rotation.z);
        writer0.WriteLine(cam.gameObject.transform.rotation.w);
        if (Music.isPlaying)
        {
            writer0.WriteLine(Music.clip.name);
        }
        else
        {
            writer0.WriteLine("NoMusic");
        }
        writer0.WriteLine(Fader.GetCurrentAnimatorStateInfo(0).shortNameHash);
        writer0.WriteLine(FCServices.FindChildWithName(GameObject.Find("FadeEffect2"), "Panel").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);

        if (GameObject.FindObjectOfType<PenaltyController>() != null)
        {
            writer0.WriteLine(GameObject.FindObjectOfType<PenaltyController>().ActiveBadges.Count);
        }
        if (GameObject.FindObjectOfType<DRHP>() != null)
        {
            writer0.WriteLine(GameObject.FindObjectOfType<DRHP>().gameObject.GetComponent<Slider>().value);
        }


        if (EvidencePoppedUp != null)
        {
            writer0.WriteLine(EvidencePoppedUp.name.Replace("(Clone)", ""));
            writer0.WriteLine(EvidencePoppedUp.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
        }
        else
        {
            writer0.WriteLine("None");
        }
        writer0.Close();

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PsycheLocks.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PsycheLocks.fcs"));
        }
        if (CurrentPsycheLocks != null)
        {

            StreamWriter writer01 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PsycheLocks.fcs"));

            writer01.WriteLine(CurrentPsycheLocks.name);
            writer01.WriteLine(CurrentPsycheLocks.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
            writer01.WriteLine(GameObject.Find("BG").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
            writer01.Close();
        }


        if (GameObject.FindObjectsOfType<BaseCharacterController>().Length > 0)
        {
            foreach (BaseCharacterController chr in GameObject.FindObjectsOfType<BaseCharacterController>()) {
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "ChrData", chr.CharacterName + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "ChrData", chr.CharacterName + ".fcs"));
                }
                StreamWriter writer1 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "ChrData", chr.CharacterName + ".fcs"));



                for (int i = 0; i < chr.gameObject.GetComponent<Animator>().layerCount; i++)
                {
                    writer1.WriteLine(chr.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(i).shortNameHash);

                }

                foreach (AnimatorControllerParameter param in chr.gameObject.GetComponent<Animator>().parameters)
                {
                    if (param.type == AnimatorControllerParameterType.Bool)
                    {
                        writer1.WriteLine(chr.gameObject.GetComponent<Animator>().GetBool(param.name));
                    }
                }
                writer1.Close();

            }




        }
        if (GameObject.FindObjectsOfType<InvestigationObject>().Length > 0)
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Investigation.fcs")))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Investigation.fcs"));
            }
            StreamWriter writer2 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "Investigation.fcs"));


            if (GameObject.FindObjectOfType<ExitRoomObject>() != null)
            {
                writer2.WriteLine(GameObject.FindObjectOfType<ExitRoomObject>().canLeave);
            }
            else
            {
                writer2.WriteLine("NoExit");
            }

            foreach (InvestigationObject iobj in GameObject.FindObjectsOfType<InvestigationObject>())
            {
                writer2.WriteLine(iobj.Investigated);
            }
            writer2.Close();

        }

        if (GameObject.FindObjectOfType<Record>())
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Evidence.fcs")))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Evidence.fcs"));
            }
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Profiles.fcs")))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Profiles.fcs"));
            }
            StreamWriter writer3 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Evidence.fcs"));
            StreamWriter writer4 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", "Profiles.fcs"));
            foreach (GameObject go in GameObject.FindObjectOfType<Record>().EvidenceList)
            {
                writer3.WriteLine(go.name);
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs"));
                }
                StreamWriter writer5 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs"));
                writer5.Write(FCServices.FindChildWithName(go, "Text (1)").GetComponent<Text>().text);
                writer5.Close();

            }
            writer3.Close();

            foreach (GameObject go in GameObject.FindObjectOfType<Record>().ProfilesList)
            {
                writer4.WriteLine(go.name);
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs"));
                }
                StreamWriter writer5 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CourtRecord", go.name + ".fcs"));
                writer5.Write(FCServices.FindChildWithName(go, "Text (1)").GetComponent<Text>().text);
                writer5.Close();

            }
            writer4.Close();


        }
        if (GameObject.FindObjectsOfType<InteractableCharacterController>().Length > 0)
        {
            foreach (InteractableCharacterController ichr in GameObject.FindObjectsOfType<InteractableCharacterController>())
            {
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Topics" + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Topics" + ".fcs"));
                }
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Evidence" + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Evidence" + ".fcs"));
                }
                StreamWriter writer6 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Topics" + ".fcs"));
                StreamWriter writer7 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "InteractableChrData", ichr.characterName + "Evidence" + ".fcs"));
                foreach (InteractableCharacterController.Topic topic in ichr.Topics)
                {
                    writer6.WriteLine(topic.TopicName);
                    writer6.WriteLine(topic.TopicFRM);
                    writer6.WriteLine(topic.talked);
                    writer6.WriteLine(topic.locked);

                }
                writer6.Close();
                foreach (InteractableCharacterController.PresentableEvidence ev in ichr.ToPresent)
                {
                    writer7.WriteLine(ev.EvidenceName);
                    writer7.WriteLine(ev.EvidenceFRM);
                    writer7.WriteLine(ev.talked);
                    writer7.WriteLine(ev.required);

                }
                writer7.Close();



            }


        }

        foreach (string path in Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "SecondaryCamData")))
        {
            File.Delete(path);
        }

        if (GameObject.FindObjectsOfType<SecondaryCameraController>().Length > 0)
        {
            foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>()) {
                if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "SecondaryCamData", scc.name + ".fcs")))
                {
                    File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "SecondaryCamData", scc.name + ".fcs"));
                }
                StreamWriter writer8 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "SecondaryCamData", scc.name + ".fcs"));
                writer8.WriteLine(scc.gameObject.transform.position.x);
                writer8.WriteLine(scc.gameObject.transform.position.y);
                writer8.WriteLine(scc.gameObject.transform.position.z);
                writer8.WriteLine(scc.gameObject.transform.rotation.x);
                writer8.WriteLine(scc.gameObject.transform.rotation.y);
                writer8.WriteLine(scc.gameObject.transform.rotation.z);
                writer8.WriteLine(scc.gameObject.transform.rotation.w);
                writer8.Close();

            }
        }

        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "AdditionalData.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "AdditionalData.fcs"));
        }
        StreamWriter writer9 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "AdditionalData.fcs"));
        writer9.WriteLine(FCServices.FindChildWithName(GameObject.Find("FadeEffectWhite"), "Panel").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
        writer9.WriteLine(GameObject.Find("Darkness").GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
        if (PictureSpawned != null)
        {
            writer9.WriteLine(PictureSpawned.name.Replace("(Clone)", ""));
        }
        else
        {
            writer9.WriteLine("NoPicture");
        }
        if (ExaminedObject.transform.childCount > 0)
        {
            writer9.WriteLine(ExaminedObject.transform.GetChild(0).gameObject.name);
            if (ExaminedObject.transform.GetChild(0).gameObject.GetComponent<Animator>())
            {
                writer9.WriteLine(ExaminedObject.transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
            }
            else
            {
                writer9.WriteLine("NoAnimatorAttached");
            }
            writer9.WriteLine(ExaminedObject.transform.rotation.x);
            writer9.WriteLine(ExaminedObject.transform.rotation.y);
            writer9.WriteLine(ExaminedObject.transform.rotation.z);
            writer9.WriteLine(ExaminedObject.transform.rotation.w);
        }
        else
        {

            writer9.WriteLine("NotExamining");
            writer9.WriteLine("NoAnimatorAttached");
            writer9.WriteLine(0);
            writer9.WriteLine(0);
            writer9.WriteLine(0);
            writer9.WriteLine(0);
        }
        writer9.WriteLine(GameObject.FindObjectOfType<FakeCursorController>().gameObject.GetComponent<RectTransform>().anchoredPosition.x);
        writer9.WriteLine(GameObject.FindObjectOfType<FakeCursorController>().gameObject.GetComponent<RectTransform>().anchoredPosition.y);
        writer9.WriteLine(isGameOver);
        writer9.WriteLine(GameObject.FindObjectOfType<PenaltyController>().transform.GetChild(0).gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash);
        writer9.WriteLine(GameObject.FindObjectOfType<PenaltyController>().PendingPenalty());
        writer9.Close();


        foreach (string path2 in Directory.GetFiles(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData")))
        {
            File.Delete(path2);
        }
        foreach (Transform unit in GameObject.FindObjectOfType<BacklogController>().gameObject.GetComponentInChildren<VerticalLayoutGroup>().transform)
        {
            StreamWriter writer10 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "BacklogData", "Unit" + unit.GetSiblingIndex() + ".fcs"));
            writer10.WriteLine(unit.GetChild(0).gameObject.GetComponent<Text>().text);
            writer10.WriteLine(unit.GetChild(1).gameObject.GetComponent<Text>().text);
            writer10.WriteLine(unit.GetChild(1).gameObject.GetComponent<Text>().color.r);
            writer10.WriteLine(unit.GetChild(1).gameObject.GetComponent<Text>().color.g);
            writer10.WriteLine(unit.GetChild(1).gameObject.GetComponent<Text>().color.b);
            if (unit.GetChild(1).gameObject.GetComponent<Text>().alignment == TextAnchor.UpperLeft)
            {
                writer10.WriteLine("Left");

            }
            if (unit.GetChild(1).gameObject.GetComponent<Text>().alignment == TextAnchor.UpperCenter)
            {
                writer10.WriteLine("Center");

            }
            writer10.Close();
        }
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "MainCEData.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "MainCEData.fcs"));
        }
        if (CrossExaminationData != null)
        {
            StreamWriter writer11 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "MainCEData.fcs"));
            foreach (string line in CrossExaminationData)
            {
                writer11.WriteLine(line);
            }
            writer11.Close();
        }
        if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "AdditionalCEData.fcs")))
        {
            File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "AdditionalCEData.fcs"));
        }
        StreamWriter writer12 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "CEData", "AdditionalCEData.fcs"));
        writer12.WriteLine(previousCEStatement);
        if (CEpressed != null)
        {

            foreach (bool line in CEpressed)
            {
                writer12.WriteLine(line);
            }

        }
        writer12.Close();

        if (GameObject.FindObjectOfType<MoveController>() != null)
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs")))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs"));
            }
            GameObject player = GameObject.FindObjectOfType<MoveController>().gameObject;
            StreamWriter writer8 = new StreamWriter(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs"));
            writer8.WriteLine(player.transform.position.x);
            writer8.WriteLine(player.transform.position.y);
            writer8.WriteLine(player.transform.position.z);
            writer8.WriteLine(player.transform.rotation.x);
            writer8.WriteLine(player.transform.rotation.y);
            writer8.WriteLine(player.transform.rotation.z);
            writer8.WriteLine(player.transform.rotation.w);
            writer8.WriteLine(FCServices.FindChildWithName(player, "Head").transform.rotation.x);
            writer8.WriteLine(FCServices.FindChildWithName(player, "Head").transform.rotation.y);
            writer8.WriteLine(FCServices.FindChildWithName(player, "Head").transform.rotation.z);
            writer8.WriteLine(FCServices.FindChildWithName(player, "Head").transform.rotation.w);
            writer8.Close();
        }
        else
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs")))
            {
                File.Delete(Path.Combine(Application.persistentDataPath, "Slot" + slotnumber, "PlayerPosition.fcs"));
            }
        }

        Debug.Log("Saved to " + slotnumber);

    }

    public void BackToInvestigation()
    {
        GameObject.Find("Interaction").GetComponent<Canvas>().enabled = false;
        GameObject.Find("BackSE").GetComponent<AudioSource>().Play();
        LoadFromFRM(talkingto.backToInvestigationFRM);
        isTalkingToCharacter = false;
        isPresentingToCharacter = false;
        talkingto = null;




    }












    public void Update()

    {
        if (Application.platform != RuntimePlatform.Android) {
            CursorUpdate(); }

        if (GameObject.Find("Unknown").GetComponent<Text>().enabled && lines.Length <= 25f || GameObject.Find("Unknown").GetComponent<Text>().enabled && lines.Length > 25f && lines[25] != "UnknownSpeaker")
        {
            GameObject.Find("Unknown").GetComponent<Text>().enabled = false;
        }

        if (GameObject.Find("CEButtons") != null)
        {
            if (CELine() > 10 && GameObject.Find("CEButtons").GetComponent<Canvas>().enabled == false)
            {
                GameObject.Find("CEButtons").GetComponent<Canvas>().enabled = true;

                FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().onClick.RemoveAllListeners();
                FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().onClick.AddListener(Press);
                FCServices.FindChildWithName("CEButtons", "Present").GetComponent<Button>().onClick.RemoveAllListeners();
                FCServices.FindChildWithName("CEButtons", "Present").GetComponent<Button>().onClick.AddListener(GameObject.FindObjectOfType<Record>().Present);
            }
            if (CELine() <= 10 && GameObject.Find("CEButtons").GetComponent<Canvas>().enabled)
            {
                GameObject.Find("CEButtons").GetComponent<Canvas>().enabled = false;
            }

            if (tw.isTyping && FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().interactable)
            {
                FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().interactable = false;
                FCServices.FindChildWithName("CEButtons", "Present").GetComponent<Button>().interactable = false;
            }
            if (!tw.isTyping && !FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().interactable)
            {
                FCServices.FindChildWithName("CEButtons", "Press").GetComponent<Button>().interactable = true;
                FCServices.FindChildWithName("CEButtons", "Present").GetComponent<Button>().interactable = true;
            }
        }

        if (this.TestimonyMessage != null && lines.Length < 25 || this.TestimonyMessage != null && lines.Length >= 25 && !lines[24].Contains("{"))
        {
            Destroy(this.TestimonyMessage);
        }





        if (GameObject.Find("Interaction") != null)
        {
            FCServices.FindChildWithName("Interaction", "Button (8)").GetComponent<ImitateOnClick>().enabled = GameObject.Find("Interaction").GetComponent<Canvas>().enabled;
        }


        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(rumble, rumble);
        }







        if (!NameBox.enabled && !EmptyBox.enabled && tw.gameObject.GetComponent<Text>().text != "" && lines[0] != "[DebateComment]")
        {
            tw.gameObject.GetComponent<Text>().text = "";
        }


        if (lines.Length > 12 && lines[12] == "Auto" && tw.isTyping == false && tw.gameObject.GetComponent<Text>().text == lines[4])
        {
            if (lines.Length < 26 || lines.Length > 25 && lines[25] != "AutoProceedDelayed")
            {
                this.FRMNext();
            }
            else
            {
                if (lines.Length > 25 && lines[25] == "AutoProceedDelayed" && !IsInvoking("FRMNext"))
                {
                    Invoke("FRMNext", float.Parse(lines[26]));
                }
            }
        }

        if (!isAnyMenusOpen())
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (!canInvestigate && !isExamining3d && lines[0] != "[PointInPicture]")
                {
                    ProceedButton button = GameObject.FindObjectOfType<ProceedButton>();
                    if (button.gameObject.GetComponent<Button>().enabled && button.gameObject.GetComponent<Button>().interactable)
                    {
                        button.Proceed();
                    }

                }

            }

            if (GameObject.FindObjectOfType<BackButtonCE>() && GameObject.FindObjectOfType<BackButtonCE>().gameObject.GetComponent<Button>().interactable)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) || Gamepad.current != null && Gamepad.current.dpad.left.wasPressedThisFrame)
                {
                    GameObject.FindObjectOfType<BackButtonCE>().GoBack();
                }
            }

        }




        if (SaveBTN != null)
        {
            if (!isDynamic())
            {
                if (!SaveBTN.interactable)
                {
                    SaveBTN.interactable = true;
                }

            }
            else
            {
                if (SaveBTN.interactable)
                {
                    SaveBTN.interactable = false;
                }
            }

        }

        if (CourtRecordBTN != null)
        {
            if (!isDynamic())
            {
                if (!CourtRecordBTN.interactable)
                {
                    CourtRecordBTN.interactable = true;
                }
            }
            else
            {
                if (CourtRecordBTN.interactable)
                {
                    CourtRecordBTN.interactable = false;
                }
            }
        }

        if (ExitRoomBTN != null)
        {
            if (canInvestigate && !ExitRoomBTN.gameObject.activeInHierarchy)
            {
                ExitRoomBTN.gameObject.SetActive(true);
            }
            if (!canInvestigate && ExitRoomBTN.gameObject.activeInHierarchy)
            {
                ExitRoomBTN.gameObject.SetActive(false);
            }

        }







        if (!canInvestigate && !isExamining3d && !isAnyMenusOpen())
        {
            if (NameBox.enabled || EmptyBox.enabled)
            {
                if (tw.isTyping && Input.GetKeyDown(KeyCode.Return) || tw.isTyping && Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    TextSkip();
                }
            }



        }



        if (Choice.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithName(Choice.gameObject, "Button"));

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithName(Choice.gameObject, "Button (1)"));

            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithName(Choice.gameObject, "Button (2)"));

            }

            if (Input.GetKeyUp(KeyCode.Alpha1) || Input.GetKeyUp(KeyCode.JoystickButton2))
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithName(Choice.gameObject, "Button"));
                PickC1();


            }
            if (Input.GetKeyUp(KeyCode.Alpha2) || Input.GetKeyUp(KeyCode.JoystickButton3))
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithName(Choice.gameObject, "Button (1)"));
                PickC2();

            }
            if (Input.GetKeyUp(KeyCode.Alpha3) || Input.GetKeyUp(KeyCode.JoystickButton1))
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithName(Choice.gameObject, "Button (2)"));
                PickC3();

            }




        }










        if (lines.Length > 24 && lines[24] == "PsycheLockExit")
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                LoadFromFRM(int.Parse(lines[25]));
            }
        }










        if (lines.Length > 31 && lines[31] == "{Reaction}" && tw.isTyping == false && tw.gameObject.GetComponent<Text>().text == lines[4] && CurrentReaction == null)
        {
            WitnessController reactor = this.FindWitness(int.Parse(lines[32]));
            CurrentReaction = Instantiate(ReactionBubble);
            CurrentReaction.transform.SetParent(reactor.HeadBone.transform);
            CurrentReaction.transform.localPosition = reactor.ReactionBubblePos;
            CurrentReaction.transform.localRotation = reactor.ReactionBubbleRot;


        }





        if (isExamining3d)
        {
            if (RenderSettings.fog)
            {
                RenderSettings.fog = false;
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

            if (Application.platform == RuntimePlatform.Android)
            {

                if (Input.touches.Length == 1 && Input.GetTouch(0).deltaPosition.x != 0)
                {
                    fakehorizontal = Input.GetTouch(0).deltaPosition.x / Screen.width * -250f;
                }
                if (Input.touches.Length == 1 && Input.GetTouch(0).deltaPosition.y != 0)
                {
                    fakevertical = Input.GetTouch(0).deltaPosition.y / Screen.height * 250f;
                }
            }


            ExaminedObject.transform.RotateAround(GameObject.Find("BM").transform.position, GameObject.Find("BM").transform.up, fakehorizontal * 80f * Time.deltaTime);
            ExaminedObject.transform.RotateAround(GameObject.Find("HN").transform.position, GameObject.Find("HN").transform.up, fakevertical * 80f * Time.deltaTime);



            if (Input.GetKey(KeyCode.G) || Gamepad.current != null && Gamepad.current.leftTrigger.isPressed)
            {
                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += Vector3.forward * -10f * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.J) || Gamepad.current != null && Gamepad.current.rightTrigger.isPressed)
            {
                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += Vector3.forward * 10f * Time.deltaTime;

            }

            if (Application.platform == RuntimePlatform.Android && Input.touches.Length == 2)
            {

                Touch firstTouch = Input.GetTouch(0);
                Touch secondTouch = Input.GetTouch(1);

                Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
                Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

                float touchesPrevPosDifference = (firstTouchPrevPos - secondTouchPrevPos).magnitude;
                float touchesCurPosDifference = (firstTouch.position - secondTouch.position).magnitude;

                float zoomModifier = (firstTouch.deltaPosition / Screen.width - secondTouch.deltaPosition / Screen.height).magnitude;

                if (touchesPrevPosDifference > touchesCurPosDifference)
                {
                    GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position -= Vector3.forward * zoomModifier * 2500f * Time.deltaTime;
                } if (touchesPrevPosDifference < touchesCurPosDifference)
                {
                    GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += Vector3.forward * zoomModifier * 2500f * Time.deltaTime;
                }


            }



            if (GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Examine3dArrows>().GetKey || GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Examine3dArrows>().AltGetKey())
            {
                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.right * 1f * Time.deltaTime * 2.5f;


            }
            if (GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.GetComponent<Examine3dArrows>().GetKey || GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.GetComponent<Examine3dArrows>().AltGetKey())
            {
                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.right * -1f * Time.deltaTime * 2.5f;


            }
            if (GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.GetComponent<Examine3dArrows>().GetKey || GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.GetComponent<Examine3dArrows>().AltGetKey())
            {

                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.up * 1f * Time.deltaTime * 2.5f;


            }
            if (GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.GetComponent<Examine3dArrows>().GetKey || GameObject.Find("ZoomedInArrows") != null && GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.GetComponent<Examine3dArrows>().AltGetKey())
            {
                GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position += GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.up * -1f * Time.deltaTime * 2.5f;


            }

            float inversedlerp = Mathf.InverseLerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamine").position.z, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.z, GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.z);
            Debug.Log(inversedlerp);

            float lerpup = Mathf.Lerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.y, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomInUp").position.y, inversedlerp);
            float lerpdown = Mathf.Lerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.y, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomInDown").position.y, inversedlerp);
            float lerpright = Mathf.Lerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.x, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomInRight").position.x, inversedlerp);
            float lerpleft = Mathf.Lerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.x, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomInLeft").position.x, inversedlerp);

            GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position = new Vector3(Mathf.Clamp(GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.x, lerpright, lerpleft), Mathf.Clamp(GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.y, lerpup, lerpdown), Mathf.Clamp(GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.z, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomOut").position.z, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.z)); ;


            if (GameObject.Find("ZoomedInArrows") != null)
            {
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.x == lerpleft && GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Image>().enabled = false;
                    Debug.Log("to false");
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.x != lerpleft && !GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(0).gameObject.GetComponent<Image>().enabled = true;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.x == lerpright && GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.GetComponent<Image>().enabled = false;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.x != lerpright && !GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(1).gameObject.gameObject.GetComponent<Image>().enabled = true;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.y == lerpdown && GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.y != lerpdown && !GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(2).gameObject.gameObject.GetComponent<Image>().enabled = true;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.y == lerpup && GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.gameObject.GetComponent<Image>().enabled = false;
                }
                if (GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.y != lerpup && !GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.GetComponent<Image>().enabled)
                {
                    GameObject.Find("ZoomedInArrows").transform.GetChild(3).gameObject.GetComponent<Image>().enabled = true;
                }
            }
        }


        if (!RenderSettings.fog && FCServices.GetComponentFromGameObject<SecondaryCameraController>("BGCamera").WithName("3dExamine").position != GameObject.Find("BGCamera").transform.position)
        {
            RenderSettings.fog = true;
        }
        if (!RenderSettings.fog && FCServices.GetComponentFromGameObject<SecondaryCameraController>("BGCamera").WithName("3dExamine").position == GameObject.Find("BGCamera").transform.position)
        {
            RenderSettings.fog = false;
        }


        if (canInvestigate || isExamining3d || lines[0] == "[JointReasoningSelect]")
        {
            if (!GameObject.FindObjectOfType<SaveLoadController>().gameObject.GetComponent<Canvas>().enabled && !GameObject.FindObjectOfType<Record>().gameObject.GetComponent<Canvas>().enabled)
            {


            }







            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (isInCE)
                {
                    if (TextId == 3 || TextId == 4 || TextId == 5 || TextId == 6)
                    {

                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (isInCE)
                {
                    if (!tw.isTyping)
                    {
                        if (TextId == 4 || TextId == 5 || TextId == 6)
                        {


                        }
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton4))
            {
                Press();
            }






        }
    }

    public void Examine3DReset()
    {
        GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
        GameObject.FindObjectOfType<MainCameraController>().TweenAngle("3dExamine", 0.5f);
        GameObject.Find("BM").transform.position = new Vector3(GameObject.Find("BM").transform.position.x, GameObject.Find("BM").transform.position.y, -1.71f);

        GameObject.Find("HN").transform.position = new Vector3(GameObject.Find("HN").transform.position.x, GameObject.Find("HN").transform.position.y, -1.71f);
        ExaminedObject.transform.DORotateQuaternion(Quaternion.identity, 0.5f);
    }

    public void TopButtonActionPresentExamine()
    {
        if (lines.Length > 0 && lines[0] == "[PointInPicture]")
        {

            GameObject selection = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("Selection"), (GameObject.Find("Picture").transform));
            selection.transform.position = GameObject.FindObjectOfType<FakeCursorController>().PointerPosition();
            if (RectTransformUtility.RectangleContainsScreenPoint(FCServices.FindChildWithName(GameObject.Find("Picture").transform.GetChild(0).gameObject, lines[1]).GetComponent<RectTransform>(), GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()))
            {
                this.LoadFromFRM(int.Parse(lines[2]));
            }
            else
            {
                this.LoadFromFRM(int.Parse(lines[3]));
            }

        }
        if (lines[0] == "[PointIn3dEvidence]")
        {
            GameObject selection = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("Selection"), (GameObject.Find("Picture").transform));
            selection.transform.position = GameObject.FindObjectOfType<FakeCursorController>().PointerPosition();


            if (FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()) != null && FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()) == FCServices.FindChildWithName(ExaminedObject.transform.GetChild(0).gameObject, lines[1]))
            {

                this.LoadFromFRM(int.Parse(lines[2]));
            }
            else
            {
                this.LoadFromFRM(int.Parse(lines[3]));
            }
        }
        if (lines[0] == "[Examine3d]" || lines[0] == "[Investigate]")
        {
            if (FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()) != null && FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).GetComponent<InvestigationObject>())
            {

                float dist = Vector3.Distance(FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).transform.position, GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position);

                if (dist <= FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).GetComponent<InvestigationObject>().examineDistance)
                {

                    canInvestigate = false;
                    isExamining3d = false;
                    GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
                    GameObject selection = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("Selection"), (GameObject.Find("Picture").transform));
                    selection.transform.position = GameObject.FindObjectOfType<FakeCursorController>().PointerPosition();
                    Invoke("DestroySelectionFX", 0.5f);
                    base.StartCoroutine(this.LoadFRMDelayed(0.5f, FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).GetComponent<InvestigationObject>().FRM));
                    MainCameraController mcc = GameObject.FindObjectOfType<MainCameraController>();
                    angleBeforeExamine.position = mcc.gameObject.transform.position;
                    angleBeforeExamine.rotation = mcc.gameObject.transform.rotation;
                    if (!FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).GetComponent<InvestigationObject>().dontRegisterExamination)
                    {
                        FCServices.GetPointTarget(GameObject.FindObjectOfType<FakeCursorController>().PointerPosition()).GetComponent<InvestigationObject>().Investigated = true;
                    }
                    if (GameObject.FindObjectOfType<MoveController>())
                    {
                        GameObject.FindObjectOfType<MoveController>().enabled = false;
                    }

                }
            }
        }
    }

    public void TextSkip()
    {
        if (PlayerPrefs.GetInt("TextSkip", 0) == 1)
        {
            tw.SkipTypewriter();

        }
    }

    public void Press()
    {
        if (!tw.isTyping && lines.Length > 24 && lines[24] == "{CrossExamination}" && lines[25] == "Statement")
        {
            previousCEStatement = CELine();
            LoadFromFRM(int.Parse(lines[26]));

        }
        if (!tw.isTyping && lines.Length > 24 && lines[24] == "{CrossExamination}" && lines[25] == "AfterPress")
        {
            if (Wit == 1 && lines[26] != "")
            {
                LoadFromFRM(int.Parse(lines[26]));
            }
            if (Wit == 2 && lines[27] != "")
            {
                LoadFromFRM(int.Parse(lines[27]));
            }
            if (Wit == 3 && lines[28] != "")
            {
                LoadFromFRM(int.Parse(lines[28]));
            }
        }
    }


    public bool isAnyMenusOpen()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Menu"))
        {
            if (go.GetComponent<Canvas>().enabled)
            {
                return true;
            }
        }
        return false;
    }


    public void PickC1()
    {

        FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>()[FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>().Length - 1].Play();
        Invoke("ActuallyLoadC1", 0.25f);
    }

    public void PickC2()
    {

        FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>()[FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>().Length - 1].Play();
        Invoke("ActuallyLoadC2", 0.25f);
    }

    public void TalkOption(int TopicId)
    {
        FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>()[FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>().Length - 1].Play();
        base.StartCoroutine(this.LoadFRMDelayed(0.25f, talkingto.Topics[TopicId].TopicFRM));
        GameObject.Find("TalkOptions").GetComponent<Canvas>().enabled = false;


    }




    public void PickC3()
    {

        FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>()[FCServices.FindChildWithName(base.gameObject, "ChoiceSE").GetComponents<AudioSource>().Length - 1].Play();
        Invoke("ActuallyLoadC3", 0.25f);
    }

    public void ActuallyLoadC1()
    {
        GameObject.FindObjectOfType<BacklogController>().AddMessage("", lines[1], Color.white, TextAnchor.UpperLeft);
        LoadFromFRM(c1);

    }
    public void ActuallyLoadC2()
    {
        GameObject.FindObjectOfType<BacklogController>().AddMessage("", lines[2], Color.white, TextAnchor.UpperLeft);
        LoadFromFRM(c2);
    }
    public void ActuallyLoadC3()
    {
        GameObject.FindObjectOfType<BacklogController>().AddMessage("", lines[3], Color.white, TextAnchor.UpperLeft);
        LoadFromFRM(c3);
    }

    public void DestroyHoldIt()
    {
        Destroy(HoldItBubble);
    }





    public void SpawnPicture()
    {
        GameObject pic = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(lines[1]), GameObject.Find("Picture").transform);
        PictureSpawned = pic;

    }

    public void DestroyPic()
    {
        Destroy(GameObject.Find("Picture").transform.GetChild(0).gameObject);
    }

    public void ShowToBeContinued()
    {
        NameBox.enabled = false;
        EmptyBox.enabled = false;
        CourtRecordBTN.gameObject.SetActive(false);
        SaveBTN.gameObject.SetActive(false);
        nametag.text = "";
        tw.gameObject.GetComponent<Text>().text = "";
        GameObject.Find("ToBeContinued").GetComponent<Canvas>().enabled = true;
    }

    public bool investigatedEverything()
    {
        foreach (InvestigationObject iobj in GameObject.FindObjectsOfType<InvestigationObject>())
        {
            if (iobj.Investigated == false && iobj.requiredToProgress)
            {
                return false;
            }
        }
        return true;
    }

    public void StopMusic()
    {
        Music.Stop();
    }


    public void PresentToPersonStart(bool playsound)
    {
        if (playsound)
        {
            base.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        }
        GameObject.Find("Interaction").GetComponent<Canvas>().enabled = false;
        GameObject.FindObjectOfType<Record>().gameObject.GetComponent<Canvas>().enabled = true;
        isPresentingToCharacter = true;
        isTalkingToCharacter = true;

    }

    public void BackFromTalking()
    {
        GameObject.Find("BackSE").GetComponent<AudioSource>().Play();
        GameObject.Find("Interaction").GetComponent<Canvas>().enabled = true;
        GameObject.Find("TalkOptions").GetComponent<Canvas>().enabled = false;
    }
    public void TalkToPersonStart(bool playsound)
    {
        if (playsound)
        {
            base.gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().Play();
        }

        GameObject.Find("Interaction").GetComponent<Canvas>().enabled = false;
        GameObject.Find("TalkOptions").GetComponent<Canvas>().enabled = true;
        FCServices.FindChildWithArray("TalkOptions", "Button", "Text").GetComponent<Text>().text = talkingto.Topics[0].TopicName;
        FCServices.FindChildWithArray("TalkOptions", "Button", "Image").SetActive(talkingto.Topics[0].talked);
        FCServices.FindChildWithArray("TalkOptions", "Button", "Image (1)").SetActive(talkingto.Topics[0].locked);




        if (talkingto.Topics.Count > 1)
        {
            FCServices.FindChildWithName("TalkOptions", "Button (1)").SetActive(true);
            FCServices.FindChildWithArray("TalkOptions", "Button (1)", "Text").GetComponent<Text>().text = talkingto.Topics[1].TopicName;
            FCServices.FindChildWithArray("TalkOptions", "Button (1)", "Image").SetActive(talkingto.Topics[1].talked);
            FCServices.FindChildWithArray("TalkOptions", "Button (1)", "Image (1)").SetActive(talkingto.Topics[1].locked);
        }
        else
        {
            FCServices.FindChildWithName("TalkOptions", "Button (1)").SetActive(false);
        }
        if (talkingto.Topics.Count > 2)
        {
            FCServices.FindChildWithName("TalkOptions", "Button (2)").SetActive(true);
            FCServices.FindChildWithArray("TalkOptions", "Button (2)", "Text").GetComponent<Text>().text = talkingto.Topics[2].TopicName;
            FCServices.FindChildWithArray("TalkOptions", "Button (2)", "Image").SetActive(talkingto.Topics[2].talked);
            FCServices.FindChildWithArray("TalkOptions", "Button (2)", "Image (1)").SetActive(talkingto.Topics[2].locked);
        }
        else
        {
            FCServices.FindChildWithName("TalkOptions", "Button (2)").SetActive(false);
        }

    }


    public IEnumerator CameraToNormal()
    {
        yield return new WaitForSeconds(6f);
        GameObject cams = GameObject.Find("Cameras");
        cams.GetComponent<Animation>().Stop();
        yield return new WaitForSeconds(0.9f);
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "MainCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().focalLength = 75f;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().farClipPlane = 20f;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().fieldOfView = 18.181f;

        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().nearClipPlane = 0.3f;

        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().focalLength = 75f;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().farClipPlane = 10f;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().fieldOfView = 18.181f;

        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().nearClipPlane = 0.3f;

        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Char"));
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Desk"));

        RenderSettings.fogDensity = 0.1f;
        GameObject.Find("Doors").GetComponent<Animator>().SetBool("isOpen", false);
        cams.GetComponent<Refresh>().DoRefresh();



        if (GameObject.Find("InSession(Clone)"))
        {
            Destroy(GameObject.Find("InSession(Clone)"));
        }
        FRMNext();

    }
    public IEnumerator CameraToNormalParameter(float time)
    {
        yield return new WaitForSeconds(time);


        GameObject cams = GameObject.Find("Cameras");

        cams.GetComponent<Animation>().Stop();
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "MainCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().enabled = true;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().focalLength = 75f;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().farClipPlane = 20f;
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().fieldOfView = 18.181f;

        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().nearClipPlane = 0.3f;

        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().focalLength = 75f;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().farClipPlane = 10f;
        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().fieldOfView = 18.181f;

        FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<Camera>().nearClipPlane = 0.3f;

        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Char"));
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Desk"));
        FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Columns");
        RenderSettings.fogDensity = 0.1f;

        cams.GetComponent<Refresh>().DoRefresh();



        FRMNext();

    }

    public InteractableCharacterController InteractableCharacterByName(string name)
    {
        foreach (InteractableCharacterController icc in GameObject.FindObjectsOfType<InteractableCharacterController>())
        {
            if (icc.characterName == name)
            {
                return icc;
            }
        }
        return null;
    }


    public void AfterSlamGavel()
    {
        FCServices.FindChildWithName("Court", "Object421").layer = LayerMask.NameToLayer("Desk");
        FCServices.FindChildWithName("Court", "Object422").layer = LayerMask.NameToLayer("Desk");
        FRMNext();
    }

    public string[] GetLinesFromFRM(int Id)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (File.Exists(Path.Combine(Application.persistentDataPath, CaseName, StoryName, Id.ToString() + ".frm")))
            {
                return File.ReadAllLines(Path.Combine(Application.persistentDataPath, CaseName, StoryName, Id.ToString() + ".frm"));

            }
            return new string[] { "" };

        }
        else
        {
            if (File.Exists(Path.Combine(Application.streamingAssetsPath, CaseName, StoryName, Id.ToString() + ".frm")))
            {
                return File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, CaseName, StoryName, Id.ToString() + ".frm"));

            }
            return new string[] { "" };
        }
    }
    public void LoadFromFRM(int Id)
    {
        if (GetLinesFromFRM(Id) != new string[] { "" })
        {
            GameObject.FindObjectOfType<BacklogController>().CommitPrevious();
            lines = GetLinesFromFRM(Id);
            CurrentFRM = Id;
            Debug.Log("Loaded " + CurrentFRM);
            GameObject.FindObjectOfType<ProceedButton>().ProceedDelayed = false;

            if (lines[0] != "[Choice]")
            {
                Choice.enabled = false;
            }
            if (lines[0].Contains("["))
            {


                if (lines[0] == "[BackToTalking]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    if (lines[1] == "Locked")
                    {
                        talkingto.Topics[int.Parse(lines[2])].locked = true;

                        if (talkingto.EvidenceFromName("Magatama") != null)
                        {
                            talkingto.EvidenceFromName("Magatama").EvidenceFRM = int.Parse(lines[3]);
                            talkingto.EvidenceFromName("Magatama").talked = false;

                        }
                        else
                        {
                            InteractableCharacterController.PresentableEvidence MagatamaEv = new InteractableCharacterController.PresentableEvidence();
                            MagatamaEv.EvidenceName = "Magatama";
                            MagatamaEv.EvidenceFRM = int.Parse(lines[3]);
                            talkingto.ToPresent.Add(MagatamaEv);
                        }

                    }
                    if (lines[1] == "Unlocked")
                    {
                        talkingto.Topics[int.Parse(lines[2])].locked = false;
                        talkingto.Topics[int.Parse(lines[2])].TopicFRM = int.Parse(lines[3]);

                        if (lines[4] == "Remove")
                        {
                            talkingto.ToPresent.Remove(talkingto.EvidenceFromName("Magatama"));
                        }
                        if (lines[4] == "Replace")
                        {
                            talkingto.EvidenceFromName("Magatama").EvidenceFRM = int.Parse(lines[5]);
                            talkingto.EvidenceFromName("Magatama").talked = false;
                        }

                    }

                    if (lines[1] == "Updated")
                    {
                        talkingto.Topics[int.Parse(lines[2])].talked = false;
                        talkingto.Topics[int.Parse(lines[2])].TopicName = lines[3];
                        talkingto.Topics[int.Parse(lines[2])].TopicFRM = int.Parse(lines[4]);
                    }

                    if (lines[1] == "Added Topic")
                    {
                        if (talkingto.Topics.Count < 3)
                        {
                            InteractableCharacterController.Topic topic = new InteractableCharacterController.Topic();
                            topic.TopicName = lines[2];
                            topic.TopicFRM = int.Parse(lines[3]);
                            talkingto.Topics.Add(topic);

                        }
                    }

                    if (lines[1] == "Added Evidence")
                    {
                        if (talkingto.Topics.Count < 3)
                        {
                            InteractableCharacterController.PresentableEvidence evidence = new InteractableCharacterController.PresentableEvidence();
                            evidence.EvidenceName = lines[2];
                            evidence.EvidenceFRM = int.Parse(lines[3]);
                            if (lines[4] == "True")
                            {
                                evidence.required = true;
                            }
                            talkingto.ToPresent.Add(evidence);


                        }
                    }



                    TalkToPersonStart(false);

                    if (lines[1] == "Unlocked" || lines[1] == "Updated")
                    {

                        if (lines[2] == "0")
                        {
                            FCServices.FindChildWithName("TalkOptions", "Button").GetComponent<Animation>().Play();
                        }
                        if (lines[2] == "1")
                        {
                            FCServices.FindChildWithName("TalkOptions", "Button (1)").GetComponent<Animation>().Play();
                        }
                        if (lines[2] == "2")
                        {
                            FCServices.FindChildWithName("TalkOptions", "Button (2)").GetComponent<Animation>().Play();
                        }
                        GameObject.Find("TalkOptions").GetComponents<AudioSource>()[3].Play();
                    }

                }

                if (lines[0] == "[BackToPresenting]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    PresentToPersonStart(false);
                }




                if (lines[0] == "[Choice]")
                {

                    if (lines[1] != "")
                    {
                        Choice1Holder.text = lines[1];
                        c1 = int.Parse(lines[4]);
                        Choice1Holder.GetComponentInParent<Image>().enabled = true;
                    }
                    else
                    {
                        Choice1Holder.text = lines[1];
                        Choice1Holder.GetComponentInParent<Image>().enabled = false;
                    }
                    if (lines[2] != "")
                    {
                        Choice2Holder.text = lines[2];
                        c2 = int.Parse(lines[5]);
                        Choice2Holder.GetComponentInParent<Image>().enabled = true;
                    }
                    else
                    {
                        Choice2Holder.text = lines[2];
                        Choice2Holder.GetComponentInParent<Image>().enabled = false;
                    }
                    if (lines[3] != "")
                    {
                        Choice3Holder.text = lines[3];
                        c3 = int.Parse(lines[6]);
                        Choice3Holder.GetComponentInParent<Image>().enabled = true;
                    }
                    else
                    {
                        Choice3Holder.text = lines[3];
                        Choice3Holder.GetComponentInParent<Image>().enabled = false;
                    }
                    Choice.enabled = true;
                }

                if (lines[0] == "[Investigate]")
                {
                    canInvestigate = true;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Save(0);
                }

                if (lines[0] == "[TalkToCharacter]")
                {
                    isTalkingToCharacter = true;
                    talkingto = this.InteractableCharacterByName(lines[1]);
                    GameObject.Find("Interaction").GetComponent<Canvas>().enabled = true;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                }


                if (lines[0] == "[Examine3d]")
                {
                    if (FCServices.GetComponentFromGameObject<SecondaryCameraController>("BGCamera").WithName("3dExamine").position != GameObject.Find("BGCamera").transform.position)
                    {
                        GameObject.FindObjectOfType<FakeCursorController>().CursorStart();
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("3dExamine");
                    }

                    isExamining3d = true;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    FCServices.FindChildWithName("PresentExamine", "Text").GetComponent<Text>().text = "";
                    Save(0);
                }


                if (lines[0] == "[LoadScene]")
                {

                    if (lines[1] == "+1")
                    {
                        int toloadnew = Array.IndexOf(AssetBundleController.GetBundleFromName(CaseName.ToLower()).GetAllScenePaths(), UnityEngine.SceneManagement.SceneManager.GetActiveScene().path) + 1;
                        UnityEngine.SceneManagement.SceneManager.LoadScene(AssetBundleController.GetBundleFromName(CaseName.ToLower()).GetAllScenePaths()[toloadnew]);
                    }
                    else
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene(AssetBundleController.GetBundleFromName(CaseName.ToLower()).GetAllScenePaths()[int.Parse(lines[1])]);
                    }


                }
                if (lines[0] == "[LoadSceneTitle]")
                {
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                }
                if (lines[0] == "[FadeOut]")
                {
                    Fader.SetTrigger("Out");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                }
                if (lines[0] == "[FadeOut2]")
                {
                    FCServices.FindChildWithName(GameObject.Find("FadeEffect2"), "Panel").GetComponent<Animator>().SetTrigger("Out");
                    Music.DOFade(0f, 75f / 60f);
                }
                if (lines[0] == "[FadeOutWhite]")
                {
                    FCServices.FindChildWithName(GameObject.Find("FadeEffectWhite"), "Panel").GetComponent<Animator>().SetTrigger("Out");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                }

                if (lines[0] == "[ToBeContinued]")
                {
                    FCServices.FindChildWithName(GameObject.Find("FadeEffect2"), "Panel").GetComponent<Animator>().SetTrigger("Cross");
                    Invoke("ShowToBeContinued", 75f / 60f);
                }

                if (lines[0] == "[DepthOfFieldOn]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<PostProcessingController>().DepthOfFieldOn();
                    Invoke("FRMNext", 1f);
                }

                if (lines[0] == "[FadeIn]")
                {
                    Fader.SetTrigger("In");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle(lines[1]);
                }
                if (lines[0] == "[FadeInWhite]")
                {
                    FCServices.FindChildWithName(GameObject.Find("FadeEffectWhite"), "Panel").GetComponent<Animator>().SetTrigger("In");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle(lines[1]);
                }
                if (lines[0] == "[FadeCross]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("Cross");
                }
                if (lines[0] == "[FadeCrossWhite]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    FCServices.FindChildWithName(GameObject.Find("FadeEffectWhite"), "Panel").GetComponent<Animator>().SetTrigger("Cross");
                }
                if (lines[0] == "[FadeCrossFast]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("CrossFast");
                }
                if (lines[0] == "[FadeCrossFastAfterDebate]")
                {

                    Fader.SetTrigger("CrossFast");
                    Invoke("HideDebateUI", 0.625f);


                }
                if (lines[0] == "[FadeCrossFastAfterHG]")
                {

                    Fader.SetTrigger("CrossFast");
                    Invoke("HideHGUI", 0.625f);


                }


                if (lines[0] == "[ToNextCEStatement]")
                {
                    Fader.SetTrigger("In");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle(this.GetLinesFromFRM(NextCELine())[0]);
                    lines[5] = NextCELine().ToString();
                }

                if (lines[0] == "[ToCEStatement]")
                {
                    Fader.SetTrigger("In");
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle(this.GetLinesFromFRM(int.Parse(CrossExaminationData[11 + int.Parse(lines[1])]))[0]);
                    lines[5] = int.Parse(CrossExaminationData[11 + int.Parse(lines[1])]).ToString();
                }


                if (lines[0] == "[CameraTween]")
                {

                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    GameObject.FindObjectOfType<MainCameraController>().TweenAngle(lines[1], float.Parse(lines[2]));
                    if (lines[3] != "NoProceed")
                    {
                        Invoke("FRMNext", float.Parse(lines[2]));
                    }
                }

                if (lines[0] == "[AfterExamineFreeRoam]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.DOLocalMove(Vector3.zero, float.Parse(lines[1]));
                    GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.DOLocalRotateQuaternion(Quaternion.identity, float.Parse(lines[1]));
                    base.StartCoroutine(this.LoadFRMDelayed(float.Parse(lines[1]), int.Parse(lines[2])));
                }

                if (lines[0] == "[AfterExamine]")
                {
                    if (investigatedEverything() && !GameObject.FindObjectOfType<ExitRoomObject>().canLeave)
                    {
                        NameBox.enabled = false;
                        EmptyBox.enabled = false;
                        nametag.text = "";
                        tw.gameObject.GetComponent<Text>().text = "";
                        MainCameraController.CameraAngle originalAngle = GameObject.FindObjectOfType<MainCameraController>().AvailableAngles[GameObject.FindObjectOfType<Investigation>().InvestigateAngleId];

                        GameObject.FindObjectOfType<MainCameraController>().TweenAngle(originalAngle, float.Parse(lines[1]));
                        base.StartCoroutine(this.LoadFRMDelayed(float.Parse(lines[1]), int.Parse(lines[3])));
                    }
                    else
                    {
                        NameBox.enabled = false;
                        EmptyBox.enabled = false;
                        nametag.text = "";
                        tw.gameObject.GetComponent<Text>().text = "";
                        if (angleBeforeExamine.position != Vector3.zero && angleBeforeExamine.rotation != Quaternion.identity)
                        {
                            GameObject.FindObjectOfType<MainCameraController>().TweenAngle(angleBeforeExamine, float.Parse(lines[1]));
                        }
                        else
                        {
                            MainCameraController.CameraAngle originalAngle = GameObject.FindObjectOfType<MainCameraController>().AvailableAngles[GameObject.FindObjectOfType<Investigation>().InvestigateAngleId];

                            GameObject.FindObjectOfType<MainCameraController>().TweenAngle(originalAngle, float.Parse(lines[1]));
                        }

                        base.StartCoroutine(this.LoadFRMDelayed(float.Parse(lines[1]), int.Parse(lines[2])));
                    }
                }

                if (lines[0] == "[Testimony]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    Fader.SetTrigger("CrossNoProceed");

                    Action selectwitnesangle = () =>
                    {
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("WitnessBeforeTestimony");
                    };
                    base.StartCoroutine(FCServices.DelayedAction(selectwitnesangle, 75f / 60f));


                    Action doteststart = () =>
                    {



                        GameObject.FindObjectOfType<MainCameraController>().TweenAngle("Witness", 2.5f);
                        Instantiate(Testimony, GameObject.Find("CETransformParent").transform);

                        Invoke("FRMNext", 2.5f);

                        CESE.gameObject.GetComponents<AudioSource>()[1].Play();
                    };

                    base.StartCoroutine(FCServices.DelayedAction(doteststart, 150f / 60f));

                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                }




                if (lines[0] == "[TestimonyEnd]")
                {
                    Music.GetComponent<AudioSource>().DOFade(0f, 150f / 60f);
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    Fader.SetTrigger("Out");
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                }
                if (lines[0] == "[CharacterFadeIn]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    this.FindCharacter(lines[1]).gameObject.GetComponent<Animator>().SetTrigger("FadeIn");


                }
                if (lines[0] == "[CharacterFadeOut]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    this.FindCharacter(lines[1]).gameObject.GetComponent<Animator>().SetTrigger("FadeOut");


                }
                if (lines[0] == "[PrePsycheLocks]")
                {
                    Music.Stop();
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.Find("BG").GetComponent<Animator>().SetTrigger("PsycheLock");
                }
                if (lines[0] == "[PsycheLocks]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    CurrentPsycheLocks = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(lines[1]));
                    CurrentPsycheLocks.name = lines[1];


                }
                if (lines[0] == "[PsycheLocksExit]")
                {
                    Music.Stop();
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("Cross");
                    Invoke("PsycheLocksExit", 75f / 60f);
                }



                if (lines[0] == "[PsycheLockBreak]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<PsycheLockController>().BreakLock();
                }

                if (lines[0] == "[PsycheLocksUnlock]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.FindObjectOfType<PsycheLockController>().Unlock();
                }

                if (lines[0] == "[UnlockSuccessful]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Instantiate(PrefabController.Instance().FindPrefab("UnlockSuccessful"), GameObject.Find("unlocksuccesful").transform);
                }
                if (lines[0] == "[AfterUnlock]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    GameObject.Find("BG").GetComponent<Animator>().SetTrigger("PsycheLockDestroy");
                    Invoke("FRMNext", 1f);
                }



                if (lines[0] == "[ShowPicture]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("CrossFast");
                    Invoke("SpawnPicture", 75f / 120f);



                }
                if (lines[0] == "[PointInPicture]")
                {
                    GameObject.FindObjectOfType<FakeCursorController>().CursorStart();
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    FCServices.FindChildWithName("PresentExamine", "Text").GetComponent<Text>().text = lines[4];

                }
                if (lines[0] == "[PointIn3dEvidence]")
                {
                    if (FCServices.GetComponentFromGameObject<SecondaryCameraController>("BGCamera").WithName("3dExamine").position != GameObject.Find("BGCamera").transform.position)
                    {
                        GameObject.FindObjectOfType<FakeCursorController>().CursorStart();
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("3dExamine");
                    }

                    isExamining3d = true;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    FCServices.FindChildWithName("PresentExamine", "Text").GetComponent<Text>().text = "";
                }


                if (lines[0] == "[HidePicture]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("CrossFast");
                    Invoke("DestroyPic", 75f / 120f);

                }




                if (lines[0] == "[AfterTestimonyEnd]")
                {

                    Fader.SetTrigger("In");
                    Destroy(this.TestimonyMessage);
                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle(lines[1]);
                }

                if (lines[0] == "[GavelSlam]")
                {

                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle("Gavel");
                    FCServices.FindChildWithName("Court", "Object421").layer = LayerMask.NameToLayer("Default");
                    FCServices.FindChildWithName("Court", "Object422").layer = LayerMask.NameToLayer("Default");
                    GameObject.FindObjectOfType<JudgeController>().gameObject.GetComponent<Animator>().Play("GavelSlam");
                    GameObject.FindObjectOfType<JudgeController>().gameObject.GetComponent<Animator>().SetBool("isSlammingGavel", true);
                    Invoke("AfterSlamGavel", 2f);
                }
                if (lines[0] == "[GavelSlam3]")
                {

                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle("Gavel");
                    FCServices.FindChildWithName("Court", "Object421").layer = LayerMask.NameToLayer("Default");
                    FCServices.FindChildWithName("Court", "Object422").layer = LayerMask.NameToLayer("Default");
                    GameObject.FindObjectOfType<JudgeController>().gameObject.GetComponent<Animator>().Play("3Gavel");
                    GameObject.FindObjectOfType<JudgeController>().gameObject.GetComponent<Animator>().SetBool("isSlammingGavel3", true);
                    Invoke("AfterSlamGavel", 2f - (0.22f / 30f) + (40f / 30f));
                }
                if (lines[0] == "[AllRise]")
                {
                    GameObject.FindObjectOfType<PostProcessingController>().BloomInSession();
                    RenderSettings.fogDensity = 0.025f;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    Fader.SetTrigger("InNoProceed");
                    Instantiate(PrefabController.Instance().FindPrefab("InSession"));
                    GameObject.Find("CourtDoors").GetComponent<Animator>().SetBool("isOpen", true);
                    GameObject.Find("Cameras").GetComponent<Animation>().Play();
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Char");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Desk");

                    if (lines[1] == "1")
                    {
                        FCServices.FindChildWithName("InSession(Clone)", "1").SetActive(true);
                        FCServices.FindChildWithName("InSession(Clone)", "2").SetActive(false);
                        FCServices.FindChildWithName("InSession(Clone)", "3").SetActive(false);
                    }
                    if (lines[1] == "2")
                    {
                        FCServices.FindChildWithName("InSession(Clone)", "1").SetActive(false);
                        FCServices.FindChildWithName("InSession(Clone)", "2").SetActive(true);
                        FCServices.FindChildWithName("InSession(Clone)", "3").SetActive(false);
                    }
                    if (lines[1] == "3")
                    {
                        FCServices.FindChildWithName("InSession(Clone)", "1").SetActive(false);
                        FCServices.FindChildWithName("InSession(Clone)", "2").SetActive(false);
                        FCServices.FindChildWithName("InSession(Clone)", "3").SetActive(true);
                    }
                    base.StartCoroutine(this.CameraToNormal());


                }

                if (lines[0] == "[Verdict]")
                {
                    GameObject.FindObjectOfType<MainCameraController>().SelectAngle("Judge");
                    if (lines[1] == "Not Guilty")
                    {
                        NameBox.enabled = false;
                        EmptyBox.enabled = false;
                        nametag.text = "";
                        tw.gameObject.GetComponent<Text>().text = "";
                        Instantiate(PrefabController.Instance().FindPrefab("NotGuilty"), GameObject.Find("CETransformParent").transform);


                    }
                    if (lines[1] == "Guilty")
                    {
                        NameBox.enabled = false;
                        EmptyBox.enabled = false;
                        nametag.text = "";
                        tw.gameObject.GetComponent<Text>().text = "";
                        Instantiate(PrefabController.Instance().FindPrefab("Guilty"), GameObject.Find("CETransformParent").transform);


                    }
                }



                if (lines[0] == "[CrossExaminationStart]")
                {
                    CrossExaminationData = lines;
                    CEpressed = new List<bool>();
                    foreach (Transform child in GameObject.Find("StatementDots").transform)
                    {
                        Destroy(child.gameObject);
                    }
                    for (int i = 0; i < CrossExaminationData.Length - 11; i++)
                    {
                        CEpressed.Add(false);

                        Instantiate(PrefabController.Instance().FindPrefab("StatementDot"), GameObject.Find("StatementDots").transform);

                    }



                    NameBox.enabled = false;
                    EmptyBox.enabled = false;






                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";

                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    Fader.SetTrigger("CrossNoProceed");

                    Action selectwitnesangle = () =>
                    {
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("Witness");
                    };
                    base.StartCoroutine(FCServices.DelayedAction(selectwitnesangle, 75f / 60f));


                    Action doteststart = () =>
                    {



                        Instantiate(CrossExaminationPrefab, GameObject.Find("CETransformParent").transform);

                        Invoke("FRMNext", 2.5f);

                        CESE.gameObject.GetComponents<AudioSource>()[0].Play();
                    };

                    base.StartCoroutine(FCServices.DelayedAction(doteststart, 150f / 60f));

                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                }
                if (lines[0] == "[EvidenceEnter]")
                {
                    GameObject.FindObjectOfType<Record>().gameObject.GetComponent<Canvas>().enabled = true;

                    foreach (Transform child in this.EvidenceSlider.transform)
                    {
                        if (child.gameObject.name != lines[1])
                        {
                            child.gameObject.SetActive(false);
                        }
                        else
                        {
                            if (lines[3] == "Update")
                            {
                                GameObject.FindObjectOfType<Record>().SelectEvidence(GameObject.FindObjectOfType<Record>().EvidenceIdByName(lines[1]));
                                base.StartCoroutine(this.UpdateEvidenceDescription(child));
                            }
                            child.gameObject.SetActive(true);
                        }
                    }
                    if (FCServices.FindChildWithArray(EvidenceSlider, lines[1]))
                    {
                        Debug.Log("yes");
                    }

                    this.EvidenceSlider.GetComponent<AudioSource>().Play();

                    nametag.text = "";
                    NameBox.enabled = false;
                    EmptyBox.enabled = true;
                    tw.gameObject.GetComponent<Text>().color = Blue;
                    tw.silent = false;
                    tw.src = RegularBlip;

                    if (lines[3] == "Add")
                    {
                        this.EvidenceSlider.GetComponent<Animator>().SetTrigger("Enter");
                        Record record = GameObject.FindObjectOfType<Record>();
                        if (lines[4] == "")
                        {
                            record.AddEvidence(lines[1], lines[1] + "Button");
                        }
                        else
                        {
                            record.AddEvidence(lines[1], lines[1] + "Button", lines[4]);
                        }

                        record.SelectEvidence(record.EvidenceList.Count);


                    }
                    if (lines[3] == "Update")
                    {

                        FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "UpdateEffect").GetComponent<Animation>().Play();

                        GameObject record = GameObject.FindObjectOfType<Record>().gameObject;





                    }
                    tw.Type(lines[2]);
                    FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "EvPrSwitch").SetActive(false);


                }
                if (lines[0] == "[EvidenceExit]")
                {
                    GameObject.FindObjectOfType<Record>().Close();
                    Invoke("FRMNext", 0.5f);

                }

                if (lines[0] == "[EvidenceExitPresent]")
                {
                    GameObject.FindObjectOfType<Record>().Close(true);
                    GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
                    Invoke("FRMNext", 0.5f);

                }




                if (lines[0] == "[Present]")
                {
                    GameObject.FindObjectOfType<Record>().isPresenting = true;
                    GameObject.FindObjectOfType<Record>().gameObject.GetComponent<Canvas>().enabled = true;

                    if (lines[5] == "Profiles")
                    {
                        GameObject.FindObjectOfType<Record>().Switch();
                    }
                }

                if (lines[0] == "[BGFade]")
                {
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";
                    if (lines[1] == "In")
                    {
                        GameObject.Find("Darkness").GetComponent<Animator>().SetBool("isBGVisible", true);
                    }
                    if (lines[1] == "Out")
                    {
                        GameObject.Find("Darkness").GetComponent<Animator>().SetBool("isBGVisible", false);
                    }
                    Invoke("FRMNext", 1.25f);

                }



                if (lines[0] == "[Interjection]")
                {
                    if (GameObject.Find("GallerySE") && GameObject.Find("GallerySE").GetComponent<AudioSource>().isPlaying)
                    {
                        GameObject.Find("GallerySE").GetComponent<AudioSource>().Stop();
                    }

                    if (Gamepad.current != null)
                    {
                        RumbleController(30f);

                    }


                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    if (lines[1] == "HoldIt")
                    {
                        if (lines[2] == "Wright")
                        {
                            Instantiate(WrightHoldItPrefab);
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("Phoenix", "Hold It!", Color.white, TextAnchor.UpperLeft);
                        }
                        if (lines[2] == "NonCharacterSpecific")
                        {
                            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("HoldItNonSpecific"));
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("", "Hold It!", Color.white, TextAnchor.UpperLeft);
                        }

                    }
                    if (lines[1] == "TakeThat")
                    {
                        if (lines[2] == "Wright")
                        {
                            Instantiate(WrightTakeThatPrefab);
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("Phoenix", "Take That!", Color.white, TextAnchor.UpperLeft);
                        }
                        if (lines[2] == "NonCharacterSpecific")
                        {
                            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("TakeThatNonSpecific"));
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("", "Take That!", Color.white, TextAnchor.UpperLeft);
                        }


                    }
                    if (lines[1] == "GotIt")
                    {
                        if (lines[2] == "Wright")
                        {
                            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("GotItWright"));
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("Phoenix", "Got It!", Color.white, TextAnchor.UpperLeft);
                        }
                        if (lines[2] == "NonCharacterSpecific")
                        {
                            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("GotItNonSpecific"));
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("", "Got It!", Color.white, TextAnchor.UpperLeft);
                        }

                    }
                    if (lines[1] == "Objection")
                    {
                        if (lines[2] == "Wright")
                        {
                            Instantiate(WrightObjectionPrefab);
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("Phoenix", "Objection!", Color.white, TextAnchor.UpperLeft);
                        }

                        if (lines[2] == "Edgeworth")
                        {
                            Instantiate(EdgeworthObjectionPrefab);
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("Edgeworth", "Objection!", Color.white, TextAnchor.UpperLeft);
                        }
                        if (lines[2] == "NonCharacterSpecific")
                        {
                            Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab("ObjectionNonSpecific"));
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("", "Objection!", Color.white, TextAnchor.UpperLeft);
                        }
                    }
                    if (lines[1] == "Custom")
                    {
                        Instantiate(PrefabController.Instance().FindPrefab(lines[2]));
                        if (lines[2].Split(char.Parse(";")).Length > 1) {
                            GameObject.FindObjectOfType<BacklogController>().AddMessage(lines[2].Split(char.Parse(";"))[0], lines[2].Split(char.Parse(";"))[1], Color.white, TextAnchor.UpperLeft);
                        }
                        else
                        {
                            GameObject.FindObjectOfType<BacklogController>().AddMessage("", lines[2], Color.white, TextAnchor.UpperLeft);
                        }
                    }
                    if (lines[3] == "True")
                    {

                        Music.Stop();



                    }




                }

                if (lines.Length > 10)
                {
                    if (GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]) != null && GameObject.FindObjectOfType<ModifyStrings>().lines.Length > 10 && GameObject.FindObjectOfType<ModifyStrings>().lines[9] != "")
                    {


                        GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]);
                        if (!GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().isPlaying)
                        {
                            GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().Play();
                        }


                        Instantiate(PrefabController.Instance().FindPrefab("Flash"));

                        if (GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "ayashii" && !GameObject.FindObjectOfType<ModifyStrings>().lines[10].Contains("diminish") && GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "flashback")
                        {
                            GameObject.FindObjectOfType<ModifyStrings>().RumbleController(15);
                            GameObject.FindObjectOfType<ModifyStrings>().ShakeCamera();
                        }





                    }
                }
                if (lines.Length > 24 && lines[24] == "{DeleteReaction}")
                {
                    Destroy(CurrentReaction);
                }
            }
            else
            {
                Choice.enabled = false;




                GameObject.FindObjectOfType<MainCameraController>().SelectAngle(lines[0]);
                if (FindCharacter(lines[1]))
                {
                    if (lines[2] == "Idle")
                    {
                        FCServices.AllAnimatorBoolsToFalse(FindCharacter(lines[1]).gameObject.GetComponent<Animator>());
                    }
                    else
                    {
                        FCServices.AllAnimatorBoolsToFalse(FindCharacter(lines[1]).gameObject.GetComponent<Animator>());
                        FindCharacter(lines[1]).gameObject.GetComponent<Animator>().SetBool("is" + lines[2], true);
                    }

                    if (FindCharacter(lines[1]).gameObject.GetComponent<WitnessController>())
                    {
                        Wit = FindCharacter(lines[1]).gameObject.GetComponent<WitnessController>().WitnessId;
                    }
                    else
                    {
                        Wit = 0;
                    }
                }


                nametag.enabled = true;
                GameObject.Find("Unknown").GetComponent<Text>().enabled = false;
                if (lines[1] == "")
                {

                    if (lines[4] == "")
                    {
                        EmptyBox.enabled = false;
                        NameBox.enabled = false;
                        nametag.text = "";
                    }
                    else
                    {
                        NameBox.enabled = false;
                        EmptyBox.enabled = true;
                        nametag.text = lines[1];
                    }
                }
                else
                {
                    if (lines[4] == "")
                    {
                        EmptyBox.enabled = false;
                        NameBox.enabled = false;
                        nametag.text = "";
                    }
                    else
                    {
                        NameBox.enabled = true;
                        EmptyBox.enabled = false;
                        nametag.text = lines[1];
                    }

                }
                if (lines[3] == "White")
                {
                    tw.gameObject.GetComponent<Text>().color = Color.white;
                }
                if (lines[3] == "Blue")
                {
                    tw.gameObject.GetComponent<Text>().color = Blue;
                }
                if (lines[3] == "Green")
                {
                    tw.gameObject.GetComponent<Text>().color = Green;
                }
                if (lines[3] == "DarkerGreen")
                {
                    tw.gameObject.GetComponent<Text>().color = DarkerGreen;
                }
                if (lines[3] == "Orange")
                {
                    tw.gameObject.GetComponent<Text>().color = Orange;
                }






                if (lines[6] == "Male")
                {
                    tw.silent = false;
                    tw.src = M;
                }
                if (lines[6] == "Female")
                {
                    tw.silent = false;
                    tw.src = F;
                }
                if (lines[6] == "Regular")
                {
                    tw.silent = false;
                    tw.src = RegularBlip;
                }
                if (lines[6] == "Silent")
                {
                    tw.silent = true;

                }
                if (lines[8] == "Left")
                {
                    tw.gameObject.GetComponent<Text>().alignment = TextAnchor.UpperLeft;
                }
                if (lines[8] == "Center")
                {
                    tw.gameObject.GetComponent<Text>().alignment = TextAnchor.UpperCenter;
                }

                if (lines[4] != "")
                {
                    if (lines.Length > 25 && lines[25] == "UnknownSpeaker")
                    {
                        GameObject.FindObjectOfType<BacklogController>().AddMessage("???", lines[4], tw.gameObject.GetComponent<Text>().color, tw.gameObject.GetComponent<Text>().alignment);
                    }
                    else
                    {
                        GameObject.FindObjectOfType<BacklogController>().AddMessage(nametag.text, lines[4], tw.gameObject.GetComponent<Text>().color, tw.gameObject.GetComponent<Text>().alignment);
                    }
                }



                if (lines.Length > 24)
                {
                    if (lines[24] == "{CrossExamination}")
                    {
                        this.isInCE = true;
                        foreach (Transform child in GameObject.Find("StatementDots").transform)
                        {
                            if (child.GetSiblingIndex() == CELine() - 11)
                            {
                                child.gameObject.GetComponent<Animator>().SetBool("isActive", true);

                            }
                            else
                            {
                                child.gameObject.GetComponent<Animator>().SetBool("isActive", false);
                            }

                        }

                    }
                    if (lines[24] == "{Testimony}")
                    {
                        if (this.TestimonyMessage == null)
                        {

                            this.TestimonyMessage = Instantiate(TMPrefab);



                        }
                    }








                }
                if (lines[4] != "")
                {
                    tw.Type(lines[4]);
                }
                else
                {
                    if (GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]) != null && GameObject.FindObjectOfType<ModifyStrings>().lines[0] != "[CrossExaminationStart]" && GameObject.FindObjectOfType<ModifyStrings>().lines.Length > 9 && GameObject.FindObjectOfType<ModifyStrings>().lines[9] != "")
                    {


                        GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]);
                        if (!GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().isPlaying)
                        {
                            GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().Play();
                        }


                        Instantiate(PrefabController.Instance().FindPrefab("Flash"));

                        if (GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "ayashii" && !GameObject.FindObjectOfType<ModifyStrings>().lines[10].Contains("diminish") && GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "flashback")
                        {
                            GameObject.FindObjectOfType<ModifyStrings>().RumbleController(15);
                            GameObject.FindObjectOfType<ModifyStrings>().ShakeCamera();
                        }





                    }
                }
            }
            if (lines.Length > 7 && lines[7] != "")
            {
                if (lines[7] == "NoMusic")
                {
                    Music.Stop();
                }
                else
                {

                    if (lines[7].Contains("Fade"))
                    {
                        if (lines[7] == "FadeOut")
                        {
                            Music.DOFade(0f, 150f / 60f);
                            Debug.Log("faded out");
                            Music.gameObject.GetComponent<MusicController>().DelayedStopMusic(150f / 60f);
                        }
                        if (lines[7] == "FadeIn")
                        {
                            Music.Play();
                            Music.DOFade(PlayerPrefs.GetFloat("BGMVolume", 0.5f), 150f / 60f);
                            Debug.Log("faded in");
                        }
                    }


                    else


                    {
                        if (Music.clip != Music.GetComponent<MusicController>().FindTrack(lines[7]))
                        {
                            Music.clip = Music.GetComponent<MusicController>().FindTrack(lines[7]);
                            Music.Stop();
                            Music.Play();
                        }


                        if (!Music.isPlaying && Music.clip == Music.GetComponent<MusicController>().FindTrack(lines[7]))
                        {
                            if (Music.volume == 0)
                            {
                                Music.volume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
                            }
                            Music.Play();
                        }


                    }
                }
            }
            if (lines.Length > 25)
            {
                if (lines[25] == "PenaltyEnter" || isInCE && CELine() != -1)
                {
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Enter");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Exit");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Hide");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().SetTrigger("Enter");
                }
                if (lines[25] == "PenaltyExit")
                {

                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Enter");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Exit");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Hide");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().SetTrigger("Exit");
                    GameObject.FindObjectOfType<PenaltyController>().HidePendingPenalty();
                }
                if (lines[25] == "PenaltyHide")
                {

                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Enter");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Exit");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().ResetTrigger("Hide");
                    GameObject.FindObjectOfType<PenaltyController>().gameObject.GetComponent<Animator>().SetTrigger("Hide");
                    GameObject.FindObjectOfType<PenaltyController>().HidePendingPenalty();
                }

                if (lines[25] == "ShowPendingPenalty")
                {
                    GameObject.FindObjectOfType<PenaltyController>().ShowPendingPenalty(int.Parse(lines[26]));
                }
                if (lines[25] == "TakePenalty")
                {
                    GameObject.FindObjectOfType<PenaltyController>().TakePenalty();
                }



                if (lines[25] == "AddStatementCE")
                {
                    this.AddLineTOCEData(lines[26]);
                    CEpressed.Add(false);
                    Instantiate(PrefabController.Instance().FindPrefab("StatementDot"), GameObject.Find("StatementDots").transform);
                }

                if (lines[25] == "ReplaceStatementCE")
                {
                    CrossExaminationData[int.Parse(lines[26])] = lines[27];
                    CEpressed[int.Parse(lines[26]) - 11] = false;
                }

                if (lines[25] == "FinalPointWright")
                {
                    GameObject.FindObjectOfType<MainCameraController>().TweenAngle("DefensePoint", 1f, false);
                    Invoke("FRMNext", 1f);

                }

                if (lines[25] == "ProceedDelayed")
                {
                    Invoke("FRMNext", float.Parse(lines[26]));
                }

                if (lines[25] == "UnknownSpeaker")
                {
                    nametag.enabled = false;
                    GameObject.Find("Unknown").GetComponent<Text>().enabled = true;
                }

                if (lines[25] == "CloseUp")
                {
                    if (lines[0] == "Wright")
                    {
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("DefenseCloseUp");
                    }
                    if (lines[0] == "Edgeworth")
                    {
                        GameObject.FindObjectOfType<MainCameraController>().SelectAngle("ProsecutionCloseUp");
                    }

                }


                if (lines[25] == "ProceedOnAnimationEndCRT")
                {

                    Animator animator = FindCharacter(lines[1]).gameObject.GetComponent<Animator>();
                    base.StartCoroutine(this.CorrectAnimDelay(animator));


                }



                if (lines[25] == "CEStateChange")
                {
                    CEpressed[int.Parse(lines[26])] = true;
                    if (areAllStatementsPressed())
                    {

                        lines[5] = lines[27];
                    }

                }

                if (lines[25] == "FlashEffect")
                {
                    Instantiate(PrefabController.Instance().FindPrefab("Flash"));
                }

                if (lines[25] == "FixCamera")
                {
                    base.StartCoroutine(this.CameraToNormalParameter(0f));
                }

                if (lines[25] == "AddProfile")
                {
                    for (int i = 26; i < lines.Length; i++)
                    {
                        GameObject.FindObjectOfType<Record>().AddProfile(lines[i], lines[i] + "Button");
                    }
                }
                if (lines[25] == "UpdateEvidenceSilent")
                {
                    for (int i = 26; i < lines.Length; i+=2)
                    {
                        foreach (Transform child in this.EvidenceSlider.transform)
                        {
                            if (child.gameObject.name != lines[i])
                            {
                               
                            }
                            else
                            {
                                
                                    GameObject.FindObjectOfType<Record>().SelectEvidence(GameObject.FindObjectOfType<Record>().EvidenceIdByName(lines[i]));
                                    base.StartCoroutine(this.UpdateEvidenceDescription(child, lines[i+1]));
                                
                               
                            }
                        }
                    }
                }


                if (lines[25] == "3dExaminedObjectChange")
                {
                    foreach (Transform child in ExaminedObject.transform)
                    {
                        if (child.name == lines[26])
                        {
                            child.SetAsFirstSibling();
                            child.gameObject.SetActive(true);
                        }
                        else
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }

                if (lines[25] == "Set3dExaminedObjectAnimatorTrigger")
                {
                    ExaminedObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger(lines[26]);

                    if (lines[27] == "")
                    {
                        base.StartCoroutine(this.CorrectAnimDelay(ExaminedObject.transform.GetChild(0).gameObject.GetComponent<Animator>()));
                    }
                    else
                    {
                        if (lines[27] == "False")
                        {

                        }
                        else
                        {
                            Invoke("FRMNext", float.Parse(lines[27]));
                        }
                    }
                }



                if (lines[25] == "CourtZoomedOut")
                {
                    GameObject.Find("Cameras").GetComponent<Animation>().Play("ZoomOutCRTNew");
                    RenderSettings.fogDensity = 0.05f;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Char");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Desk");

                    if (lines[26] == "") { }
                    else
                    {
                        if (lines[26] == "False")
                        {

                        }
                        else
                        {
                            base.StartCoroutine(this.CameraToNormalParameter(float.Parse(lines[26])));

                        }
                    }
                    if (!GameObject.Find("GallerySE").GetComponent<AudioSource>().isPlaying && lines[27] == "True")
                    {
                        GameObject.Find("GallerySE").GetComponent<AudioSource>().Play();
                    }

                }
                if (lines[25] == "CourtZoomOut")
                {
                    GameObject.Find("Cameras").GetComponent<Animation>().Play("CourtZoomAnim");
                    RenderSettings.fogDensity = 0.05f;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Char");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Desk");
                    if (lines[26] == "") { base.StartCoroutine(this.CameraToNormalParameter(2.66f)); }
                    else
                    {
                        if (lines[26] == "False")
                        {

                        }
                        else
                        {
                            base.StartCoroutine(this.CameraToNormalParameter(float.Parse(lines[26])));

                        }
                    }

                    if (!GameObject.Find("GallerySE").GetComponent<AudioSource>().isPlaying)
                    {
                        GameObject.Find("GallerySE").GetComponent<AudioSource>().Play();
                    }

                }
                if (lines[25] == "CourtRotate1")
                {
                    GameObject.Find("Cameras").GetComponent<Animation>().Play("CRTRotate");
                    RenderSettings.fogDensity = 0.05f;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Char");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Desk");

                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Columns"));
                    if (lines[26] == "") { base.StartCoroutine(this.CameraToNormalParameter(4f)); }
                    else
                    {
                        if (lines[26] == "False")
                        {

                        }
                        else
                        {
                            base.StartCoroutine(this.CameraToNormalParameter(float.Parse(lines[26])));

                        }
                    }
                    if (!GameObject.Find("GallerySE").GetComponent<AudioSource>().isPlaying)
                    {
                        GameObject.Find("GallerySE").GetComponent<AudioSource>().Play();
                    }

                }
                if (lines[25] == "CourtRotate2")
                {
                    GameObject.Find("Cameras").GetComponent<Animation>().Play("CRTRotate2");
                    RenderSettings.fogDensity = 0.05f;
                    NameBox.enabled = false;
                    EmptyBox.enabled = false;
                    nametag.text = "";
                    tw.gameObject.GetComponent<Text>().text = "";


                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Char");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("Desk");
                    FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("Columns"));
                    if (lines[26] == "") { base.StartCoroutine(this.CameraToNormalParameter(4f)); }
                    else
                    {
                        if (lines[26] == "False")
                        {

                        }
                        else
                        {
                            base.StartCoroutine(this.CameraToNormalParameter(float.Parse(lines[26])));

                        }
                    }
                    if (!GameObject.Find("GallerySE").GetComponent<AudioSource>().isPlaying)
                    {
                        GameObject.Find("GallerySE").GetComponent<AudioSource>().Play();
                    }

                }


                if (lines[25] == "CanLeaveTrue")
                {
                    GameObject.FindObjectOfType<ExitRoomObject>().canLeave = true;
                }

                if (lines[25] == "PopUpEvidence")
                {
                    if (EvidencePoppedUp != null)
                    {
                        Destroy(EvidencePoppedUp);
                    }
                    EvidencePoppedUp = Instantiate(PrefabController.Instance().FindPrefab(lines[26]));
                }

                if (lines[25] == "EvidenceHide")
                {

                    EvidencePoppedUp.GetComponentInChildren<Animator>().SetTrigger("leave");

                }
                if (lines[25] == "EvidenceHideImmediate")
                {
                    EvidencePoppedUp.GetComponentInChildren<Animator>().SetTrigger("leaveimmediate");

                }

                if (lines[25] == "TalkStateChanged")
                {
                    talkingto.Topics[int.Parse(lines[26])].talked = true;
                    if (talkingto.actuallyInvestigated())
                    {
                        lines[5] = lines[27];
                    }


                }

                if (lines[25] == "PresentEvidenceStateChanged")
                {
                    talkingto.EvidenceFromName(lines[26]).talked = true;
                    if (talkingto.actuallyInvestigated())
                    {
                        lines[5] = lines[27];
                    }


                }

                if (lines[25] == "FadeOutDelayed")
                {
                    Action fadeout = () =>
                    {
                        Fader.SetTrigger("Out");
                        NameBox.enabled = false;
                        EmptyBox.enabled = false;
                        nametag.text = "";
                        tw.gameObject.GetComponent<Text>().text = "";
                    };

                    base.StartCoroutine(FCServices.DelayedAction(fadeout, float.Parse(lines[26])));
                }

                if (lines[25] == "PastEffectOn")
                {
                    GameObject.FindObjectOfType<PostProcessingController>().PastEffect();
                    if (lines[26] == "False" || lines[26] == "")
                    {

                    }
                    else
                    {
                        Invoke("FRMNext", float.Parse(lines[26]));
                    }
                }
                if (lines[25] == "PastEffectOff")
                {
                    GameObject.FindObjectOfType<PostProcessingController>().PastEffectOffTween();
                    if (lines[26] == "False" || lines[26] == "")
                    {

                    }
                    else
                    {
                        Invoke("FRMNext", float.Parse(lines[26]));
                    }
                }
                if (lines[25] == "PastEffectOnImmediate")
                {
                    GameObject.FindObjectOfType<PostProcessingController>().PastEffectOn();
                    if (lines[26] == "False" || lines[26] == "")
                    {

                    }
                    else
                    {
                        Invoke("FRMNext", float.Parse(lines[26]));
                    }
                }
                if (lines[25] == "PastEffectOffImmediate")
                {
                    GameObject.FindObjectOfType<PostProcessingController>().PastEffectOff();
                    if (lines[26] == "False" || lines[26] == "")
                    {

                    }
                    else
                    {
                        Invoke("FRMNext", float.Parse(lines[26]));
                    }
                }



                if (lines.Length > 27 && lines[27] == "AfterPointInPicture")
                {
                    foreach (Transform child in GameObject.Find("Picture").transform)
                    {
                        if (child.gameObject.name != "SelectionAppears")
                        {
                            Destroy(child.gameObject);
                        }
                    }

                    if (isExamining3d)
                    {
                        isExamining3d = false;
                    }


                }




            }

        }
    }

    public bool isDynamic()
    {

        if (tw.isTyping || lines[0].Contains("Fade") || FCServices.Compare(lines[0].Replace("[", "").Replace("]", ""), "ToBeContinued", "ToNextCEStatement", "ToCEStatement", "CameraTween", "AfterExamine", "Testimony", "TestimonyEnd",  "ShowPicture", "HidePicture", "AfterTestimonyEnd", "GavelSlam", "GavelSlam3", "AllRise", "Verdict", "CrossExaminationStart",  "EvidenceEnter", "EvidenceExit", "EvidenceExitPresent", "BGFade", "Interjection") || IsInvoking("FRMNext") || lines.Length > 25 && FCServices.Compare(lines[25], "FinalPointWright", "ProceedDelayed", "ProceedOnAnimationEndCRT", "FadeOutDelayed") || lines.Length > 26 && FCServices.Compare(lines[25], "CourtZoomedOut", "CourtZoomOut", "CourtRotate1", "CourtRotate2", "PastEffectOn", "PastEffectOff", "PastEffectOnImmediate", "PastEffectOffImmediate") && lines[26] != "False" || lines.Length > 27 && lines[25] == "Set3dExaminedObjectAnimatorTrigger" && lines[27] != "False")
        {
            return true;
        }
        return false;
    }

   
    public void CharacterLookAt(string torotate, string tolookat, float rotoffset)
    {
        BaseCharacterController victim = this.FindCharacter(torotate);
        BaseCharacterController at = this.FindCharacter(tolookat);

        Vector3 targetPostition = new Vector3(at.transform.position.x,
                                              victim.transform.position.y,
                                                at.transform.position.z);


        victim.transform.LookAt(targetPostition);
        victim.transform.Rotate(0f, rotoffset, 0f);

    }

    public void CharacterToOriginalRotation(string torotate)
    {
        BaseCharacterController victim = this.FindCharacter(torotate);
        victim.transform.rotation = victim.originalrotation;
    }

    public void CharacterLookAtTween(string torotate, string tolookat, float rotoffset, float time)
    {
        BaseCharacterController victim = this.FindCharacter(torotate);
        BaseCharacterController at = this.FindCharacter(tolookat);

        Vector3 targetPostition = new Vector3(at.transform.position.x,
                                              victim.transform.position.y,
                                                at.transform.position.z);


        victim.transform.LookAt(targetPostition);
        victim.transform.Rotate(0f, rotoffset, 0f);

        Quaternion targetrot = victim.transform.rotation;
        victim.transform.rotation = victim.originalrotation;
        victim.transform.DORotateQuaternion(targetrot, time);

    }

    public void CharacterToOriginalRotationTween(string torotate, float time)
    {
        BaseCharacterController victim = this.FindCharacter(torotate);
        victim.transform.DORotateQuaternion(victim.originalrotation, time);
    }


  
    public IEnumerator CorrectAnimDelay(Animator animator)
    {
        yield return new WaitForSeconds(0.1f);

        Invoke("FRMNext", animator.GetNextAnimatorClipInfo(0)[0].clip.length + animator.GetAnimatorTransitionInfo(0).duration);
       
        yield break;
    }

    public IEnumerator UpdateEvidenceDescription(Transform child)
    {
        yield return new WaitForSeconds(30f / 45f);
        Record record = GameObject.FindObjectOfType<Record>();
        FCServices.FindChildWithName(child.gameObject, "Text (1)").GetComponent<Text>().text = lines[4];
        yield break;
    }
    public IEnumerator UpdateEvidenceDescription(Transform child, string updated)
    {
        yield return new WaitForSeconds(0f);
        Record record = GameObject.FindObjectOfType<Record>();
        FCServices.FindChildWithName(child.gameObject, "Text (1)").GetComponent<Text>().text = updated;
        yield break;
    }



    public bool areAllStatementsPressed()
    {
        foreach (bool b in CEpressed)
        {
            if (b == false)
            {
                return false;
            }
        }
        return true;
    }
  


  

   

    public void StartTestimony()
    {

    }



  
}
