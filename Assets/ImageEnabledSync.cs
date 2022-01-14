using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageEnabledSync : MonoBehaviour
{

    public Image tosyncwith;
    public void Update()
    {
        base.gameObject.GetComponent<Image>().enabled = tosyncwith.enabled;



    }
}
