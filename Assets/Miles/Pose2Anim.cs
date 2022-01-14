#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Pose2Anim : MonoBehaviour
{

    [MenuItem("FCTools/Pose to Animation Start")]
    static void DoPose2Anim()
    {
        AnimationClip edited = Selection.activeGameObject.GetComponent<Animator>().runtimeAnimatorController.animationClips[0];
        Pose2Anim.SetAllTransformCurvesAt0(Selection.activeGameObject.transform.GetChild(0).name, Selection.activeGameObject.transform.GetChild(0), edited);

        Transform[] childrenofbone255 = Selection.activeGameObject.transform.GetChild(0).gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform ch in childrenofbone255)
        {
            Transform[] parents = ch.gameObject.GetComponentsInParent<Transform>();
            System.Array.Reverse(parents);
            string relativepathf = "";
            foreach (Transform p in parents)
            {
                if (p.gameObject != Selection.activeGameObject)
                {
                    relativepathf += p.name;
                    relativepathf += "/";

                }
            }
            relativepathf = relativepathf.Substring(0, relativepathf.Length - 1);
            Pose2Anim.SetAllTransformCurvesAt0(relativepathf, ch, edited);
        }
        Debug.Log("Sucessfully added pose to animation start.");






    }

    public static void DoPose2AnimPublic(AnimationClip toedit, Transform poseparent)
    {
        AnimationClip edited = toedit;
        Pose2Anim.SetAllTransformCurvesAt0(poseparent.GetChild(0).name, poseparent.GetChild(0), edited);

        Transform[] childrenofbone255 = poseparent.GetChild(0).gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform ch in childrenofbone255)
        {
            Transform[] parents = ch.gameObject.GetComponentsInParent<Transform>();
            System.Array.Reverse(parents);
            string relativepathf = "";
            foreach (Transform p in parents)
            {
                if (p.gameObject != poseparent.gameObject)
                {
                    relativepathf += p.name;
                    relativepathf += "/";

                }
            }
            relativepathf = relativepathf.Substring(0, relativepathf.Length - 1);
            Pose2Anim.SetAllTransformCurvesAt0(relativepathf, ch, edited);
        }
        Debug.Log("Sucessfully added pose to animation start.");


    }
    public static void DoPose2AnimPublicTimeFrame(AnimationClip toedit, Transform poseparent, float timeframe)
    {
        AnimationClip edited = toedit;
        Pose2Anim.SetAllTransformCurvesAtTimeFrame(poseparent.GetChild(0).name, poseparent.GetChild(0), edited, timeframe);

        Transform[] childrenofbone255 = poseparent.GetChild(0).gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform ch in childrenofbone255)
        {
            Transform[] parents = ch.gameObject.GetComponentsInParent<Transform>();
            System.Array.Reverse(parents);
            string relativepathf = "";
            foreach (Transform p in parents)
            {
                if (p.gameObject != poseparent.gameObject)
                {
                    relativepathf += p.name;
                    relativepathf += "/";

                }
            }
            relativepathf = relativepathf.Substring(0, relativepathf.Length - 1);
            Pose2Anim.SetAllTransformCurvesAtTimeFrame(relativepathf, ch, edited, timeframe);
        }
        Debug.Log("Sucessfully added pose to animation start.");


    }

    public static void SetAllTransformCurvesAt0(string relativepath, Transform holder, AnimationClip edited)
    {
       
        var curvebinding = new EditorCurveBinding();
        curvebinding.propertyName = "m_LocalPosition.x";
        curvebinding.path = relativepath;
        curvebinding.type = typeof(Transform);
        var acurve = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding) != null)
        {
        
           acurve = AnimationUtility.GetEditorCurve(edited, curvebinding);
        }
        
        acurve.AddKey(new Keyframe(0f, holder.localPosition.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding, acurve);
        var curvebinding2 = new EditorCurveBinding();
        curvebinding2.propertyName = "m_LocalPosition.y";
        curvebinding2.path = relativepath;
        curvebinding2.type = typeof(Transform);
        var acurve2 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding2) != null)
        {

            acurve2 = AnimationUtility.GetEditorCurve(edited, curvebinding2);
        }
        acurve2.AddKey(new Keyframe(0f, holder.localPosition.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding2, acurve2);
        var curvebinding3 = new EditorCurveBinding();
        curvebinding3.propertyName = "m_LocalPosition.z";
        curvebinding3.path = relativepath;
        curvebinding3.type = typeof(Transform);
        var acurve3 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding3) != null)
        {

            acurve3 = AnimationUtility.GetEditorCurve(edited, curvebinding3);
        }
        acurve3.AddKey(new Keyframe(0f, holder.localPosition.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding3, acurve3);

        var curvebinding4 = new EditorCurveBinding();
        curvebinding4.propertyName = "m_LocalRotation.x";
        curvebinding4.path = relativepath;
        curvebinding4.type = typeof(Transform);
        var acurve4 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding4) != null)
        {

            acurve4 = AnimationUtility.GetEditorCurve(edited, curvebinding4);
        }
        acurve4.AddKey(new Keyframe(0f, holder.localRotation.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding4, acurve4);
        var curvebinding5 = new EditorCurveBinding();
        curvebinding5.propertyName = "m_LocalRotation.y";
        curvebinding5.path = relativepath;
        curvebinding5.type = typeof(Transform);
        var acurve5 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding5) != null)
        {

            acurve5 = AnimationUtility.GetEditorCurve(edited, curvebinding5);
            
        }
        acurve5.AddKey(new Keyframe(0f, holder.transform.localRotation.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding5, acurve5);
        var curvebinding6 = new EditorCurveBinding();
        curvebinding6.propertyName = "m_LocalRotation.z";
        curvebinding6.path = relativepath;
        curvebinding6.type = typeof(Transform);
        var acurve6 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding6) != null)
        {

            acurve6 = AnimationUtility.GetEditorCurve(edited, curvebinding6);
        }
        acurve6.AddKey(new Keyframe(0f, holder.transform.localRotation.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding6, acurve6);
        var curvebinding7 = new EditorCurveBinding();
        curvebinding7.propertyName = "m_LocalRotation.w";
        curvebinding7.path = relativepath;
        curvebinding7.type = typeof(Transform);
        var acurve7 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding7) != null)
        {

            acurve7 = AnimationUtility.GetEditorCurve(edited, curvebinding7);
        }
        acurve7.AddKey(new Keyframe(0f, holder.transform.localRotation.w));
        AnimationUtility.SetEditorCurve(edited, curvebinding7, acurve7);

        var curvebinding8 = new EditorCurveBinding();
        curvebinding8.propertyName = "m_LocalScale.x";
        curvebinding8.path = relativepath;
        curvebinding8.type = typeof(Transform);
        var acurve8 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding8) != null)
        {

            acurve8 = AnimationUtility.GetEditorCurve(edited, curvebinding8);
        }
        acurve8.AddKey(new Keyframe(0f, holder.localScale.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding8, acurve8);
        var curvebinding9 = new EditorCurveBinding();
        curvebinding9.propertyName = "m_LocalScale.y";
        curvebinding9.path = relativepath;
        curvebinding9.type = typeof(Transform);
        var acurve9 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding9) != null)
        {

            acurve9 = AnimationUtility.GetEditorCurve(edited, curvebinding9);
        }
        acurve9.AddKey(new Keyframe(0f, holder.localScale.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding9, acurve9);
        var curvebinding0 = new EditorCurveBinding();
        curvebinding0.propertyName = "m_LocalScale.z";
        curvebinding0.path = relativepath;
        curvebinding0.type = typeof(Transform);
        var acurve0 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding0) != null)
        {

            acurve0 = AnimationUtility.GetEditorCurve(edited, curvebinding0);
        }
        acurve0.AddKey(new Keyframe(0f, holder.localScale.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding0, acurve0);


    }
    public static void SetAllTransformCurvesAtTimeFrame(string relativepath, Transform holder, AnimationClip edited, float timeframe)
    {

        var curvebinding = new EditorCurveBinding();
        curvebinding.propertyName = "m_LocalPosition.x";
        curvebinding.path = relativepath;
        curvebinding.type = typeof(Transform);
        var acurve = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding) != null)
        {

            acurve = AnimationUtility.GetEditorCurve(edited, curvebinding);
        }

        acurve.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localPosition.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding, acurve);
        var curvebinding2 = new EditorCurveBinding();
        curvebinding2.propertyName = "m_LocalPosition.y";
        curvebinding2.path = relativepath;
        curvebinding2.type = typeof(Transform);
        var acurve2 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding2) != null)
        {

            acurve2 = AnimationUtility.GetEditorCurve(edited, curvebinding2);
        }
        acurve2.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localPosition.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding2, acurve2);
        var curvebinding3 = new EditorCurveBinding();
        curvebinding3.propertyName = "m_LocalPosition.z";
        curvebinding3.path = relativepath;
        curvebinding3.type = typeof(Transform);
        var acurve3 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding3) != null)
        {

            acurve3 = AnimationUtility.GetEditorCurve(edited, curvebinding3);
        }
        acurve3.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localPosition.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding3, acurve3);

        var curvebinding4 = new EditorCurveBinding();
        curvebinding4.propertyName = "m_LocalRotation.x";
        curvebinding4.path = relativepath;
        curvebinding4.type = typeof(Transform);
        var acurve4 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding4) != null)
        {

            acurve4 = AnimationUtility.GetEditorCurve(edited, curvebinding4);
        }
        acurve4.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localRotation.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding4, acurve4);
        var curvebinding5 = new EditorCurveBinding();
        curvebinding5.propertyName = "m_LocalRotation.y";
        curvebinding5.path = relativepath;
        curvebinding5.type = typeof(Transform);
        var acurve5 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding5) != null)
        {

            acurve5 = AnimationUtility.GetEditorCurve(edited, curvebinding5);

        }
        acurve5.AddKey(new Keyframe(timeframe / edited.frameRate, holder.transform.localRotation.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding5, acurve5);
        var curvebinding6 = new EditorCurveBinding();
        curvebinding6.propertyName = "m_LocalRotation.z";
        curvebinding6.path = relativepath;
        curvebinding6.type = typeof(Transform);
        var acurve6 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding6) != null)
        {

            acurve6 = AnimationUtility.GetEditorCurve(edited, curvebinding6);
        }
        acurve6.AddKey(new Keyframe(timeframe / edited.frameRate, holder.transform.localRotation.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding6, acurve6);
        var curvebinding7 = new EditorCurveBinding();
        curvebinding7.propertyName = "m_LocalRotation.w";
        curvebinding7.path = relativepath;
        curvebinding7.type = typeof(Transform);
        var acurve7 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding7) != null)
        {

            acurve7 = AnimationUtility.GetEditorCurve(edited, curvebinding7);
        }
        acurve7.AddKey(new Keyframe(timeframe / edited.frameRate, holder.transform.localRotation.w));
        AnimationUtility.SetEditorCurve(edited, curvebinding7, acurve7);

        var curvebinding8 = new EditorCurveBinding();
        curvebinding8.propertyName = "m_LocalScale.x";
        curvebinding8.path = relativepath;
        curvebinding8.type = typeof(Transform);
        var acurve8 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding8) != null)
        {

            acurve8 = AnimationUtility.GetEditorCurve(edited, curvebinding8);
        }
        acurve8.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localScale.x));
        AnimationUtility.SetEditorCurve(edited, curvebinding8, acurve8);
        var curvebinding9 = new EditorCurveBinding();
        curvebinding9.propertyName = "m_LocalScale.y";
        curvebinding9.path = relativepath;
        curvebinding9.type = typeof(Transform);
        var acurve9 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding9) != null)
        {

            acurve9 = AnimationUtility.GetEditorCurve(edited, curvebinding9);
        }
        acurve9.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localScale.y));
        AnimationUtility.SetEditorCurve(edited, curvebinding9, acurve9);
        var curvebinding0 = new EditorCurveBinding();
        curvebinding0.propertyName = "m_LocalScale.z";
        curvebinding0.path = relativepath;
        curvebinding0.type = typeof(Transform);
        var acurve0 = new AnimationCurve();
        if (AnimationUtility.GetEditorCurve(edited, curvebinding0) != null)
        {

            acurve0 = AnimationUtility.GetEditorCurve(edited, curvebinding0);
        }
        acurve0.AddKey(new Keyframe(timeframe / edited.frameRate, holder.localScale.z));
        AnimationUtility.SetEditorCurve(edited, curvebinding0, acurve0);


    }
}
#endif
