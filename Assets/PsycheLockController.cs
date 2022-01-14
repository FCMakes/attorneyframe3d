using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsycheLockController : MonoBehaviour
{
    public List<GameObject> Locks;

    public void BreakLock()
    {
        Locks[Locks.Count - 1].GetComponent<Animator>().SetTrigger("Break");
        Locks.Remove(Locks[Locks.Count - 1]);


    }

    public void Unlock()
    {
        base.gameObject.GetComponent<Animator>().SetTrigger("unlock");
    }

}
