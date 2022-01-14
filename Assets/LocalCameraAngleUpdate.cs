using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraAngleUpdate : MonoBehaviour
{
    public List<MainCameraController.CameraAngle> anglesToUpdateLocally;

    public void Update()
    {
        foreach (MainCameraController.CameraAngle ca in anglesToUpdateLocally)
        {
            MainCameraController mcc = GameObject.FindObjectOfType<MainCameraController>();
            if (mcc.WithName(ca.name) != null && mcc.WithName(ca.name).position != base.gameObject.transform.TransformPoint(ca.position) || mcc.WithName(ca.name).rotation != base.gameObject.transform.rotation * ca.rotation)
            {
                mcc.WithName(ca.name).position = base.gameObject.transform.TransformPoint(ca.position);
                mcc.WithName(ca.name).rotation = base.gameObject.transform.rotation * ca.rotation;

            }
        }
    }
   

}
