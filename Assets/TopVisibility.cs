using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopVisibility : MonoBehaviour
{
    // Start is called before the first frame update
   
   public void Update()
    {
        if (base.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled != base.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().enabled)
        {
            base.gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().enabled = base.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled;
        }
        if (base.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled != base.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled)
        {
            if (GameObject.FindObjectOfType<TW_MultiStrings_Regular>().gameObject.GetComponent<Text>().color != GameObject.FindObjectOfType<ModifyStrings>().Blue)
            {
                base.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = base.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled;
                base.gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().enabled = base.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().enabled;
            }
        }
        if (GameObject.FindObjectOfType<TW_MultiStrings_Regular>().gameObject.GetComponent<Text>().color == GameObject.FindObjectOfType<ModifyStrings>().Blue)
        { 
            if (base.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled)
            {
                base.gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled = false;
                base.gameObject.transform.GetChild(2).GetChild(0).gameObject.GetComponent<Text>().enabled = false;
            }
        }
        }
}
