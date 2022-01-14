using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendingExaminationEffect : MonoBehaviour
{

    public Vector3 offset;
    public void Update()
    {
       
       
        ModifyStrings hq = GameObject.FindObjectOfType<ModifyStrings>();
        if (FCServices.GetComponentFromObjectOfType<Animator>(typeof(FakeCursorController)).GetBool("isOverExaminable") && !FCServices.GetComponentFromObjectOfType<Animator>(typeof(FakeCursorController)).GetBool("isAlreadyExamined") && !base.gameObject.GetComponent<Animator>().GetBool("isVisible") && !Input.GetMouseButton(1))
        {
            base.gameObject.GetComponent<Animator>().SetBool("isVisible", true);
        } 
        if (FCServices.GetComponentFromObjectOfType<Animator>(typeof(FakeCursorController)).GetBool("isOverExaminable") && FCServices.GetComponentFromObjectOfType<Animator>(typeof(FakeCursorController)).GetBool("isAlreadyExamined") && base.gameObject.GetComponent<Animator>().GetBool("isVisible") || !FCServices.GetComponentFromObjectOfType<Animator>(typeof(FakeCursorController)).GetBool("isOverExaminable")  && base.gameObject.GetComponent<Animator>().GetBool("isVisible") || !hq.canInvestigate && !hq.isExamining3d && hq.lines[0] != "[JointReasoningSelect]" && base.gameObject.GetComponent<Animator>().GetBool("isVisible"))
        {
            base.gameObject.GetComponent<Animator>().SetBool("isVisible", false);
        }

    }
}
