using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{


    [System.Serializable]
    public class TrackLoopData
    {
        public string name;
        public float startTime;
        public float endTime;
    }

    public List<TrackLoopData> TrackLoops;

    public List<AudioClip> Tracks;

    public void DelayedStopMusic(float time)
    {
        Invoke("StopMusic", time);
    }
   public void StopMusic()
    {
        base.gameObject.GetComponent<AudioSource>().Stop();
    }



    public void Update()
    {if (base.gameObject.GetComponent<AudioSource>().clip != null)
        {
            foreach (TrackLoopData tld in TrackLoops)
                if (base.gameObject.GetComponent<AudioSource>().clip.name == tld.name)
                {
                   
                    if (base.gameObject.GetComponent<AudioSource>().time >= tld.endTime)
                    {

                        base.gameObject.GetComponent<AudioSource>().time = tld.startTime;
                    }
                    else
                    {
                      
                    }

                    if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                    {
                        base.gameObject.GetComponent<AudioSource>().time = tld.endTime - 2;
                    }

                }
        }
         
      
    }


    public AudioClip FindTrack(string name)
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
