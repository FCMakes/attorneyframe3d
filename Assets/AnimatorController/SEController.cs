using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<AudioClip> Tracks;
   

    public AudioClip FindSE(string name)
    {
        foreach (AudioClip clip in Tracks)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }
}
