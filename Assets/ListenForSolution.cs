using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListenForSolution : MonoBehaviour
{
    public Text txt;
    // Start is called before the first frame update
    public void Update()
    {
        if (txt.text == "You are actually...")
        {
            Debug.Log("Played");
            base.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
