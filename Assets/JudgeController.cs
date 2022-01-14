using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeController : BaseCharacterController
{
    // Start is called before the first frame update
    public ModifyStrings HQ;

   
   public void GavelSlam()
    {
        base.gameObject.GetComponent<SoundPlay>().PlaySound();
        GameObject.Find("GallerySE").GetComponent<AudioSource>().Stop();
    }
   
}
