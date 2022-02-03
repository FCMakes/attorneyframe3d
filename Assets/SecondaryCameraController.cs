using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SecondaryCameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public List<MainCameraController.CameraAngle> AvailableAngles;

    public void AddAngle(MainCameraController.CameraAngle angle)
    {
        AvailableAngles.Add(angle);
    }


    public MainCameraController.CameraAngle WithName(string name)
    {

        foreach (MainCameraController.CameraAngle  ca in AvailableAngles)
        {
            if (ca.name == name)
            {
                return ca;
            }

        }
        return null;
    }
    public bool isAngle(string tocheck)
    {
        if (base.gameObject.transform.rotation == WithName(tocheck).rotation && base.gameObject.transform.position == WithName(tocheck).position)
        {
            return true;
        }
        return false;

    }
    public void UpdateShakePos()
    {
        if (base.gameObject.GetComponent<CameraShake>())
        {
            base.gameObject.GetComponent<CameraShake>().originalPos = base.gameObject.transform.localPosition;
        }

    }

    public void TweenAngleLongWay(string name, float time)
    {
        foreach (MainCameraController.CameraAngle angle in AvailableAngles)
        {
            if (angle.name == name)
            {
                base.gameObject.transform.DOMove(angle.position, time);
                base.gameObject.transform.DORotate(angle.rotation.eulerAngles, time, RotateMode.FastBeyond360);
            }
        }
        Invoke("UpdateShakePos", time);
    }
    public void TweenAngle(MainCameraController.CameraAngle angle, float time)
    {
        base.gameObject.transform.DOMove(angle.position, time);
        base.gameObject.transform.DORotateQuaternion(angle.rotation, time);
        Invoke("UpdateShakePos", time);
    }
    public void TweenAngle(string name, float time)
    {
        if (name == "Wright" || name == "Edgeworth")
        {
            if (name == "Wright")
            {
                base.gameObject.transform.DOMove(WithName("Defense").position, time);
                base.gameObject.transform.DORotateQuaternion(WithName("Defense").rotation, time);

            }
            if (name == "Edgeworth")
            {
                base.gameObject.transform.DOMove(WithName("Prosecution").position, time);
                base.gameObject.transform.DORotateQuaternion(WithName("Prosecution").rotation, time);
            }
        }
        
        else
        {
            foreach (MainCameraController.CameraAngle angle in AvailableAngles)
            {
                if (angle.name == name)
                {
                    base.gameObject.transform.DOMove(angle.position, time);
                    base.gameObject.transform.DORotateQuaternion(angle.rotation, time);
                }
            }
        }
        Invoke("UpdateShakePos", time);

    }

    public void SelectAngle(string name)
    {
        if (name == "Wright" || name == "Edgeworth")
        {
            if (name == "Wright")
            {
                base.gameObject.transform.SetPositionAndRotation(WithName("Defense").position, WithName("Defense").rotation);
            }
            if (name == "Edgeworth")
            {
                base.gameObject.transform.SetPositionAndRotation(WithName("Prosecution").position, WithName("Prosecution").rotation);
            }
        }
        else
        {
            foreach (MainCameraController.CameraAngle angle in AvailableAngles)
            {
                if (angle.name == name)
                {
                    base.gameObject.transform.SetPositionAndRotation(angle.position, angle.rotation);
                }
            }
        }

        UpdateShakePos();
    }

}
