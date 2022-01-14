using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruthBulletsController : MonoBehaviour
{
    public string ActiveBullet;
    public GameObject BulletsScrollbox;

    
    public void PlaySE()
    {
        base.gameObject.GetComponent<AudioSource>().Play();
    }
    public void SetActiveBullet(string name)
    {
        if (FCServices.FindChildWithArray(base.gameObject, "Evidence", name))
        {
            
            foreach (Transform child in FCServices.FindChildWithName(base.gameObject, "Evidence").transform)
            {
                if (child.gameObject.name == name)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
            foreach (Transform button in BulletsScrollbox.transform)
            {
                if (button.name == name)
                {
                    button.gameObject.GetComponent<Image>().enabled = true;
                }
                else
                {
                    button.gameObject.GetComponent<Image>().enabled = false;
                }
            }
            ActiveBullet = name;
      

        }
    }


}
