using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoenixController : BaseCharacterController
{

    public ModifyStrings HQ;

  

    public void Slam()
    {
        base.gameObject.GetComponents<AudioSource>()[0].Play();
    }
   
}
