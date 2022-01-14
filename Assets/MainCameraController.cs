using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCameraController : MonoBehaviour
{
   [System.Serializable]
   public class CameraAngle
    {
        public string name;
        public Vector3 position;
        public Quaternion rotation;
    }

    public List<CameraAngle> AvailableAngles;

    public void AddAngle(CameraAngle angle)
    {
        AvailableAngles.Add(angle);
    }


    public CameraAngle WithName(string name)
    {

        foreach (CameraAngle ca in AvailableAngles)
        {
            if (ca.name == name)
            {
                return ca;
            }

        }
        return null;
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
        if (GameObject.FindObjectOfType<PostProcessingController>() != null)
        {
            Invoke("TurnOnMB", 0.25f * time);
            Invoke("TurnOffMB", 0.75f * time);
        }
        foreach (CameraAngle angle in AvailableAngles)
        {
            if (angle.name == name)
            {
                base.gameObject.transform.DOMove(angle.position, time);
                base.gameObject.transform.DORotate(angle.rotation.eulerAngles, time, RotateMode.FastBeyond360);
            }
        }

        foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
        {
            scc.TweenAngleLongWay(name, time);
        }

        Invoke("UpdateShakePos", time);
    }
    public void TweenAngle(CameraAngle angle, float time)
    {
        if (GameObject.FindObjectOfType<PostProcessingController>() != null)
        {
            Invoke("TurnOnMB", 0.25f * time);
            Invoke("TurnOffMB", 0.75f * time);
        }

        base.gameObject.transform.DOMove(angle.position, time);
        base.gameObject.transform.DORotateQuaternion(angle.rotation, time);
        Invoke("UpdateShakePos", time);
        foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
        {
            scc.TweenAngle(angle, time);
        }
    }
    public void TweenAngle(string name, float time)
    {
        if (GameObject.FindObjectOfType<PostProcessingController>() != null)
        {
            GameObject.FindObjectOfType<PostProcessingController>().MBOn();
            Invoke("TurnOffMB", time);
        }
        if (name == "Wright" || name == "Edgeworth")
        {
            if (name == "Wright")
            {
                base.gameObject.transform.DOMove(AvailableAngles[0].position, time);
                base.gameObject.transform.DORotateQuaternion(AvailableAngles[0].rotation, time);
                foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
                {
                    scc.TweenAngle("Wright", time);
                }
            }
            if (name == "Edgeworth")
            {
                base.gameObject.transform.DOMove(AvailableAngles[1].position, time);
                base.gameObject.transform.DORotateQuaternion(AvailableAngles[1].rotation, time);
                foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
                {
                    scc.TweenAngle("Edgeworth", time);
                }
            }
        }
        else
        {
            foreach (CameraAngle angle in AvailableAngles)
            {
                if (angle.name == name)
                {
                    base.gameObject.transform.DOMove(angle.position, time);
                    base.gameObject.transform.DORotateQuaternion(angle.rotation, time);
                }
            }
            foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
            {
                scc.TweenAngle(name, time);
            }
        }
        Invoke("UpdateShakePos", time);
       
    }
    public void TweenAngle(string name, float time, bool blurred)
    {
        if (GameObject.FindObjectOfType<PostProcessingController>() != null && blurred)
        {
            GameObject.FindObjectOfType<PostProcessingController>().MBOn();
            Invoke("TurnOffMB", time);
        }
        if (name == "Wright" || name == "Edgeworth")
        {
            if (name == "Wright")
            {
                base.gameObject.transform.DOMove(WithName("Defense").position, time);
                base.gameObject.transform.DORotateQuaternion(WithName("Defense").rotation, time);
                foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
                {
                    scc.TweenAngle("Wright", time);
                }
            }
            if (name == "Edgeworth")
            {
                base.gameObject.transform.DOMove(WithName("Edgeworth").position, time);
                base.gameObject.transform.DORotateQuaternion(WithName("Edgeworth").rotation, time);
                foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
                {
                    scc.TweenAngle("Edgeworth", time);
                }
            }
        }
        else
        {
            foreach (CameraAngle angle in AvailableAngles)
            {
                if (angle.name == name)
                {
                    base.gameObject.transform.DOMove(angle.position, time);
                    base.gameObject.transform.DORotateQuaternion(angle.rotation, time);
                }
            }
            foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
            {
                scc.TweenAngle(name, time);
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
            foreach (CameraAngle angle in AvailableAngles)
            {
                if (angle.name == name)
                {
                    base.gameObject.transform.SetPositionAndRotation(angle.position, angle.rotation);
                }
            }
        }
        foreach (SecondaryCameraController scc in GameObject.FindObjectsOfType<SecondaryCameraController>())
        {
            scc.SelectAngle(name);
        }


        UpdateShakePos();
    }

    public void TurnOffMB()
    {
        GameObject.FindObjectOfType<PostProcessingController>().MBOff();
    }

    public void TurnOnMB()
    {
        GameObject.FindObjectOfType<PostProcessingController>().MBOn();
    }

}
