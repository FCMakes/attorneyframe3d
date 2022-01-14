using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Switch : MonoBehaviour
{
   
    public GameObject[] Lights;
    public Material LightMaterial;
    public GameObject InvisibleWall;

    public void DoSwitch()
    {
        
            foreach(GameObject light in Lights)
            {
                light.GetComponent<AudioSource>().Play();
                light.GetComponent<Light>().enabled = false;
                light.GetComponent<MeshRenderer>().sharedMaterial = LightMaterial;
            }
        InvisibleWall.SetActive(false);
        }

    public void Update()
    {
       
    }
}
