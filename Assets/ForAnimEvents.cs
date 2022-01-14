using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnimEvents : MonoBehaviour
{
    public GameObject Victim;
    public AudioSource ToPlay;

    public void SetVictimActive()
    {
        Victim.SetActive(true);
    }
    public void SetVictimInactive()
    {
        Victim.SetActive(false);
    }

    public void PlayAudio()
    {
        ToPlay.Play();
    }

    public void StopAudio()
    {
        ToPlay.Stop();
    }

}
