using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    // Start is called before the first frame update
   public void PlaySound()
    {
        base.gameObject.GetComponent<AudioSource>().Play();
    }

    public void PlaySound2()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
    }
}
