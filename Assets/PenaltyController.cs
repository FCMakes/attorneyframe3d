using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyController : MonoBehaviour
{
    public List<GameObject> ActiveBadges;


    public void TakePenalty()
    {
        base.gameObject.GetComponent<AudioSource>().Play();
        List<GameObject> toremove = new List<GameObject>();
        foreach (GameObject go in ActiveBadges)
        {
            if (go.GetComponent<Animator>().GetBool("isPending"))
            {
               
                go.GetComponent<Animator>().SetTrigger("Explode");
                toremove.Add(go);
            }
        }
        foreach (GameObject go in toremove)
        {
            ActiveBadges.Remove(go);
        }
    }
    public void HidePendingPenalty()
    {
        foreach (GameObject go in ActiveBadges)
        {
            go.GetComponent<Animator>().SetBool("isPending", false);
        }
    }

    public int PendingPenalty()
    {
        int pend = 0;
        foreach (GameObject go in ActiveBadges)
        {
            if (go.GetComponent<Animator>().GetBool("isPending"))
            {
                pend += 1;
            }
           
        }
        return pend;
    }
    public void ShowPendingPenalty(int risk)
    {
        if (risk == 1)
        {
            ActiveBadges[0].GetComponent<Animator>().SetBool("isPending", true);

        }
        if (risk == 2)
        {
            ActiveBadges[0].GetComponent<Animator>().SetBool("isPending", true);
            if (ActiveBadges.Count > 1)
            {
                ActiveBadges[1].GetComponent<Animator>().SetBool("isPending", true);
            }
        }
        if (risk == 3)
        {
            ActiveBadges[0].GetComponent<Animator>().SetBool("isPending", true);
            if (ActiveBadges.Count > 1)
            {
                ActiveBadges[1].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 2)
            {
                ActiveBadges[2].GetComponent<Animator>().SetBool("isPending", true);
            }
        }
        if (risk == 4)
        {
            ActiveBadges[0].GetComponent<Animator>().SetBool("isPending", true);
            if (ActiveBadges.Count > 1)
            {
                ActiveBadges[1].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 2)
            {
                ActiveBadges[2].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 3)
            {
                ActiveBadges[3].GetComponent<Animator>().SetBool("isPending", true);
            }
        }
        if (risk == 5)
        {
            ActiveBadges[0].GetComponent<Animator>().SetBool("isPending", true);
            if (ActiveBadges.Count > 1)
            {
                ActiveBadges[1].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 2)
            {
                ActiveBadges[2].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 3)
            {
                ActiveBadges[3].GetComponent<Animator>().SetBool("isPending", true);
            }
            if (ActiveBadges.Count > 4)
            {
                ActiveBadges[4].GetComponent<Animator>().SetBool("isPending", true);
            }
        }
    }
}
