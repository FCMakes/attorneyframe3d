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

    [MenuItem("FCTools/UNCombine")]
    static void UNCombineMeshes()
    {
        float MeshQ = 0;
        MeshFilter[] Meshes = Resources.FindObjectsOfTypeAll<MeshFilter>();
        foreach (MeshFilter mesh in Meshes)
        {

            if (mesh.gameObject.GetComponent<MeshCollider>() && mesh.sharedMesh != mesh.gameObject.GetComponent<MeshCollider>().sharedMesh)
            {
                mesh.sharedMesh = mesh.gameObject.GetComponent<MeshCollider>().sharedMesh;
                MeshQ += 1f;
            }


        }



        Debug.Log("Uncombined " + MeshQ.ToString() + " meshes");
    }



    [MenuItem("FCTools/UNCombine Method 2")]
    static void UNCombineMeshes2()
    {
        float MeshQ = 0;
        GameObject[] Meshes = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject meshholder in Meshes)
        {

            if (meshholder.GetComponent<MeshFilter>() && AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Mesh/" + meshholder.name + ".asset") && meshholder.GetComponent<MeshFilter>().sharedMesh != AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Mesh/" + meshholder.name + ".asset"))
            {
                MeshFilter mesh = meshholder.GetComponent<MeshFilter>();
                mesh.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Mesh/" + meshholder.name + ".asset");
                MeshQ += 1f;

            }
        }


        Debug.Log("Uncombined " + MeshQ.ToString() + " meshes");
    }

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

    [MenuItem("FCTools/See asset type")]
    static void Tell()
    {



        Debug.Log(Selection.activeObject.GetType());
        Debug.Log(AssetDatabase.GetAssetPath(Selection.activeObject));
    }


    [MenuItem("FCTools/Assign event")]
    static void assign()
    {

        Selection.activeGameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameObject.FindObjectOfType<SaveLoadController>().ShowConfirmation(Selection.activeGameObject.GetComponent<UnityEngine.UI.Button>()));



    }
    public static void SetTextureImporterFormat(Texture2D texture, bool isReadable)
    {
        if (null == texture) return;

        string assetPath = AssetDatabase.GetAssetPath(texture);
        var tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

       
        if (tImporter != null)
        {
            

            tImporter.isReadable = isReadable;
            Debug.Log("did shit");
            AssetDatabase.ImportAsset(assetPath);
            AssetDatabase.Refresh();
        }
    }
    public static void AddDescendantsWithTag(Transform parent, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
          
                list.Add(child.gameObject);
            AddDescendantsWithTag(child, list);
            
          
        }
    }
    [MenuItem("FCTools/CreateDir")]
    static void CreateDirectory()
    {
        Directory.CreateDirectory(Path.Combine(Application.persistentDataPath, "Map1"));
        Debug.Log("Created");
    }

    [MenuItem("FCTools/CreateCS")]
    static void CreateCS()
    {
        string pathmain = "Assets/";

      

        StreamWriter writer1 = new StreamWriter(Path.Combine(pathmain, "MadeByScript.cs"), true);
        writer1.WriteLine("using UnityEngine;");
        writer1.WriteLine("public class MadeByScript : MonoBehaviour");
        writer1.WriteLine("{");
        writer1.WriteLine("public void Start()");
        writer1.WriteLine("{");
        writer1.WriteLine("Debug.Log(\"Script created, lol\");");
        writer1.WriteLine("}");
        writer1.WriteLine("}");
        writer1.Close();

    }
    [MenuItem("FCTools/Camera Thing")]
    static void CameraDo()
    {

        Selection.activeGameObject.GetComponent<Camera>().transform.SetPositionAndRotation(Camera.current.transform.position, Camera.current.transform.rotation);

    }

    [MenuItem("FCTools/ShowRotation")]
    static void RotationShow()
    {

        Debug.Log(Selection.activeGameObject.transform.eulerAngles.y);

    }

    [MenuItem("FCTools/MoveTest")]
    static void EditAnimation()
    {

        if (Selection.activeGameObject.GetComponent<Animation>())
        {
            AnimationCurve curve = AnimationCurve.Linear(0.0F, 0.0F, 0.0F, 0.0F);
            Selection.activeGameObject.GetComponent<Animation>().clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        }
     
        

    }

    [MenuItem("FCTools/Fix Name")]
    static void Namefix()
    {
        foreach (Object obj in Selection.objects)
        {
            string source = obj.name;
            int count = source.Split('1').Length;
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(obj), "Anim" + count);

        }



    }
   

    [MenuItem("FCTools/Down")]
    static void down()
    {

       GameObject newgobj = Instantiate(Selection.activeGameObject, Selection.activeGameObject.transform.parent);
        Vector3 diff = newgobj.transform.parent.GetChild(4).localPosition - newgobj.transform.parent.GetChild(3).localPosition;
        newgobj.transform.localPosition += diff;
        int buttonnum = int.Parse(newgobj.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text) + 1;
        newgobj.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = buttonnum.ToString();
        newgobj.name = "Slot" + buttonnum;


    }


    [MenuItem("FCTools/Add Camera Angle")]
    static void addangle()
    {
        Transform t = Selection.activeGameObject.transform;
        MainCameraController mcc = GameObject.FindObjectOfType<MainCameraController>();
        MainCameraController.CameraAngle Angle = new MainCameraController.CameraAngle();
        Angle.name = t.gameObject.name;
        Angle.position = t.position;
        Angle.rotation = t.rotation;

        mcc.AddAngle(Angle);




    }

    [MenuItem("FCTools/Fix Font Batch")]
    static void FontFix()
    {
        foreach (GameObject go in Selection.gameObjects)
        {
            foreach (Text txt in go.GetComponentsInChildren<Text>())
            {
                txt.font = GameObject.Find("TW_MS_Regular").GetComponent<Text>().font;
                txt.fontSize += 3;
            }
        }
    }
   


    [MenuItem("FCTools/GetCurves")]
    static void curves()
    {
      foreach (UnityEditor.AnimationClipCurveData acd in AnimationUtility.GetAllCurves((AnimationClip)Selection.activeObject))
        {
            Debug.Log(acd.path);
            Debug.Log(acd.propertyName);
            Debug.Log(acd.type);
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

        ScreenCapture.CaptureScreenshot(Path.Combine(@"C:/Users/User/Documents/oCam", "Screenshot-" + System.DateTime.Now.ToString().Replace(":", ".").Replace(".", "-").Replace(" ", "-") + ".png"));
        Debug.Log("Saved screenshot to " + Path.Combine(@"C:/Users/User/Documents/oCam", "Screenshot-" + System.DateTime.Now.ToString().Replace(":", ".").Replace(".", "-").Replace(" ", "-").Replace("/", "-") + ".png"));

    }

    [MenuItem("FCTools/Fix Mesh Colliders")]
    static void FixColliders()
    {

      foreach (GameObject go in Selection.gameObjects)
        {
            go.GetComponent<MeshCollider>().sharedMesh = go.GetComponent<SkinnedMeshRenderer>().sharedMesh;
        }

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

    [MenuItem("FCTools/Set Evidence Positions")]
    static void setevpos()
    {
        Record rec = GameObject.FindObjectOfType<Record>();
        rec.Ev1Pos = FCServices.FindChildWithName(rec.gameObject, rec.EvidenceList[0].name).transform.localPosition;
        rec.Ev2Pos = FCServices.FindChildWithName(rec.gameObject, rec.EvidenceList[1].name).transform.localPosition;
        rec.Ev3Pos = FCServices.FindChildWithName(rec.gameObject, rec.EvidenceList[2].name).transform.localPosition;
        rec.Ev4Pos = FCServices.FindChildWithName(rec.gameObject, rec.EvidenceList[3].name).transform.localPosition;
        rec.Ev5Pos = FCServices.FindChildWithName(rec.gameObject, rec.EvidenceList[4].name).transform.localPosition;

    }

    [MenuItem("FCTools/Fix Name 3")]
    static void Namefix3()
    {
        foreach (Object obj in Selection.objects)
        {
           
            string source = obj.name;
            string str = source.Substring(7, 3);
            int it = int.Parse(str);
         
            AssetDatabase.MoveAsset(AssetDatabase.GetAssetPath(obj), Path.Combine("Assets", "temp", "res","a.png"));
                AssetDatabase.RenameAsset(Path.Combine("Assets", "temp", "res", "a.png"), "Thing2000" + (17 - it).ToString());
              
               
            

        }



    }

}
#endif