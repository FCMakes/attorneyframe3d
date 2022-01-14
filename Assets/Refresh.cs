using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{

    public void DoRefresh()
    {
        foreach (Transform child in base.gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
        foreach (Transform child in base.gameObject.transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    
    [ExecuteInEditMode]
   public void Start()
    {

        DoRefresh();
    }

    
}
