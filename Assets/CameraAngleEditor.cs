#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraAngleEditor : EditorWindow
{
    string AngleName;
    Camera source1;
    Camera source2;
    Camera source3;
    // Start is called before the first frame update
    [MenuItem("Window/Camera Angle Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CameraAngleEditor window = (CameraAngleEditor)EditorWindow.GetWindow(typeof(CameraAngleEditor));
        window.Show();
    }

    void OnGUI()
    {
        AngleName = EditorGUILayout.TextField("Angle to Edit", AngleName);
        GUILayout.Label("Background camera angle holder");
        source1 = (Camera)EditorGUILayout.ObjectField(source1, typeof(Camera));
        GUILayout.Label("Character camera angle holder");
        source2 = (Camera)EditorGUILayout.ObjectField(source2, typeof(Camera));
        GUILayout.Label("Foreground camera angle holder");
        source3 = (Camera)EditorGUILayout.ObjectField(source3, typeof(Camera));
        if (GUILayout.Button("Save changes"))
        {
            if (source1 != null)
            {
                SecondaryCameraController toedit = FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<SecondaryCameraController>();
                if (toedit.WithName(AngleName) == null)
                {
                    MainCameraController.CameraAngle toadd = new MainCameraController.CameraAngle();
                    toadd.name = AngleName;
                    toadd.position = source1.gameObject.transform.position;
                    toadd.rotation = source1.gameObject.transform.rotation;
                    toedit.AddAngle(toadd);
                }
                else
                {
                    toedit.WithName(AngleName).position = source1.gameObject.transform.position;
                    toedit.WithName(AngleName).rotation = source1.gameObject.transform.rotation;
                }
            }

            if (source2 != null)
            {
                MainCameraController toedit = FCServices.FindChildWithName("Cameras", "MainCamera").GetComponent<MainCameraController>();
                if (toedit.WithName(AngleName) == null)
                {
                    MainCameraController.CameraAngle toadd = new MainCameraController.CameraAngle();
                    toadd.name = AngleName;
                    toadd.position = source2.gameObject.transform.position;
                    toadd.rotation = source2.gameObject.transform.rotation;
                    toedit.AddAngle(toadd);
                }
                else
                {
                    toedit.WithName(AngleName).position = source2.gameObject.transform.position;
                    toedit.WithName(AngleName).rotation = source2.gameObject.transform.rotation;
                }
            }
            if (source3 != null)
            {
                SecondaryCameraController toedit = FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<SecondaryCameraController>();
                if (toedit.WithName(AngleName) == null)
                {
                    MainCameraController.CameraAngle toadd = new MainCameraController.CameraAngle();
                    toadd.name = AngleName;
                    toadd.position = source3.gameObject.transform.position;
                    toadd.rotation = source3.gameObject.transform.rotation;
                    toedit.AddAngle(toadd);
                }
                else
                {
                    toedit.WithName(AngleName).position = source3.gameObject.transform.position;
                    toedit.WithName(AngleName).rotation = source3.gameObject.transform.rotation;
                }
            }
        }

        if (GUILayout.Button("Delete Angle"))
        {
            FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<SecondaryCameraController>().AvailableAngles.Remove(FCServices.FindChildWithName("Cameras", "BGCamera").GetComponent<SecondaryCameraController>().WithName(AngleName));
            FCServices.FindChildWithName("Cameras", "MainCamera").GetComponent<MainCameraController>().AvailableAngles.Remove(FCServices.FindChildWithName("Cameras", "MainCamera").GetComponent<MainCameraController>().WithName(AngleName));
            FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<SecondaryCameraController>().AvailableAngles.Remove(FCServices.FindChildWithName("Cameras", "DeskCamera").GetComponent<SecondaryCameraController>().WithName(AngleName));

        }

    }

    }
#endif