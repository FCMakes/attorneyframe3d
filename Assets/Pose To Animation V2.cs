#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
using UnityEditor.Animations;

public class PoseToAnimationV2 : EditorWindow
{
    public bool pose2animstart;
    public bool addtoanimator;
    public Animator toaddto;
    public string customnewpath;
    public Transform poseholder;
    public AnimationClip animtoaddto;
    public float timeframe;
    // Start is called before the first frame update
    [MenuItem("Window/Pose to Animation V2")]
    static void Init()
    {
       
        // Get existing open window or if none, make a new one:
        PoseToAnimationV2 window = (PoseToAnimationV2)EditorWindow.GetWindow(typeof(PoseToAnimationV2));
       
        window.Show();
        window.Start();

    }

    private void Start()
    {
        pose2animstart = true;
    }
    private void OnGUI()
    {
        customnewpath = EditorGUILayout.TextField("Desired name/path", customnewpath);
        pose2animstart = EditorGUILayout.Toggle("Pose to animation start", pose2animstart);
        addtoanimator = EditorGUILayout.Toggle("Add animation to animator", addtoanimator);
        if (addtoanimator)
        {
            toaddto = (Animator)EditorGUILayout.ObjectField(toaddto, typeof(Animator));
        }
        if (GUILayout.Button("Import anim from FBX and set pose to start"))
        {
            string filepath = EditorUtility.OpenFilePanel("Choose the FBX file to import from", "", "fbx");
            string newpath = Path.Combine(Application.dataPath,  filepath.Split(char.Parse(@"/"))[filepath.Split(char.Parse(@"/")).Length - 1]);
            File.Copy(filepath, newpath);
            AssetDatabase.Refresh();
            Object[] allassets = AssetDatabase.LoadAllAssetsAtPath(Path.Combine("Assets", filepath.Split(char.Parse(@"/"))[filepath.Split(char.Parse(@"/")).Length - 1]));
           

            foreach (Object ob in allassets)
            {

                if (ob.GetType() == typeof(AnimationClip))
                {

                    AnimationClip newclip = new AnimationClip();
                    EditorUtility.CopySerialized((AnimationClip)ob, newclip);

                    if (customnewpath != "")
                    {
                        AssetDatabase.CreateAsset(newclip, Path.Combine("Assets", customnewpath) + ".anim");
                    }
                
                    else
                    {
                        AssetDatabase.CreateAsset(newclip, Path.Combine("Assets", filepath.Split(char.Parse(@"/"))[filepath.Split(char.Parse(@"/")).Length - 1].Replace(".FBX", ".anim")));
                    }
                }
            }
            AnimationClip ac = new AnimationClip();
            if (customnewpath == "")
            {
                ac = AssetDatabase.LoadAssetAtPath<AnimationClip>(Path.Combine("Assets", filepath.Split(char.Parse(@"/"))[filepath.Split(char.Parse(@"/")).Length - 1].Replace(".FBX", ".anim")));
            }
            else
            {
                ac = AssetDatabase.LoadAssetAtPath<AnimationClip>(Path.Combine("Assets", customnewpath) + ".anim");
            }
            if (pose2animstart)
            {
               
                Transform tf = Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine("Assets", filepath.Split(char.Parse(@"/"))[filepath.Split(char.Parse(@"/")).Length - 1]))).transform;
                Pose2Anim.DoPose2AnimPublic(ac, tf);

                DestroyImmediate(tf.gameObject);
            }
            File.Delete(newpath);
            if (addtoanimator)
            {
                var runtimeController = toaddto.runtimeAnimatorController;

                var controller = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.Animations.AnimatorController>(UnityEditor.AssetDatabase.GetAssetPath(runtimeController));

                AnimatorState anst = controller.layers[0].stateMachine.AddState(ac.name);
                anst.motion = ac;
                anst.name = ac.name;

               
            
            }
            AssetDatabase.Refresh();
           
            

        }

        GUILayout.Label("Advanced");
        poseholder = (Transform)EditorGUILayout.ObjectField(poseholder, typeof(Transform));
        animtoaddto = (AnimationClip)EditorGUILayout.ObjectField(animtoaddto, typeof(AnimationClip));
        timeframe = EditorGUILayout.FloatField("Time Frame", timeframe);

        if (GUILayout.Button("Pose to Animation Time Frame"))
        {
            Pose2Anim.DoPose2AnimPublicTimeFrame(animtoaddto, poseholder, timeframe);
        }




    }
}
#endif