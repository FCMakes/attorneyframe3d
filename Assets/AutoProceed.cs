using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoProceed : MonoBehaviour
{
    // Start is called before the first frame update
   public void ProceedDestroyAfterAudioEnd()
    {
        Invoke("ProceedFRM", base.gameObject.GetComponent<AudioSource>().clip.length);

    }

    public void ProceedFRMKeep()
    {

        GameObject.FindObjectOfType<ModifyStrings>().FRMNext();
       
    }
    public void ProceedFRM()
    {

        GameObject.FindObjectOfType<ModifyStrings>().FRMNext();
        Destroy(base.gameObject);
    }

    public void ShakeCam()
    {
        GameObject.FindObjectOfType<ModifyStrings>().ShakeCamera();
    }


    public void ParentDestruct()
    {
        Destroy(base.gameObject.transform.parent.gameObject);
    }

    public void SelfDestruct()
    {
        Destroy(base.gameObject);
    }

    public void CloneSelf()
    {
        Instantiate(base.gameObject, base.gameObject.transform.parent);
    }
}
