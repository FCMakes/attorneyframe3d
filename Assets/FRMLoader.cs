#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class FRMLoader : EditorWindow
{
    int toload;
    string totween;
    int tosave;
    int slottoload;
    int secondarycamtoload;
    string charname1;
    string charname2;
    float offesttorot;
    float rottime;
    string toplay;

    [MenuItem("Window/FRM Loader")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        FRMLoader window = (FRMLoader)EditorWindow.GetWindow(typeof(FRMLoader));
        window.Show();
    }

    void OnGUI()
    {
        toload = EditorGUILayout.IntField("FRM to Load", toload);
        if (GUILayout.Button("Load"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().LoadFromFRM(toload);
        }
        totween = EditorGUILayout.TextField("Camera Tween Test", totween);
        if (GUILayout.Button("Select"))
        {
            GameObject.FindObjectOfType<MainCameraController>().SelectAngle(totween);
        }
        if (GUILayout.Button("Tween"))
        {
            GameObject.FindObjectOfType<MainCameraController>().TweenAngle(totween, 0.5f);
        }

        toplay = EditorGUILayout.TextField("Music to Play", toplay);
        if (GUILayout.Button("Play"))
        {
            MusicController mc = GameObject.FindObjectOfType<MusicController>();
            mc.gameObject.GetComponent<AudioSource>().clip = mc.FindTrack(toplay);
            mc.gameObject.GetComponent<AudioSource>().time = 0f;
           mc.gameObject.GetComponent<AudioSource>().Play();
        }

        tosave = EditorGUILayout.IntField("Slot to save to", tosave);
        if (GUILayout.Button("Save"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().Save(tosave);
        }
        slottoload = EditorGUILayout.IntField("Slot to load from", slottoload);
        if (GUILayout.Button("Load"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().LoadFromSlot(slottoload);
        }
        if (GUILayout.Button("Clear Load Key"))
        {
            PlayerPrefs.DeleteKey("SlotToLoad");
            PlayerPrefs.Save();
        }
        secondarycamtoload = EditorGUILayout.IntField("Secondary cam to save angle to", secondarycamtoload);
        if (GUILayout.Button("Save"))
        {
            Transform t = Selection.activeGameObject.transform;
            SecondaryCameraController mcc = GameObject.FindObjectsOfType<SecondaryCameraController>()[secondarycamtoload];
            MainCameraController.CameraAngle Angle = new MainCameraController.CameraAngle();
            Angle.name = t.gameObject.name;
            Angle.position = t.position;
            Angle.rotation = t.rotation;

            mcc.AddAngle(Angle);
        }
        charname1 = EditorGUILayout.TextField("Victim", charname1);
        charname2 = EditorGUILayout.TextField("Target", charname2);
        offesttorot = EditorGUILayout.FloatField("Rotate Offset", offesttorot);
        rottime = EditorGUILayout.FloatField("Rotate Time", rottime);
        if (GUILayout.Button("Look at"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().CharacterLookAt(charname1, charname2, offesttorot);
        }
        if (GUILayout.Button("Look at Tween"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().CharacterLookAtTween(charname1, charname2, offesttorot, rottime);
        }
        if (GUILayout.Button("Back to original rotation"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().CharacterToOriginalRotation(charname1);
        }
        if (GUILayout.Button("Back to original rotation Tween"))
        {
            GameObject.FindObjectOfType<ModifyStrings>().CharacterToOriginalRotationTween(charname1, rottime);
        }

    }
}
#endif