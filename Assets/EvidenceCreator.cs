#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class EvidenceCreator : EditorWindow
{

    Sprite EvidenceSprite;
    string EvidenceName;
    string EvidenceDisplayName;
    string EvidenceDescription;
    string Age;
    EvidenceSelect.selecttype thistype;
    enum Side
    {
        Left, 
        Right
    }

    Side thisside;

    [MenuItem("Window/Evidence Creator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EvidenceCreator window = (EvidenceCreator)EditorWindow.GetWindow(typeof(EvidenceCreator));
        window.Show();
    }

    private void OnGUI()
    {
        EvidenceName = EditorGUILayout.TextField("Evidence Name", EvidenceName);
        GUILayout.Label("Evidence Sprite (Square, Grey Background)");
        EvidenceSprite = (Sprite)EditorGUILayout.ObjectField(EvidenceSprite, typeof(Sprite));
        EvidenceDisplayName = EditorGUILayout.TextField("Evidence Label", EvidenceDisplayName);
        thistype = (EvidenceSelect.selecttype)EditorGUILayout.EnumPopup("Evidence Type", thistype);
        if (thistype == EvidenceSelect.selecttype.Evidence)
        {
            GUILayout.Label("Description");
            EvidenceDescription = EditorGUILayout.TextArea(EvidenceDescription);
        }
        else
        {
            GUILayout.Label("Age");
            Age = EditorGUILayout.TextArea(Age);
            GUILayout.Label("Description");
            EvidenceDescription = EditorGUILayout.TextArea(EvidenceDescription);
        }

        if (GUILayout.Button("Create Evidence"))
        {
            GameObject NewEvidence = Instantiate(GameObject.FindObjectOfType<Record>().EvidenceList[0], FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, "Image").transform);
            NewEvidence.name = EvidenceName;
            NewEvidence.GetComponentInChildren<Image>().sprite = EvidenceSprite;
            FCServices.FindChildWithName(NewEvidence, "Text").GetComponent<Text>().text = EvidenceDisplayName;
            if (thistype == EvidenceSelect.selecttype.Evidence)
            {
                FCServices.FindChildWithName(NewEvidence, "Text (1)").GetComponent<Text>().text = EvidenceDescription;
                GameObject.FindObjectOfType<Record>().EvidenceList.Add(NewEvidence);
            }
            else
            {
                string profiletext;
                profiletext = "";
                
                    profiletext += "                                                  Age: ";
                    profiletext += Age;
                
               
                profiletext += Environment.NewLine;
                profiletext += EvidenceDescription;
                FCServices.FindChildWithName(NewEvidence, "Text (1)").GetComponent<Text>().text = profiletext;
                GameObject.FindObjectOfType<Record>().ProfilesList.Add(NewEvidence);

            }
            GameObject newbutton = Instantiate(FCServices.FindChildWithName(GameObject.FindObjectOfType<Record>().gameObject, GameObject.FindObjectOfType<Record>().EvidenceList[0].name), GameObject.FindObjectOfType<Record>().gameObject.transform);
            newbutton.name = EvidenceName;
            newbutton.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = EvidenceSprite;
            newbutton.GetComponent<EvidenceSelect>().thistype = thistype;
            if (thistype == EvidenceSelect.selecttype.Evidence)
            {
                newbutton.GetComponent<EvidenceSelect>().ToSelect = GameObject.FindObjectOfType<Record>().EvidenceList.IndexOf(NewEvidence) + 1;
                newbutton.transform.localPosition = GameObject.FindObjectOfType<Record>().buttonpos();
            }
            else
            {
                newbutton.GetComponent<EvidenceSelect>().ToSelect = GameObject.FindObjectOfType<Record>().ProfilesList.IndexOf(NewEvidence) + 1;
                newbutton.transform.localPosition = GameObject.FindObjectOfType<Record>().buttonposprof();
            }

            newbutton.transform.SetSiblingIndex(GameObject.FindObjectOfType<Record>().gameObject.transform.childCount - 3);
            NewEvidence.SetActive(false);
            if (thistype == EvidenceSelect.selecttype.Profiles || thistype == EvidenceSelect.selecttype.Evidence && GameObject.FindObjectOfType<Record>().EvidenceList.IndexOf(NewEvidence) + 1 > 5)
            {
                newbutton.SetActive(false);
            }
           



        }

        if (GUILayout.Button("Delete Evidence"))
        {
            if (thistype == EvidenceSelect.selecttype.Evidence)
            {
                GameObject.FindObjectOfType<Record>().RemoveEvidence(EvidenceName);
            }
            else
            {
                GameObject.FindObjectOfType<Record>().RemoveProfile(EvidenceName);
            }
        }        
    }

}
#endif