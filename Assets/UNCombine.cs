#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using UnityEngine.UI;

public class UNCombine : MonoBehaviour
{

  
    [MenuItem("GameObject/FCTools/Hide untextured (AA)", false, 49)]
    static void DeleteOutline()
    {
        float KillQ = 0;

        foreach (Transform obj in Selection.activeGameObject.transform)
        {
            if (obj.gameObject.activeSelf)
            {
                if (obj.gameObject.GetComponent<MeshRenderer>() && obj.gameObject.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("Default-Material") || obj.gameObject.GetComponent<SkinnedMeshRenderer>() && obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("Default-Material") || obj.gameObject.GetComponent<MeshRenderer>() && obj.gameObject.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("No Name") || obj.gameObject.GetComponent<SkinnedMeshRenderer>() && obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("No Name"))
                {
                    obj.gameObject.SetActive(false);
                    KillQ += 1;
                }
            }

        }
        Debug.Log("Hid " + KillQ.ToString() + " untextured objects");
    }
    [MenuItem("GameObject/FCTools/Show untextured (AA)", false, 49)]

    static void ShowOutline()
    {
        float KillQ = 0;

        foreach (Transform obj in Selection.activeGameObject.transform)
        {
            if (!obj.gameObject.activeSelf)
            {
                if (obj.gameObject.GetComponent<MeshRenderer>() && obj.gameObject.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("Default-Material") || obj.gameObject.GetComponent<SkinnedMeshRenderer>() && obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("Default-Material") || obj.gameObject.GetComponent<MeshRenderer>() && obj.gameObject.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("No Name") || obj.gameObject.GetComponent<SkinnedMeshRenderer>() && obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("No Name"))
                {
                    obj.gameObject.SetActive(true);
                    KillQ += 1;
                }
            }

        }
        Debug.Log("Shown " + KillQ.ToString() + " untextured objects");



    }
  
    [MenuItem("GameObject/FCTools/Remove Transform Parent", false, 49)]
    static void RemoveTr()
    {

        Selection.activeGameObject.transform.SetParent(null);

        
    }

    
    public static void AddDescendantsWithTag(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
          
                list.Add(child.gameObject);
            AddDescendantsWithTag(child, list);
            
          
        }
    }
   

    [MenuItem("FCTools/Create Fade In Animation")]
    static void FadeIn()
    {
        AnimationClip clip = new AnimationClip();
        
        foreach (Transform child in Selection.activeGameObject.transform)
        {
            if (child.gameObject.GetComponent<SkinnedMeshRenderer>() && !child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("ol"))
            {
                

                var curvebinding = new EditorCurveBinding();
                curvebinding.propertyName = "material._Color.a";
                curvebinding.path = child.gameObject.name;
                curvebinding.type = typeof(SkinnedMeshRenderer);
                var acurve = new AnimationCurve();

                acurve.AddKey(new Keyframe(0f, 0f));
                acurve.AddKey(new Keyframe(1f, 1f));
                AnimationUtility.SetEditorCurve(clip, curvebinding, acurve);

                var curvebinding2 = new EditorCurveBinding();
                curvebinding2.propertyName = "material._Color.r";
                curvebinding2.path = child.gameObject.name;
                curvebinding2.type = typeof(SkinnedMeshRenderer);
                var acurve2 = new AnimationCurve();

                acurve2.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r));
                acurve2.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r));
                AnimationUtility.SetEditorCurve(clip, curvebinding2, acurve2);

                var curvebinding3 = new EditorCurveBinding();
                curvebinding3.propertyName = "material._Color.g";
                curvebinding3.path = child.gameObject.name;
                curvebinding3.type = typeof(SkinnedMeshRenderer);
                var acurve3 = new AnimationCurve();

                acurve3.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g));
                acurve3.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g));
                AnimationUtility.SetEditorCurve(clip, curvebinding3, acurve3);

                var curvebinding4 = new EditorCurveBinding();
                curvebinding4.propertyName = "material._Color.b";
                curvebinding4.path = child.gameObject.name;
                curvebinding4.type = typeof(SkinnedMeshRenderer);
                var acurve4 = new AnimationCurve();

                acurve4.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b));
                acurve4.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b));
                AnimationUtility.SetEditorCurve(clip, curvebinding4, acurve4);
            }
            if (child.gameObject.GetComponent<SkinnedMeshRenderer>() && child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("ol"))
            {
                var curvebinding = new EditorCurveBinding();
                curvebinding.propertyName = "m_Enabled";
                curvebinding.path = child.gameObject.name;
                curvebinding.type = typeof(SkinnedMeshRenderer);
                var acurve = new AnimationCurve();

                acurve.AddKey(new Keyframe(0f, 0f));
                acurve.AddKey(new Keyframe(59f / 60f, 0f));
                acurve.AddKey(new Keyframe(1f, 1f));
                AnimationUtility.SetEditorCurve(clip, curvebinding, acurve);

            }

            }

        AssetDatabase.CreateAsset(clip, Path.Combine("Assets", Selection.activeGameObject.name + "FadeIn.anim"));




    }

    [MenuItem("FCTools/Create Fade Out Animation")]
    static void FadeOut()
    {
        AnimationClip clip = new AnimationClip();

        foreach (Transform child in Selection.activeGameObject.transform)
        {
            if (child.gameObject.GetComponent<SkinnedMeshRenderer>() && !child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("ol"))
            {


                var curvebinding = new EditorCurveBinding();
                curvebinding.propertyName = "material._Color.a";
                curvebinding.path = child.gameObject.name;
                curvebinding.type = typeof(SkinnedMeshRenderer);
                var acurve = new AnimationCurve();

                acurve.AddKey(new Keyframe(0f, 1f));
                acurve.AddKey(new Keyframe(1f, 0f));
                AnimationUtility.SetEditorCurve(clip, curvebinding, acurve);

                var curvebinding2 = new EditorCurveBinding();
                curvebinding2.propertyName = "material._Color.r";
                curvebinding2.path = child.gameObject.name;
                curvebinding2.type = typeof(SkinnedMeshRenderer);
                var acurve2 = new AnimationCurve();

                acurve2.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r));
                acurve2.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.r));
                AnimationUtility.SetEditorCurve(clip, curvebinding2, acurve2);

                var curvebinding3 = new EditorCurveBinding();
                curvebinding3.propertyName = "material._Color.g";
                curvebinding3.path = child.gameObject.name;
                curvebinding3.type = typeof(SkinnedMeshRenderer);
                var acurve3 = new AnimationCurve();

                acurve3.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g));
                acurve3.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.g));
                AnimationUtility.SetEditorCurve(clip, curvebinding3, acurve3);

                var curvebinding4 = new EditorCurveBinding();
                curvebinding4.propertyName = "material._Color.b";
                curvebinding4.path = child.gameObject.name;
                curvebinding4.type = typeof(SkinnedMeshRenderer);
                var acurve4 = new AnimationCurve();

                acurve4.AddKey(new Keyframe(1f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b));
                acurve4.AddKey(new Keyframe(0f, child.gameObject.GetComponent<SkinnedMeshRenderer>().material.color.b));
                AnimationUtility.SetEditorCurve(clip, curvebinding4, acurve4);
            }
            if (child.gameObject.GetComponent<SkinnedMeshRenderer>() && child.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial.name.Contains("ol"))
            {
                var curvebinding = new EditorCurveBinding();
                curvebinding.propertyName = "m_Enabled";
                curvebinding.path = child.gameObject.name;
                curvebinding.type = typeof(SkinnedMeshRenderer);
                var acurve = new AnimationCurve();

                acurve.AddKey(new Keyframe(0f, 1f));
                acurve.AddKey(new Keyframe(1f / 60f, 0f));
                acurve.AddKey(new Keyframe(1f, 0f));
                AnimationUtility.SetEditorCurve(clip, curvebinding, acurve);

            }

        }

        AssetDatabase.CreateAsset(clip, Path.Combine("Assets", Selection.activeGameObject.name + "FadeOut.anim"));




    }

    [MenuItem("FCTools/Screenshot")]
    static void Capture()
    {

        ScreenCapture.CaptureScreenshot(Path.Combine(Application.dataPath, "Screenshots", "Screenshot-" + System.DateTime.Now.ToString().Replace(":", ".").Replace(".", "-").Replace(" ", "-") + ".png").Replace(@"/", @"\"), 1);
        Debug.Log("Saved screenshot to " + Path.Combine(Application.dataPath, "Screenshots", "Screenshot-" + System.DateTime.Now.ToString().Replace(":", ".").Replace(".", "-").Replace(" ", "-").Replace("/", "-") + ".png").Replace(@"/", @"\"));

    }

    
    [MenuItem("Assets/Add to Prefab Controller")]
    static void toprefabcontroller()
    {
        PrefabController pc = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/PrefabController.prefab").GetComponent<PrefabController>();
        pc.Prefabs.AddRange(Selection.gameObjects);
        PrefabUtility.RecordPrefabInstancePropertyModifications(pc);
        PrefabUtility.SavePrefabAsset(pc.gameObject);
        AssetDatabase.Refresh();
    }
    [MenuItem("GameObject/FCTools/Copy properties (AA)", false, 49)]

    static void CopyProperties()
    {
        GameObject Origin;
        GameObject To;
        if (FCServices.FindChildWithName(Selection.gameObjects[0], "mixamorig:Hips"))
        {
            Origin = Selection.gameObjects[1];
            To = Selection.gameObjects[0];
        }
        else
        {
            Origin = Selection.gameObjects[0];
            To = Selection.gameObjects[1];
        }

        foreach (Transform obj in To.transform)
        {
            if (FCServices.FindChildWithName(Origin, obj.gameObject.name))
            {


                obj.gameObject.SetActive(FCServices.FindChildWithName(Origin, obj.gameObject.name).activeSelf);


                obj.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterial = FCServices.FindChildWithName(Origin, obj.gameObject.name).GetComponent<MeshRenderer>().sharedMaterial;



            }

        }
    }

   

}
#endif