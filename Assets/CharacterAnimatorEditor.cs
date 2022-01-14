#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class CharacterAnimatorEditor : EditorWindow
{
    Animator tobeedited;
    string idlestatename;
    string posestatename;
    string poseparametername;
    string preanimstatename;
    string tnstatename;
    float tweenlength;
    float tweenlength2;
    int layerindex;
    AnimationClip talkanim;
    AnimationClip additionalanim;
    string additionallayername;
    public enum TransitionType
    {
        BothUnityTweens,
        PreAnimAndTween,
        PreAnimAndTN,
        TweenAndTN

    }
    TransitionType currentType;

    [MenuItem("Window/Character Animator Editor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CharacterAnimatorEditor window = (CharacterAnimatorEditor)EditorWindow.GetWindow(typeof(CharacterAnimatorEditor));
        window.Show();
        window.Start();
    }

    void Start()
    {
        tweenlength = 0.25f;
        tweenlength2 = tweenlength;
        layerindex = 0;
    }

    private void OnGUI()
    {
        tobeedited = (Animator)EditorGUILayout.ObjectField(tobeedited, typeof(Animator));
        if (tobeedited != null)
        {
            currentType = (TransitionType)EditorGUILayout.EnumPopup("Transition Type", currentType);
        layerindex = EditorGUILayout.IntField("Layer Index", layerindex);
        if (currentType != TransitionType.PreAnimAndTN)
        {
            tweenlength = EditorGUILayout.FloatField("Tween Length", tweenlength);
            if (currentType == TransitionType.BothUnityTweens)
            {
                tweenlength2 = EditorGUILayout.FloatField("2nd Tween Length", tweenlength2);
            }
        }
      
        
        
            var runtimeController = tobeedited.runtimeAnimatorController;

            var controller = UnityEditor.AssetDatabase.LoadAssetAtPath<UnityEditor.Animations.AnimatorController>(UnityEditor.AssetDatabase.GetAssetPath(runtimeController));

            idlestatename = EditorGUILayout.TextField("Idle State Name", idlestatename);
            posestatename = EditorGUILayout.TextField("Pose State Name", posestatename);
            if (currentType == TransitionType.PreAnimAndTN || currentType == TransitionType.PreAnimAndTween)
            {
                preanimstatename = EditorGUILayout.TextField("Pre Anim State Name", preanimstatename);
            }
            if (currentType == TransitionType.PreAnimAndTN || currentType == TransitionType.TweenAndTN)
            {
                tnstatename = EditorGUILayout.TextField("TN State Name", tnstatename);
            }
            poseparametername = EditorGUILayout.TextField("Pose Parameter Name", poseparametername);

            AnimatorControllerParameter newparam = new AnimatorControllerParameter();
            newparam.name = "is" + poseparametername;
            newparam.type = AnimatorControllerParameterType.Bool;


            if (GUILayout.Button("Create Pose Transitions"))
            {
                controller.AddParameter(newparam);
                if (currentType == TransitionType.BothUnityTweens)
                {
                    AnimatorState IdleState = FCServices.GetStateByName(idlestatename, controller, layerindex);
                    AnimatorState PoseState = FCServices.GetStateByName(posestatename, controller, layerindex);

                    AnimatorStateTransition trans1 = IdleState.AddTransition(PoseState);
                    trans1.AddCondition(AnimatorConditionMode.If, 1f, "is" + poseparametername);
                    trans1.hasExitTime = false;
                    trans1.hasFixedDuration = true;
                    trans1.duration = tweenlength;
                    AnimatorStateTransition trans2 = PoseState.AddTransition(IdleState);
                    trans2.AddCondition(AnimatorConditionMode.IfNot, 1f, "is" + poseparametername);
                    trans2.hasExitTime = false;
                    trans2.hasFixedDuration = true;
                    trans2.duration = tweenlength2;
                }
                if (currentType == TransitionType.PreAnimAndTween)
                {
                    AnimatorState IdleState = FCServices.GetStateByName(idlestatename, controller, layerindex);
                    AnimatorState PoseState = FCServices.GetStateByName(posestatename, controller, layerindex);
                    AnimatorState PreAnimState = FCServices.GetStateByName(preanimstatename, controller, layerindex);

                    AnimatorStateTransition trans1 = IdleState.AddTransition(PreAnimState);
                    trans1.AddCondition(AnimatorConditionMode.If, 1f, "is" + poseparametername);
                    trans1.hasExitTime = false;
                    trans1.hasFixedDuration = true;
                    trans1.duration = 0f;
                    AnimatorStateTransition trans05 = PreAnimState.AddTransition(PoseState);

                    trans05.hasExitTime = true;
                    trans05.exitTime = 1;
                    trans05.hasFixedDuration = true;
                    trans05.duration = 0f;

                    AnimatorStateTransition trans2 = PoseState.AddTransition(IdleState);
                    trans2.AddCondition(AnimatorConditionMode.IfNot, 1f, "is" + poseparametername);
                    trans2.hasExitTime = false;
                    trans2.hasFixedDuration = true;
                    trans2.duration = tweenlength;
                }
                if (currentType == TransitionType.PreAnimAndTN)
                {
                    AnimatorState IdleState = FCServices.GetStateByName(idlestatename, controller, layerindex);
                    AnimatorState PoseState = FCServices.GetStateByName(posestatename, controller, layerindex);
                    AnimatorState PreAnimState = FCServices.GetStateByName(preanimstatename, controller, layerindex);
                    AnimatorState TNState = FCServices.GetStateByName(tnstatename, controller, layerindex);

                    AnimatorStateTransition trans1 = IdleState.AddTransition(PreAnimState);
                    trans1.AddCondition(AnimatorConditionMode.If, 1f, "is" + poseparametername);
                    trans1.hasExitTime = false;
                    trans1.hasFixedDuration = true;
                    trans1.duration = 0f;
                    AnimatorStateTransition trans05 = PreAnimState.AddTransition(PoseState);

                    trans05.hasExitTime = true;
                    trans05.exitTime = 1;
                    trans05.hasFixedDuration = true;
                    trans05.duration = 0f;

                    AnimatorStateTransition trans2 = PoseState.AddTransition(TNState);
                    trans2.AddCondition(AnimatorConditionMode.IfNot, 1f, "is" + poseparametername);
                    trans2.hasExitTime = false;
                    trans2.hasFixedDuration = true;
                    trans2.duration = 0f;

                    AnimatorStateTransition trans25 = TNState.AddTransition(IdleState);

                    trans25.hasExitTime = true;
                    trans25.exitTime = 1;
                    trans25.hasFixedDuration = true;
                    trans25.duration = 0f;

                }
                if (currentType == TransitionType.TweenAndTN)
                {
                    AnimatorState IdleState = FCServices.GetStateByName(idlestatename, controller, layerindex);
                    AnimatorState PoseState = FCServices.GetStateByName(posestatename, controller, layerindex);

                    AnimatorState TNState = FCServices.GetStateByName(tnstatename, controller, layerindex);


                    AnimatorStateTransition trans05 = IdleState.AddTransition(PoseState);

                    trans05.hasExitTime = false;
                    trans05.AddCondition(AnimatorConditionMode.If, 1f, "is" + poseparametername);
                    trans05.hasFixedDuration = true;
                    trans05.duration = tweenlength;

                    AnimatorStateTransition trans2 = PoseState.AddTransition(TNState);
                    trans2.AddCondition(AnimatorConditionMode.IfNot, 1f, "is" + poseparametername);
                    trans2.hasExitTime = false;
                    trans2.hasFixedDuration = true;
                    trans2.duration = 0f;

                    AnimatorStateTransition trans25 = TNState.AddTransition(IdleState);

                    trans25.hasExitTime = true;
                    trans25.exitTime = 1;
                    trans25.hasFixedDuration = true;
                    trans25.duration = 0f;

                }
            }

            talkanim = (AnimationClip)EditorGUILayout.ObjectField(talkanim, typeof(AnimationClip));
            if (GUILayout.Button("Create Talking Layer"))
            {
                AnimatorControllerParameter newparam2 = new AnimatorControllerParameter();
                newparam2.name = "isTalking";
                newparam2.type = AnimatorControllerParameterType.Bool;
                controller.AddParameter(newparam2);

                controller.AddLayer("Talk");
                tobeedited.SetLayerWeight(tobeedited.GetLayerIndex("Talk"), 1f);
                AnimatorState nothing = controller.layers[1].stateMachine.AddState("Nothing");

                AnimatorState talking = controller.layers[1].stateMachine.AddState("Talking");
                talking.motion = talkanim;
                talking.name = "Talking";


                AnimatorStateTransition tr1 = nothing.AddTransition(talking);
                tr1.hasExitTime = false;
                tr1.hasFixedDuration = true;
                tr1.duration = 0;
                tr1.AddCondition(AnimatorConditionMode.If, 1f, "isTalking");

                AnimatorStateTransition tr2 = talking.AddTransition(nothing);
                tr2.hasExitTime = false;
                tr2.hasFixedDuration = true;
                tr2.duration = 0;
                tr2.AddCondition(AnimatorConditionMode.IfNot, 1f, "isTalking");



            }
            additionallayername = EditorGUILayout.TextField("Additional Layer Name", additionallayername);
            additionalanim = (AnimationClip)EditorGUILayout.ObjectField(additionalanim, typeof(AnimationClip));

            if (GUILayout.Button("Create Additional Layer"))
            {
                controller.AddLayer(additionallayername);
                AnimatorState additionalanimstate = controller.layers[tobeedited.GetLayerIndex(additionallayername)].stateMachine.AddState(additionalanim.name);
                additionalanimstate.motion = additionalanim;
                additionalanimstate.name = additionalanim.name;
                tobeedited.SetLayerWeight(tobeedited.GetLayerIndex(additionallayername), 1f);
            }

        }

        
        

       

    }
}
#endif