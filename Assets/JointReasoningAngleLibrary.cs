using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointReasoningAngleLibrary : MonoBehaviour
{
   [System.Serializable]
   public class JointReasoningAngle
    {
        public string JRAngleName;
        public FCServices.Interval ClampX;
        public FCServices.Interval ClampY;
        public Transform RotParent;
        public string CameraAngleName;
    }

    public List<JointReasoningAngle> angles;
}
