using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Record : MonoBehaviour
{
    // Start is called before the first frame update
    public ModifyStrings hq;
    public bool isDisplayingEvidence;
    public int ActiveEv;
    public int MinEv;
    public int MaxEv;
    public int Page;
    public List<GameObject> EvidenceList;
    public List<GameObject> ProfilesList;
    public GameObject ObjPrefab;
    public GameObject Obj;
    public AudioSource ObjS;
    public Vector3 Ev1Pos;
    public Vector3 Ev2Pos;
    public Vector3 Ev3Pos;
    public Vector3 Ev4Pos;
    public Vector3 Ev5Pos;
    public bool isPresenting;

    public void SyncCollectedEvidence(int slotnumber)
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

    public void Close()
    {

        if (isPresenting)
        {
            
            GameObject.FindObjectOfType<PenaltyController>().HidePendingPenalty();
        }
        FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "Image (2)").SetActive(true);
        foreach (GameObject go in EvidenceList)
        {
            if (go == EvidenceList[0])
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            if (this.PageOfEvidence(EvidenceList.IndexOf(go) + 1) == 1)
            {
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
            }
            else
            {
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
            }
        }
        SelectEvidence(1);
        foreach (GameObject go in ProfilesList)
        {
            go.SetActive(false);
            FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
        }
        isDisplayingEvidence = true;
            base.gameObject.GetComponent<Canvas>().enabled = false;
        base.gameObject.GetComponents<AudioSource>()[2].Play();
       
        isPresenting = false;
    }

    public void Close(bool issilent)
    {
        FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "Image (2)").SetActive(true);
        foreach (GameObject go in EvidenceList)
        {
            if (go == EvidenceList[0])
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
            if (this.PageOfEvidence(EvidenceList.IndexOf(go) + 1) == 1)
            {
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
            }
            else
            {
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
            }
        }
        SelectEvidence(1);
        foreach (GameObject go in ProfilesList)
        {
            go.SetActive(false);
            FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
        }
        isDisplayingEvidence = true;
        base.gameObject.GetComponent<Canvas>().enabled = false;
        if (issilent)
        {

        }
        else
        {
            base.gameObject.GetComponents<AudioSource>()[2].Play();
        }
        if (isPresenting)
        {
            GameObject.FindObjectOfType<PenaltyController>().HidePendingPenalty();
        }
        isPresenting = false;
    }


    public void AddEvidence(string name, string name2)
    {
        GameObject evidence = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name), base.gameObject.transform.GetChild(2));
        GameObject button = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name2), base.gameObject.transform);
       
        
        EvidenceList.Add(evidence);
        evidence.name = name;
        button.name = evidence.name;
        button.transform.SetSiblingIndex(gameObject.transform.childCount - 3);

        button.GetComponent<EvidenceSelect>().ToSelect = EvidenceList.Count;

        button.transform.localPosition = this.buttonpos();
        evidence.SetActive(false);
        if (this.PageOfEvidence(ActiveEv) != this.PageOfEvidence(EvidenceList.Count))
        {
            button.SetActive(false);
        }




    }

    public void AddEvidence(string name, string name2, string name3)
    {
        GameObject evidence = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name), base.gameObject.transform.GetChild(2));
        GameObject button = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name2), base.gameObject.transform);
        GameObject details = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name3), FCServices.FindChildWithName(base.gameObject, "Details").transform);

        EvidenceList.Add(evidence);
        evidence.name = name;
        button.name = evidence.name;
        details.name = evidence.name;
        details.SetActive(false);
        button.transform.SetSiblingIndex(gameObject.transform.childCount - 3);

        button.GetComponent<EvidenceSelect>().ToSelect = EvidenceList.Count;

        button.transform.localPosition = this.buttonpos();
        evidence.SetActive(false);
        if (this.PageOfEvidence(ActiveEv) != this.PageOfEvidence(EvidenceList.Count))
        {
            button.SetActive(false);
        }




    }


    public void AddProfile(string name, string name2)
    {
        GameObject evidence = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name), base.gameObject.transform.GetChild(2));
        GameObject button = Instantiate(GameObject.FindObjectOfType<PrefabController>().FindPrefab(name2), base.gameObject.transform);
        ProfilesList.Add(evidence);
        evidence.name = name;
        button.name = evidence.name;
        button.transform.SetSiblingIndex(gameObject.transform.childCount - 3);

        button.GetComponent<EvidenceSelect>().ToSelect = ProfilesList.Count;
        button.GetComponent<EvidenceSelect>().thistype = EvidenceSelect.selecttype.Profiles;

       button.transform.localPosition = this.buttonposprof();
        evidence.SetActive(false);
        if (this.PageOfEvidence(ActiveEv) != this.PageOfEvidence(ProfilesList.Count))
        {
            button.SetActive(false);
        }




    }


    public void Switch()
    {
        if (!base.gameObject.GetComponents<AudioSource>()[0].isPlaying && base.gameObject.GetComponent<Canvas>().enabled)
        {
            base.gameObject.GetComponents<AudioSource>()[0].Play();
        }
        if (isDisplayingEvidence)
        {
            foreach (GameObject go in EvidenceList)
            {
                go.SetActive(false);
                
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
            }
            foreach (GameObject go in ProfilesList)
            {
                if (ActiveEv <= ProfilesList.Count)
                {
                    if (PageOfEvidence(ProfilesList.IndexOf(go) + 1) == PageOfEvidence(ActiveEv))
                {
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                }
                else
                {
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                }}
                else
                {

                    if (PageOfEvidence(ProfilesList.IndexOf(go) + 1) == PageOfEvidence(ProfilesList.Count))
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                    }
                    else
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                    }
                }
            }

            if (ActiveEv <= ProfilesList.Count)
            {
                SelectProfile(ActiveEv);
            }
            else
            {
                SelectProfile(ProfilesList.Count);
            }
            isDisplayingEvidence = false;
        }
        else
        {
            foreach (GameObject go in ProfilesList)
            {
                go.SetActive(false);
                FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
            }
            foreach (GameObject go in EvidenceList)
            {
                if (ActiveEv <= EvidenceList.Count)
                {
                    if (PageOfEvidence(EvidenceList.IndexOf(go) + 1) == PageOfEvidence(ActiveEv))
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                    }
                    else
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                    }
                }
                else
                {
                    if (PageOfEvidence(EvidenceList.IndexOf(go) + 1) == PageOfEvidence(ProfilesList.Count))
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                    }
                    else
                    {
                        FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                    }
                }
               
            }
            if (ActiveEv <= EvidenceList.Count)
            {
                SelectEvidence(ActiveEv);
            }
            else
            {
                SelectEvidence(EvidenceList.Count);
            }
            isDisplayingEvidence = true;
        }
    }

    public void PreviousPage()
    {
        if (isDisplayingEvidence)
        {
            if (this.PageOfEvidence(EvidenceList.Count) > 1)
            {
                base.gameObject.GetComponents<AudioSource>()[3].Play();
                if (Page != 1)
                {
                   
                    SelectEvidence(ActiveEv - 5);

                }
                else
                {
                    if (this.PageOfEvidence(EvidenceList.Count) * 5 + ActiveEv <= EvidenceList.Count)
                    {
                        SelectEvidence(this.PageOfEvidence(EvidenceList.Count) * 5 + ActiveEv);
                    }
                    else
                    {
                        SelectEvidence(this.PageOfEvidence(EvidenceList.Count) * 5 - 4);
                    }
                }
            }
            
        }
        else
        {
            if (this.PageOfEvidence(ProfilesList.Count) > 1)
            {
                base.gameObject.GetComponents<AudioSource>()[3].Play();
                if (Page != 1)
                {

                    SelectEvidence(ActiveEv - 5);

                }
                else
                {
                    if (this.PageOfEvidence(ProfilesList.Count) * 5 + ActiveEv <= ProfilesList.Count)
                    {
                        SelectEvidence(this.PageOfEvidence(ProfilesList.Count) * 5 + ActiveEv);
                    }
                    else
                    {
                        SelectEvidence(this.PageOfEvidence(ProfilesList.Count) * 5 - 4);
                    }
                }
            }

        }
    }


    public void OpenDetails(string evidencename)
    {
         base.gameObject.GetComponents<AudioSource>()[4].Play();
        FCServices.FindChildWithName(base.gameObject, "Details").SetActive(true);
        foreach (Transform child in FCServices.FindChildWithName(base.gameObject, "Details").transform)
        {
            if (child.gameObject.name != "Panel" && child.gameObject.name != "Button" && child.gameObject.name != evidencename)
            {
                child.gameObject.SetActive(false);
            }
            else
            {
                child.gameObject.SetActive(true);
            }

        }


    }

    public void CloseDetails()
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        FCServices.FindChildWithName(base.gameObject, "Details").SetActive(false);
    }


    public Vector3 buttonpos()
    {
        int num = 5 - (this.PageOfEvidence(EvidenceList.Count) * 5 - EvidenceList.Count);
        switch (num)
        {
            case 1:
                return Ev1Pos;
               
            case 2:
                return Ev2Pos;
              
            case 3:
                return Ev3Pos;
                
            case 4:
                return Ev4Pos;
                

            case 5:
                return Ev5Pos;

        }
        return Vector3.zero;

            }
    public Vector3 buttonposgeneral(int id)
    {
        int num = 5 - (this.PageOfEvidence(id) * 5 - id);
        switch (num)
        {
            case 1:
                return Ev1Pos;

            case 2:
                return Ev2Pos;

            case 3:
                return Ev3Pos;

            case 4:
                return Ev4Pos;


            case 5:
                return Ev5Pos;

        }
        return Vector3.zero;

    }


    public Vector3 buttonposprof()
    {
        int num = 5 - (this.PageOfEvidence(EvidenceList.Count) * 5 - ProfilesList.Count);
        switch (num)
        {
            case 1:
                return Ev1Pos;

            case 2:
                return Ev2Pos;

            case 3:
                return Ev3Pos;

            case 4:
                return Ev4Pos;


            case 5:
                return Ev5Pos;

        }
        return Vector3.zero;

    }

    public string ActiveEvidenceName()
    {
        if (isDisplayingEvidence)
        {
            return EvidenceList[ActiveEv - 1].name;
        }
        else
        {
            return ProfilesList[ActiveEv - 1].name;
        }

    }

    public void NextPage()
    {
        if (isDisplayingEvidence)
        {

            if (this.PageOfEvidence(EvidenceList.Count) > 1)
            {
                base.gameObject.GetComponents<AudioSource>()[3].Play();

                if (Page < this.PageOfEvidence(EvidenceList.Count))
                {
                    if (EvidenceList.Count >= ActiveEv + 5)
                    {
                        SelectEvidence(ActiveEv + 5);
                    }
                    else
                    {
                        SelectEvidence(EvidenceList.Count);
                    }
                }
                else
                {
                    SelectEvidence( 5 - (Page * 5 - ActiveEv));
                }
            }

            
        }
        else
        {
            
         if (this.PageOfEvidence(ProfilesList.Count) > 1)
            {
                base.gameObject.GetComponents<AudioSource>()[3].Play();

                if (Page < this.PageOfEvidence(ProfilesList.Count))
                {
                    if (ProfilesList.Count >= ActiveEv + 5)
                    {
                        SelectEvidence(ActiveEv + 5);
                    }
                    else
                    {
                        SelectEvidence(ProfilesList.Count);
                    }
                }
                else
                {
                    SelectEvidence(5 - (Page * 5 - ActiveEv));
                }
            }
        }
        
    }

    public void CourtRecordBack()
    {
        
        if (hq.isTalkingToCharacter || hq.lines.Length > 24 && hq.lines[24] == "PsycheLockExit"){
            if (hq.isTalkingToCharacter)
            {
                Close();
                GameObject.Find("Interaction").GetComponent<Canvas>().enabled = true;
                hq.isPresentingToCharacter = false;

            }
            if (hq.lines.Length > 24 && hq.lines[24] == "PsycheLockExit")
            {

                hq.LoadFromFRM(int.Parse(hq.lines[25]));

            }
        }
        else
        {
            Close();
           
        }
        hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().enabled = true;
        FCServices.ButtonUpEffect(hq.PresentButton.gameObject);
       

    }

    public void Open()
    {






        SelectEvidence(1);
        base.gameObject.GetComponent<Canvas>().enabled = true;
        base.gameObject.GetComponents<AudioSource>()[1].Play();
       
    }
    public void Open(bool slowdowntime)
    {

        




        ActiveEv = 1;
        base.gameObject.GetComponent<Canvas>().enabled = true;
        base.gameObject.GetComponents<AudioSource>()[1].Play();

    }
    public void Present()
    {
       
        Open(false);
       
        if (hq.isInCE)
        {
            GameObject.FindObjectOfType<PenaltyController>().ShowPendingPenalty(1);

        }
        hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().enabled = false;
        isPresenting = true;
    }

    public void Update()
    {

        if (FCServices.FindChildWithName(base.gameObject, "Image").GetComponent<Animator>().GetBool("isEvidence") != isDisplayingEvidence)
        {
            FCServices.FindChildWithName(base.gameObject, "Image").GetComponent<Animator>().SetBool("isEvidence", isDisplayingEvidence);
        }

        if (hq.lines[0] == "[EvidenceEnter]" && Input.anyKeyDown)
        {
            hq.FRMNext();
            FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").SetActive(true);
        }

        if (FCServices.FindChildWithName(base.gameObject, "EvPrSwitch") != null)
        {
            if (!FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").activeInHierarchy && !isPresenting && hq.lines[0] != "[EvidenceEnter]")
            {
                FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").SetActive(true);
            }
            if (FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").activeInHierarchy && isPresenting)
            {
                FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").SetActive(false);
            }

            if (FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").GetComponent<Animator>().GetBool("isDisplayingEvidence") != isDisplayingEvidence)
            {
                FCServices.FindChildWithName(base.gameObject, "EvPrSwitch").GetComponent<Animator>().SetBool("isDisplayingEvidence", isDisplayingEvidence);
            }

        }

        if (FCServices.FindChildWithName(base.gameObject, "PresentButton") != null)
        {
            if (!FCServices.FindChildWithName(base.gameObject, "PresentButton").activeInHierarchy && isPresenting || !FCServices.FindChildWithName(base.gameObject, "PresentButton").activeInHierarchy && hq.isPresentingToCharacter)
            {
                FCServices.FindChildWithName(base.gameObject, "PresentButton").SetActive(true);
            }
            if (FCServices.FindChildWithName(base.gameObject, "PresentButton").activeInHierarchy && !isPresenting && !hq.isPresentingToCharacter)
            {
                FCServices.FindChildWithName(base.gameObject, "PresentButton").SetActive(false);
            }
        }
       
        if (hq.lines[0] == "[Present]" && FCServices.FindChildWithName(base.gameObject, "BackButton").activeSelf)
        {
            FCServices.FindChildWithName(base.gameObject, "BackButton").SetActive(false);
        }
        if (hq.lines[0] != "[Present]" && !FCServices.FindChildWithName(base.gameObject, "BackButton").activeSelf)
        {
            FCServices.FindChildWithName(base.gameObject, "BackButton").SetActive(true);
        }




        if (hq.isTalkingToCharacter && hq.isPresentingToCharacter && Input.GetKeyUp(KeyCode.E) && base.gameObject.GetComponent<Canvas>().enabled || hq.isTalkingToCharacter && Input.GetKeyUp(KeyCode.JoystickButton2) && base.gameObject.GetComponent<Canvas>().enabled)
        {
            if (hq.talkingto.EvidenceFromName(ActiveEvidenceName()) != null)
            {
                hq.LoadFromFRM(hq.talkingto.EvidenceFromName(ActiveEvidenceName()).EvidenceFRM);
                hq.isTalkingToCharacter = false;
                hq.isPresentingToCharacter = false;
                Close(true);
            }
            else
            {
                hq.LoadFromFRM(hq.talkingto.AnyEvidenceFRM);
                hq.isTalkingToCharacter = false;
                hq.isPresentingToCharacter = false;
                Close(true);
            }


        }

       



        if (FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithArray(base.gameObject, "Details", "Button"));
            }
            if (Input.GetKeyUp(KeyCode.Backspace) || Input.GetKeyUp(KeyCode.JoystickButton1))
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithArray(base.gameObject, "Details", "Button"));
                CloseDetails();
            }

        }

        if (base.gameObject.GetComponent<Canvas>().enabled)
        {
            if (Gamepad.current != null && Gamepad.current.dpad.IsPressed() && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf || Input.GetKeyDown(KeyCode.LeftArrow) && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf || Input.GetKeyDown(KeyCode.RightArrow) && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
            {
                if (Gamepad.current != null && Gamepad.current.dpad.right.wasPressedThisFrame && ActiveEv != EvidenceList.Count && isDisplayingEvidence || Gamepad.current != null && Gamepad.current.dpad.right.wasPressedThisFrame && ActiveEv != ProfilesList.Count && !isDisplayingEvidence || Input.GetKeyDown(KeyCode.RightArrow) && ActiveEv != ProfilesList.Count && !isDisplayingEvidence || Input.GetKeyDown(KeyCode.RightArrow) && ActiveEv != EvidenceList.Count && isDisplayingEvidence)
                {
                    if (!base.gameObject.GetComponents<AudioSource>()[0].isPlaying)
                    {
                        base.gameObject.GetComponents<AudioSource>()[0].Play();
                    }
                    if (isDisplayingEvidence)
                    {

                        SelectEvidence(ActiveEv + 1);
                    }
                    else
                    {
                        SelectProfile(ActiveEv + 1);
                    }
                }
                if (Gamepad.current != null && Gamepad.current.dpad.left.wasPressedThisFrame && ActiveEv != 1 || Input.GetKeyDown(KeyCode.LeftArrow) && ActiveEv != 1)
                {
                    if (!base.gameObject.GetComponents<AudioSource>()[0].isPlaying)
                    {
                        base.gameObject.GetComponents<AudioSource>()[0].Play();
                    }
                    if (isDisplayingEvidence)
                    {
                        SelectEvidence(ActiveEv - 1);
                    }
                    else
                    {
                        SelectProfile(ActiveEv - 1);
                    }
                }

            }


        }



        if (isDisplayingEvidence)
        {
            if (EvidenceList.Count <= 5 && FCServices.FindChildWithName(base.gameObject, "NextPage").activeSelf && FCServices.FindChildWithName(base.gameObject, "PreviousPage").activeSelf)
            {
                FCServices.FindChildWithName(base.gameObject, "NextPage").SetActive(false);
                FCServices.FindChildWithName(base.gameObject, "PreviousPage").SetActive(false);
            }
            if (EvidenceList.Count > 5 && !FCServices.FindChildWithName(base.gameObject, "NextPage").activeSelf && !FCServices.FindChildWithName(base.gameObject, "PreviousPage").activeSelf)
            {
                FCServices.FindChildWithName(base.gameObject, "NextPage").SetActive(true);
                FCServices.FindChildWithName(base.gameObject, "PreviousPage").SetActive(true);
            }

        }
        else
        {
            if (ProfilesList.Count <= 5 && FCServices.FindChildWithName(base.gameObject, "NextPage").activeSelf && FCServices.FindChildWithName(base.gameObject, "PreviousPage").activeSelf)
            {
                FCServices.FindChildWithName(base.gameObject, "NextPage").SetActive(false);
                FCServices.FindChildWithName(base.gameObject, "PreviousPage").SetActive(false);
            }
            if (ProfilesList.Count > 5 && !FCServices.FindChildWithName(base.gameObject, "NextPage").activeSelf && !FCServices.FindChildWithName(base.gameObject, "PreviousPage").activeSelf)
            {
                FCServices.FindChildWithName(base.gameObject, "NextPage").SetActive(true);
                FCServices.FindChildWithName(base.gameObject, "PreviousPage").SetActive(true);
            }

        }



        if (Input.GetKeyDown(KeyCode.Tab) && base.gameObject.GetComponent<Canvas>().enabled && !isPresenting || Input.GetKeyDown(KeyCode.JoystickButton5) && base.gameObject.GetComponent<Canvas>().enabled && !isPresenting)
        {
            if (hq.lines.Length > 0 && hq.lines[0] == "[Present]")
            {

            }
            else
            {
                if (!FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
                {
                    Switch();
                }
              
            }
        }


        if (Input.GetKeyUp(KeyCode.E) && hq.isInCE && !base.gameObject.GetComponent<Canvas>().enabled && hq.PresentButton.gameObject.transform.parent.gameObject.GetComponent<Canvas>().enabled || Input.GetKeyUp(KeyCode.JoystickButton2) && hq.isInCE && !base.gameObject.GetComponent<Canvas>().enabled && hq.PresentButton.gameObject.transform.parent.gameObject.GetComponent<Canvas>().enabled)
        {
            if (!hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().invokeOnClick)
            {
                Open(false);
                isPresenting = true;
                if (hq.isInCE)
                {
                    GameObject.FindObjectOfType<PenaltyController>().ShowPendingPenalty(1);

                }
                hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().enabled = false;
            }
            return;
        }

        if (Input.GetKeyUp(KeyCode.E) && !hq.isPresentingToCharacter && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf || Input.GetKeyUp(KeyCode.JoystickButton2) && !hq.isPresentingToCharacter && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
        {
            PresentButton();
            hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().enabled = true;
            FCServices.ButtonUpEffect(hq.PresentButton.gameObject);
            
        }

        if (FCServices.FindChildWithName(base.gameObject, "NextPage").activeSelf && base.gameObject.GetComponent<Canvas>().enabled && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame)
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithName(base.gameObject, "NextPage"));
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) || Gamepad.current != null && Gamepad.current.dpad.up.wasReleasedThisFrame)
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithName(base.gameObject, "NextPage"));
                FCServices.FindChildWithName(base.gameObject, "NextPage").GetComponent<Button>().onClick.Invoke();
            }

        }
        if (FCServices.FindChildWithName(base.gameObject, "PreviousPage").activeSelf && base.gameObject.GetComponent<Canvas>().enabled && !FCServices.FindChildWithName(base.gameObject, "Details").activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Gamepad.current != null && Gamepad.current.dpad.down.wasPressedThisFrame)
            {
                FCServices.ButtonDownEffect(FCServices.FindChildWithName(base.gameObject, "PreviousPage"));
            }
            if (Input.GetKeyUp(KeyCode.DownArrow) || Gamepad.current != null && Gamepad.current.dpad.down.wasReleasedThisFrame)
            {
                FCServices.ButtonUpEffect(FCServices.FindChildWithName(base.gameObject, "PreviousPage"));
                FCServices.FindChildWithName(base.gameObject, "PreviousPage").GetComponent<Button>().onClick.Invoke();
            }

        }

    }

    public void PresentButton()
    {
        ModifyStrings hq = GameObject.FindObjectOfType<ModifyStrings>();
        
        if (!hq.isPresentingToCharacter)
        {
            if (!hq.tw.isTyping && base.gameObject.GetComponent<Canvas>().enabled && hq.lines.Length > 24 && hq.lines[24] == "{CrossExamination}" && hq.lines[25] == "Statement" && isPresenting)
            {

                base.gameObject.GetComponent<Canvas>().enabled = false;
               
                if (hq.lines[27] != "NonObjectionable" && EvidenceList[ActiveEv - 1].name == hq.lines[27] || hq.lines[28] != "" && EvidenceList[ActiveEv - 1].name == hq.lines[28])
                {
                    hq.LoadFromFRM(int.Parse(hq.lines[30]));
                }
                else
                {
                    hq.LoadFromFRM(int.Parse(hq.lines[29]));
                }
            }
            if (hq.lines[0] == "[Present]")
            {
                foreach (GameObject go in EvidenceList)
                {
                    if (go == EvidenceList[0])
                    {
                        go.SetActive(true);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                }

                foreach (GameObject go in ProfilesList)
                {
                    go.SetActive(false);
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                }
                isDisplayingEvidence = true;
                base.gameObject.GetComponent<Canvas>().enabled = false;
                if (hq.lines[3] == "1")
                {
                    if (hq.lines[4] == EvidenceList[ActiveEv - 1].name)
                    {

                        hq.LoadFromFRM(int.Parse(hq.lines[2]));
                    }
                    else
                    {

                        hq.LoadFromFRM(int.Parse(hq.lines[1]));
                    }
                }
            }
           
          
            hq.PresentButton.gameObject.GetComponent<ImitateOnClick>().enabled = true;
            FCServices.ButtonUpEffect(hq.PresentButton.gameObject);
        }
        else
        {
            if (hq.talkingto.EvidenceFromName(ActiveEvidenceName()) != null)
            {
                hq.LoadFromFRM(hq.talkingto.EvidenceFromName(ActiveEvidenceName()).EvidenceFRM);
                hq.isTalkingToCharacter = false;
                hq.isPresentingToCharacter = false;
                Close(true);
            }
            else
            {
                hq.LoadFromFRM(hq.talkingto.AnyEvidenceFRM);
                hq.isTalkingToCharacter = false;
                hq.isPresentingToCharacter = false;
                Close(true);
            }
        }
        isPresenting = false;
    }

    public void PreviousEv()
    {
       
        if (ActiveEv == MinEv)
        {
            SelectEvidence(MaxEv);
        }
        else {
            SelectEvidence(ActiveEv - 1);

        }

    }
    public void NextEv()
    {

        if (ActiveEv == MaxEv)
        {
            SelectEvidence(MinEv);
        }
        else
        {
            SelectEvidence(ActiveEv + 1);
        }
    }
 
    public void RemoveEvidence(string name)
    {
        int evid = EvidenceIdByName(name);
        Destroy(EvidenceList[evid - 1]);
        List<GameObject> newevl = new List<GameObject>();
        foreach (GameObject gob in EvidenceList)
        {
            if (gob != null && EvidenceList.IndexOf(gob) != evid - 1)
            {
                newevl.Add(gob);
            }
        }
        EvidenceList = newevl;

        foreach (EvidenceSelect es in FCServices.GetAllObjectsOfTypeInScene<EvidenceSelect>())
        {
            if (es.ToSelect == evid && es.thistype == EvidenceSelect.selecttype.Evidence)
            {
                Destroy(es.gameObject);
            }
            if (es.ToSelect > evid && es.thistype == EvidenceSelect.selecttype.Evidence)
            {
                es.ToSelect -= 1;
            }
            es.gameObject.GetComponent<RectTransform>().localPosition = buttonposgeneral(es.ToSelect);
            
        }
        Switch();
        Switch();
    }
    public void RemoveProfile(string name)
    {
        int evid = ProfileIdByName(name);
        Destroy(ProfilesList[evid - 1]);
        List<GameObject> newevl = new List<GameObject>();
        foreach (GameObject gob in ProfilesList)
        {
            if (gob != null && ProfilesList.IndexOf(gob) != evid - 1)
            {
                newevl.Add(gob);
            }
        }
        ProfilesList = newevl;

        foreach (EvidenceSelect es in FCServices.GetAllObjectsOfTypeInScene<EvidenceSelect>())
        {
            if (es.ToSelect == evid && es.thistype == EvidenceSelect.selecttype.Profiles)
            {
                Destroy(es.gameObject);
            }
            if (es.ToSelect > evid && es.thistype == EvidenceSelect.selecttype.Profiles)
            {
                es.ToSelect -= 1;
            }
            es.gameObject.GetComponent<RectTransform>().localPosition = buttonposgeneral(es.ToSelect);

        }
        Switch();
        Switch();
    }
    public int PageOfEvidence(int Id)
    {
        if (Id / 5 == (float)Id / 5f)
        {
            return Id / 5;
        }
        else
        {
            return Id / 5 + 1;
        }
    }

    public void SelectEvidence(int Id)
    {
        foreach (EvidenceSelect es in FCServices.GetAllObjectsOfTypeInScene<EvidenceSelect>())
        {
            if (es.ToSelect == Id && es.thistype == EvidenceSelect.selecttype.Evidence)
            {
                es.gameObject.GetComponent<Image>().color = new Color(es.gameObject.GetComponent<Image>().color.r, es.gameObject.GetComponent<Image>().color.g, es.gameObject.GetComponent<Image>().color.b, 1f);
            }
            else
            {
                es.gameObject.GetComponent<Image>().color = new Color(es.gameObject.GetComponent<Image>().color.r, es.gameObject.GetComponent<Image>().color.g, es.gameObject.GetComponent<Image>().color.b, 0f);
            }

        }


        if ((this.PageOfEvidence(Id) == this.PageOfEvidence(ActiveEv)))
        {
            
                 
            foreach (GameObject go in EvidenceList)
            {
                if (go == EvidenceList[Id - 1])
                {
                    go.SetActive(true);
                }
                else
                {
                    go.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject go in EvidenceList)
            {

                if (this.PageOfEvidence(EvidenceList.IndexOf(go) + 1) == this.PageOfEvidence(Id))
                {
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
          

                }
                else
                {

                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);




                }

                if (go == EvidenceList[Id - 1])
                {
                    go.SetActive(true);
                 
                   
                }
                else
                {
                    go.SetActive(false);
                   
                }
            }
            if (this.PageOfEvidence(ActiveEv) < this.PageOfEvidence(Id))
            {

                Page += 1;
            }
            if (this.PageOfEvidence(ActiveEv) > this.PageOfEvidence(Id))
            {

                Page -= 1;
            }

        }
        ActiveEv = Id;
        if (5 - (Page * 5 - Id) == 1)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-176.8f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);
              
        }
        if (5 - (Page * 5 - Id) == 2)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-81.70002f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 3)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(14.3f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 4)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(112.2f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 5)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(210.3f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }

    }

    public int EvidenceIdByName(string name)
    {
        foreach (GameObject go in EvidenceList)
        {
            if (go.name == name)
            {
                return EvidenceList.IndexOf(go) + 1;
            }
        }
        return 0;
    }
    public int ProfileIdByName(string name)
    {
        foreach (GameObject go in ProfilesList)
        {
            if (go.name == name)
            {
                return ProfilesList.IndexOf(go) + 1;
            }
        }
        return 0;
    }


    public void SelectProfile(int Id)
    {

        foreach (EvidenceSelect es in GameObject.FindObjectsOfType<EvidenceSelect>())
        {
            if (es.ToSelect == Id && es.thistype == EvidenceSelect.selecttype.Profiles)
            {
                es.gameObject.GetComponent<Image>().color = new Color(es.gameObject.GetComponent<Image>().color.r, es.gameObject.GetComponent<Image>().color.g, es.gameObject.GetComponent<Image>().color.b, 1f);
            }
            else
            {
                es.gameObject.GetComponent<Image>().color = new Color(es.gameObject.GetComponent<Image>().color.r, es.gameObject.GetComponent<Image>().color.g, es.gameObject.GetComponent<Image>().color.b, 0f);
            }

        }

        if ((this.PageOfEvidence(Id) == this.PageOfEvidence(ActiveEv)))
        {


            foreach (GameObject go in ProfilesList)
            {
                if (go == ProfilesList[Id - 1])
                {
                    go.SetActive(true);
                }
                else
                {
                    go.SetActive(false);
                }
            }
        }
        else
        {
            foreach (GameObject go in ProfilesList)
            {

                if (this.PageOfEvidence(ProfilesList.IndexOf(go) + 1) == this.PageOfEvidence(Id))
                {
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(true);
                }
                else
                {
                    FCServices.FindChildWithName(base.gameObject, go.name).SetActive(false);
                }

                if (go == ProfilesList[Id - 1])
                {

                    go.SetActive(true);
                  
                }
                else
                {
                    go.SetActive(false);
                    
                }
            }
            if (this.PageOfEvidence(ActiveEv) < this.PageOfEvidence(Id))
            {

                Page += 1;
            }
            if (this.PageOfEvidence(ActiveEv) > this.PageOfEvidence(Id))
            {

                Page -= 1;
            }

        }


        ActiveEv = Id;
        if (5 - (Page * 5 - Id) == 1)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-176.8f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 2)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(-81.70002f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 3)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(14.3f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 4)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(112.2f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
        if (5 - (Page * 5 - Id) == 5)
        {

            FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition = new Vector2(210.3f, FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<RectTransform>().anchoredPosition.y);

        }
    }

}
