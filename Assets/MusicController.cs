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

    public void Start()
    {
        InvokeRepeating("MusicUpdate", 0f, 0.0001f);
    }
    public void DelayedStopMusic(float time)
    {
        Invoke("StopMusic", time);
    }
   public void StopMusic()
    {
        base.gameObject.GetComponent<AudioSource>().Stop();
    }



    public void MusicUpdate()
    {
        Debug.Log("Invoking");
        if (base.gameObject.GetComponent<AudioSource>().clip != null)
        {
            if (FromName(base.gameObject.GetComponent<AudioSource>().clip.name) != null)
            {
                if (base.gameObject.GetComponent<AudioSource>().time >= FromName(base.gameObject.GetComponent<AudioSource>().clip.name).endTime && FromName(base.gameObject.GetComponent<AudioSource>().clip.name).endTime <= base.gameObject.GetComponent<AudioSource>().clip.length || base.gameObject.GetComponent<AudioSource>().time >= base.gameObject.GetComponent<AudioSource>().clip.length && FromName(base.gameObject.GetComponent<AudioSource>().clip.name).endTime > base.gameObject.GetComponent<AudioSource>().clip.length)
                {
                    base.gameObject.GetComponent<AudioSource>().time = FromName(base.gameObject.GetComponent<AudioSource>().clip.name).startTime;
                }


                if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    base.gameObject.GetComponent<AudioSource>().time = FromName(base.gameObject.GetComponent<AudioSource>().clip.name).endTime - 3f;
                }
            }
        }
        
      
    }

    public TrackLoopData FromName(string name)
    {
        foreach (TrackLoopData tld in TrackLoops)
        {
            if (tld.name == name)
            {
                return tld;
            }
        }
        return null;
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
