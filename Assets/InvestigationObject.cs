using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigationObject : MonoBehaviour
{
    public int FRM;
    public bool Investigated;
    public bool requiredToProgress;
    public bool dontRegisterExamination;
    public float examineDistance;

    public void Start()
    {
        if (examineDistance == 0)
        {
            examineDistance = float.PositiveInfinity;
        }
    }


}
