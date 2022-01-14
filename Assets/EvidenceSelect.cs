using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceSelect : FCMakes.LazyPort.ButtonInputManager { 

    public enum selecttype
    {
        Evidence,
        Profiles
    }
    public selecttype thistype;
    public int ToSelect;

    public void Update()
    {
        if (this.GetKeyDown)
        {
          
            Record rc = GameObject.FindObjectOfType<Record>();
            if (!rc.gameObject.GetComponents<AudioSource>()[0].isPlaying)
            {
                rc.gameObject.GetComponents<AudioSource>()[0].Play();
            }
            if (thistype == selecttype.Evidence)
            {
                rc.SelectEvidence(ToSelect);
            }
            else
            {
                rc.SelectProfile(ToSelect);
            }
        }
    }


}
