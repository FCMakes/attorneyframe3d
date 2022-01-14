using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProceedButton : MonoBehaviour
{
    public ModifyStrings Hq;
    public bool ProceedDelayed;
    public void Update()
    {
        if (GameObject.Find("ButtonTextSkip") != null)
        {
            if (GameObject.Find("ButtonTextSkip").GetComponent<Button>().interactable == base.gameObject.GetComponent<Button>().interactable)
            {
                GameObject.Find("ButtonTextSkip").GetComponent<Button>().interactable = !base.gameObject.GetComponent<Button>().interactable;
                GameObject.Find("ButtonTextSkip").GetComponent<Image>().raycastTarget = GameObject.Find("ButtonTextSkip").GetComponent<Button>().interactable;
            }
        }

        if (Hq.tw.isTyping && base.gameObject.GetComponent<Button>().interactable)
        {
            base.gameObject.GetComponent<Button>().interactable = false;
        }
        if (!Hq.tw.isTyping && !base.gameObject.GetComponent<Button>().interactable && !this.ProceedDelayed)

        {
            
            base.gameObject.GetComponent<Button>().interactable = true;
            if (Hq.lines.Length > 12 && Hq.lines[12] == "Auto")
            {
                
            }
            else
            {
                if (base.gameObject.GetComponent<Image>().enabled)
                {
                    Hq.Save(0);
                }
               
            }
          
        }
        if (Hq.lines[0] != "[DebateComment]" && Hq.lines[0] != "[JointReasoningComment]")
        {
            if (!Hq.NameBox.enabled && !Hq.EmptyBox.enabled || Hq.Choice.enabled || Hq.lines[0].Contains("[") || Hq.lines[0] == "[Present]" || Hq.CELine() == -1 && Hq.tw.isTyping)
            {
                Debug.Log("Hid button");
                base.gameObject.GetComponent<Image>().enabled = false;
                base.gameObject.GetComponent<Button>().interactable = false;
                FCServices.FindChildWithName(base.gameObject.transform.parent.gameObject, "Image").GetComponent<Image>().enabled = false;
            }
        }
       
        if (Hq.NameBox.enabled && !base.gameObject.GetComponent<Image>().enabled && !Hq.Choice.enabled && !Hq.lines[0].Contains("[") && Hq.tw.isTyping == false|| Hq.EmptyBox.enabled && !base.gameObject.GetComponent<Image>().enabled && !Hq.Choice.enabled && !Hq.lines[0].Contains("[") && Hq.tw.isTyping == false)
        {
            base.gameObject.GetComponent<Image>().enabled = true;
            if (!Hq.tw.isTyping)
            {
                base.gameObject.GetComponent<Button>().interactable = true;
            }
            FCServices.FindChildWithName(base.gameObject.transform.parent.gameObject, "Image").GetComponent<Image>().enabled = true;
        }

        base.gameObject.GetComponent<Animator>().SetBool("isActive", base.gameObject.GetComponent<Button>().interactable);
    }

    public void Proceed()
    {
        if (Hq.CELine() == -1)
        {
            base.gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            GameObject.Find("ChoiceSE").GetComponent<AudioSource>().Play();
        }
        base.gameObject.GetComponent<Button>().interactable = false;
        this.ProceedDelayed = true;
        Invoke("ActuallyProceed", 0.25f);

       

    }

    public void ActuallyProceed()
    {
      
        Hq.FRMNext();
        this.ProceedDelayed = false;
    }
}
