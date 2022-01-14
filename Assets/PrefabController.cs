using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabController : MonoBehaviour
{
    public List<GameObject> Prefabs;
 

   
    public static PrefabController Instance()
    {
        return GameObject.FindObjectOfType<PrefabController>();
    }

    public GameObject FindPrefab(string name)
    {
        foreach (GameObject p in Prefabs)
        {
            if (p.name == name)
            {
                return p;
            }
        }
        return null;
    }
}
