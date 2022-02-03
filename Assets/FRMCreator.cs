#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FRMCreator : EditorWindow
{

    int mode;
    List<string> futurelines; 
    enum TextColor
    {
        White,
        Blue,
        Orange,
        Green,
        DarkerGreen
    }
    TextColor usedcolor;
    enum Blip
    {
        Regular,
        Male,
        Female,
        Silent
    }
    Blip usedblip;
    enum Alignment
    {
        Left,
        Center
    }
    Alignment usedalignment;
    bool autoproceed;

    enum PartOfMechanic
    {
        None,
        Testimony,
        CrossExamination
       

    }

    PartOfMechanic thispart;

    enum Line26Command
    {   
        None,
        PenaltyEnter,
        PenaltyExit,
        PenaltyHide,
        ShowPendingPenalty,
        TakePenalty,
        AddStatementCE,
        ReplaceStatementCE,
        FinalPointWright,
        CloseUp,
        ProceedOnAnimationEndCRT,
        CEStateChange,
        PopUpEvidence,
        EvidenceHide,
        EvidenceHideImmediate,
        ProceedDelayed,
        FlashEffect,
        Examined3dObjectChange,
        Set3dExaminedObjectAnimatorTrigger,
        CourtZoomedOut,
        CourtZoomOut,
        CourtRotate1,
        CourtRotate2,
        FixCamera,
        PastEffectOn,
        PastEffectOff,
        PastEffectOnImmediate,
        PastEffectOffImmediate,
        FadeOutDelayed,
        DeleteEvidence,
        DeleteProfile,
        AddProfile,
        UpdateEvidenceSilent,
        UnknownSpeaker,
        ChangeLetterTypeDelay,
        SkipToPose,
        SkipToPoseName,
        SkipToPoseNameCharacter




    }

    SpecialFRMS spf;

    enum InterjectionType
    {
        Objection,
        HoldIt,
        TakeThat,
        GotIt,
        Custom
    }
    enum InterjectionCharacter
    {
        Wright,
        Edgeworth,
        NonCharacterSpecific

    }

    enum EvidenceOperationType
    {
        Add,
        Update
    }

    enum SpecialFRMS
    {
        Choice,
        Examine3d,
        LoadScene,
        FadeOut,
        FadeOut2,
        FadeOutWhite,
        ToBeContinued,
        HideToBeContinued,
        FadeIn,
        FadeInWhite,
        FadeCross,
        FadeCrossWhite,
        FadeCrossFast,
        ToNextCEStatement,
        ToCEStatement,
        CameraTween,
        AfterExamine,
        Testimony,
        TestimonyEnd,
        CharacterFadeIn,
        CharacterFadeOut,
        ShowPicture,
        PointInPicture,
        PointIn3dEvidence,
        HidePicture,
        AfterTestimonyEnd,
        GavelSlam,
        GavelSlam3,
        AllRise,
        Verdict,
        CrossExaminationStart,
        EvidenceEnter,
        EvidenceExit,
        EvidenceExitPresent,
        Present,
        BGFade,
        Interjection



    }

    EvidenceOperationType currentEvOpType;

    Line26Command additionalevent;
    int additionaleventdatanum;
    int FRMId;
    string editfilepath;
    InterjectionType currentInterjectionType;
    InterjectionCharacter currentInterjectionCharacter;
    bool stopMusic;
    EvidenceSelect.selecttype presentType;
    bool objectionable;
    int statementcount;
    int lineslength;

    [MenuItem("Window/FRM Creator")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        FRMCreator window = (FRMCreator)EditorWindow.GetWindow(typeof(FRMCreator));
        window.Show();
    }

    void OnGUI()
    {
        if (mode == 0)
        {
            futurelines = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                futurelines.Add("");
            }
            if (GUILayout.Button("New Basic FRM"))
            {
                mode = 1;
            }
            if (GUILayout.Button("New Interjection FRM"))
            {
                mode = 3;
            }
            if (GUILayout.Button("New Camera Tween FRM"))
            {
                mode = 4;
            }
            if (GUILayout.Button("New Evidence Slide-In FRM"))
            {
                mode = 5;
            }
            if (GUILayout.Button("New Choice FRM"))
            {
                mode = 6;
            }
            if (GUILayout.Button("New Present Evidence FRM"))
            {
                mode = 7;
            }
            if (GUILayout.Button("New Present in Picture FRM"))
            {
                mode = 8;
            }
            if (GUILayout.Button("New Present in 3d Evidence FRM"))
            {
                mode = 10;
            }
            if (GUILayout.Button("New Cross-Examination Start FRM"))
            {
                mode = 9;
            }
            if (GUILayout.Button("New Misc. Special FRM"))
            {
                mode = 11;
            }
            editfilepath = EditorGUILayout.TextField("FRM Id to Edit", editfilepath);
            if (GUILayout.Button("Edit existing FRM"))
            {
                mode = 2;
                FRMId = int.Parse(editfilepath);
                futurelines = new List<string>();
                futurelines.AddRange(File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, editfilepath + ".frm")));
            }
        }

        if (mode == 3)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }
                
            }
            futurelines[0] = "[Interjection]";
            currentInterjectionType = (InterjectionType)EditorGUILayout.EnumPopup("Interjection Type", currentInterjectionType);
            futurelines[1] = currentInterjectionType.ToString();
            if (currentInterjectionType != InterjectionType.Custom)
            {
                currentInterjectionCharacter = (InterjectionCharacter)EditorGUILayout.EnumPopup("Interjection Character", currentInterjectionCharacter);
                futurelines[2] = currentInterjectionCharacter.ToString();
            }
            else
            {
                futurelines[2] = EditorGUILayout.TextField("Prefab Name", futurelines[2]);
            }
            stopMusic = EditorGUILayout.Toggle("Stop Music", stopMusic);
            futurelines[3] = stopMusic.ToString();
            futurelines[5] = EditorGUILayout.TextField("Next Frame", futurelines[5]);
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 4)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[CameraTween]";
            futurelines[1] = EditorGUILayout.TextField("Angle to Tween to", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Tween Time", futurelines[2]);

            futurelines[5] = EditorGUILayout.TextField("Next Frame", futurelines[5]);
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 5)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[EvidenceEnter]";
            futurelines[1] = EditorGUILayout.TextField("Evidence Name", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Text", futurelines[2]);
            currentEvOpType = (EvidenceOperationType)EditorGUILayout.EnumPopup("Slide-In Type", currentEvOpType);
            futurelines[3] = currentEvOpType.ToString();
            string caption = "";
            if (currentEvOpType == EvidenceOperationType.Add)
            {
                caption = "Details Prefab Name";

            }
            else
            {
                caption = "Updated description";
            }
            futurelines[4] = EditorGUILayout.TextField(caption, futurelines[4]);
            futurelines[5] = EditorGUILayout.TextField("Next Frame", futurelines[5]);
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 6)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[Choice]";
            futurelines[1] = EditorGUILayout.TextField("Choice 1 Text", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Choice 2 Text", futurelines[2]);
            futurelines[3] = EditorGUILayout.TextField("Choice 3 Text", futurelines[3]);
            futurelines[4] = EditorGUILayout.TextField("Choice 1 FRM", futurelines[4]);

            futurelines[5] = EditorGUILayout.TextField("Choice 2 FRM", futurelines[5]);
            futurelines[6] = EditorGUILayout.TextField("Choice 3 FRM", futurelines[6]);
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 7)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[Present]";
            futurelines[1] = EditorGUILayout.TextField("Wrong FRM", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Right FRM", futurelines[2]);
            futurelines[3] = "1";
            futurelines[4] = EditorGUILayout.TextField("Correct evidence name", futurelines[4]);
            presentType = (EvidenceSelect.selecttype)EditorGUILayout.EnumPopup("Present Type", presentType);
            futurelines[5] = presentType.ToString();
          
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 8)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[PointInPicture]";
            futurelines[1] = EditorGUILayout.TextField("Wrong FRM", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Right FRM", futurelines[2]);
            futurelines[3] = EditorGUILayout.TextField("Correct area name", futurelines[3]);
            futurelines[4] = EditorGUILayout.TextField("Question", futurelines[4]);
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
            futurelines[25] = additionalevent.ToString().Replace("None", "");
            additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
            for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }

        if (mode == 11)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            spf = (SpecialFRMS)EditorGUILayout.EnumPopup("Special FRM Type", spf);
            futurelines[0] = "[" + spf.ToString() + "]";
            lineslength = EditorGUILayout.IntField("Lines length", lineslength);
            for (int i = 1; i < lineslength + 1; i++) {

                futurelines[i] = EditorGUILayout.TextField("Line #" + i, futurelines[i]);

            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }

        }

            if (mode == 10)
            {
                if (futurelines == null)
                {
                    futurelines = new List<string>();
                    for (int i = 0; i < 29; i++)
                    {
                        futurelines.Add("");
                    }

                }
                futurelines[0] = "[PointIn3dExamine]";
                futurelines[1] = EditorGUILayout.TextField("Wrong FRM", futurelines[1]);
                futurelines[2] = EditorGUILayout.TextField("Right FRM", futurelines[2]);
                futurelines[3] = EditorGUILayout.TextField("Correct area name", futurelines[3]);

                futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
                additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
                futurelines[25] = additionalevent.ToString().Replace("None", "");
                additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
                for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
                {
                    futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
                }
                FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
                if (GUILayout.Button("Create FRM File"))
                {
                    string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    StreamWriter writer = new StreamWriter(path);
                    foreach (string line in futurelines)
                    {
                        writer.WriteLine(line);
                    }
                    writer.Close();
                    Debug.Log("Created FRM File at " + path);
                    AssetDatabase.Refresh();


                }
                if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 9)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }

            }
            futurelines[0] = "[CrossExaminationStart]";
          
            futurelines[5] = EditorGUILayout.TextField("Next Frame", futurelines[5]);

            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            futurelines[10] = EditorGUILayout.TextField("Hint FRM", futurelines[10]);

           
           statementcount = EditorGUILayout.IntField("Statement Number", statementcount);
            for (int i = 11; i < (10 + statementcount + 1); i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Statement " + (i - 10).ToString(), futurelines[i]);
            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }
        if (mode == 2)
        {
           for (int i = 0; i < futurelines.Count; i++)
            {
                futurelines[i] = EditorGUILayout.TextField("Line #" + i, futurelines[i]);
            }

           if (GUILayout.Button("Add line"))
            {
                futurelines.Add("");
            }

            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
            }
        }

        if (mode == 1)
        {
            if (futurelines == null)
            {
                futurelines = new List<string>();
                for (int i = 0; i < 29; i++)
                {
                    futurelines.Add("");
                }
            }
            futurelines[0] = EditorGUILayout.TextField("Camera Angle", futurelines[0]);
            futurelines[1] = EditorGUILayout.TextField("Speaking Character", futurelines[1]);
            futurelines[2] = EditorGUILayout.TextField("Animation Name", futurelines[2]);
            usedcolor = (TextColor)EditorGUILayout.EnumPopup("Main Text Color", usedcolor);
            futurelines[3] = usedcolor.ToString();
            futurelines[4] = EditorGUILayout.TextField("Text", futurelines[4]);
            futurelines[5] = EditorGUILayout.TextField("Next Frame", futurelines[5]);
            usedblip = (Blip)EditorGUILayout.EnumPopup("Text Blip", usedblip);
            futurelines[6] = usedblip.ToString();
            futurelines[7] = EditorGUILayout.TextField("Music Change", futurelines[7]);
            usedalignment = (Alignment)EditorGUILayout.EnumPopup("Text Alignment", usedalignment);
            futurelines[8] = usedalignment.ToString();
            futurelines[9] = EditorGUILayout.TextField("Letter Id to Play SE", futurelines[9]);
            futurelines[10] = EditorGUILayout.TextField("SE Name", futurelines[10]);
            autoproceed = EditorGUILayout.Toggle("Auto Proceed", autoproceed);
            if (autoproceed)
            {
                futurelines[12] = "Auto";
            }
            thispart = (PartOfMechanic)EditorGUILayout.EnumPopup("Part of Mechanic", thispart);
            if (thispart != PartOfMechanic.None)
            {
                futurelines[24] = "{" + thispart.ToString() + "}";
            }

            if (thispart != PartOfMechanic.CrossExamination)
            {
                additionalevent = (Line26Command)EditorGUILayout.EnumPopup("Additional Event", additionalevent);
                futurelines[25] = additionalevent.ToString().Replace("None", "");
                additionaleventdatanum = EditorGUILayout.IntField("Additional Event Data Size", additionaleventdatanum);
                for (int i = 26; i < (25 + additionaleventdatanum + 1); i++)
                {
                    futurelines[i] = EditorGUILayout.TextField("Additional Event Data Line " + (i - 25).ToString(), futurelines[i]);
                }
            }
            else
            {
                futurelines[25] = "Statement";
                futurelines[26] = EditorGUILayout.TextField("Press FRM", futurelines[26]);
                objectionable = EditorGUILayout.Toggle("Objectionable", objectionable);
                if (objectionable)
                {
                    futurelines[27] = EditorGUILayout.TextField("Correct Evidence", futurelines[27]);
                    futurelines[28] = EditorGUILayout.TextField("Correct Evidence 2", futurelines[28]);
                    futurelines[29] = EditorGUILayout.TextField("Wrong FRM", futurelines[29]);
                    futurelines[30] = EditorGUILayout.TextField("Right FRM", futurelines[30]);
                }
                else
                {
                    futurelines[27] = "NonObjectionable";
                    futurelines[29] = EditorGUILayout.TextField("Wrong FRM", futurelines[29]);
                }

            }
            FRMId = EditorGUILayout.IntField("FRM Id", FRMId);
            if (GUILayout.Button("Create FRM File"))
            {
                string path = Path.Combine(Application.streamingAssetsPath, GameObject.FindObjectOfType<ModifyStrings>().CaseName, GameObject.FindObjectOfType<ModifyStrings>().StoryName, FRMId.ToString() + ".frm");

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                StreamWriter writer = new StreamWriter(path);
                foreach (string line in futurelines)
                {
                    writer.WriteLine(line);
                }
                writer.Close();
                Debug.Log("Created FRM File at " + path);
                AssetDatabase.Refresh();


            }
            if (GUILayout.Button("Back"))
            {
                mode = 0;
                futurelines.Clear();
            }



            }
    
    
    }


    }
#endif