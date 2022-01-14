using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonCE : MonoBehaviour
{
    public ModifyStrings Hq;
    public bool PreviousDelayed;
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void Update()
    {
        if (Hq.tw.isTyping && base.gameObject.GetComponent<Button>().interactable || Hq.lines.Length > 24 && Hq.lines[25] == "Statement" && Hq.CurrentFRM == int.Parse(Hq.CrossExaminationData[11]) && base.gameObject.GetComponent<Button>().interactable)
        {
            base.gameObject.GetComponent<Button>().interactable = false;
        }
        if (!Hq.tw.isTyping && !base.gameObject.GetComponent<Button>().interactable && !this.PreviousDelayed && Hq.lines.Length > 24 && Hq.lines[25] == "Statement" && Hq.CurrentFRM != int.Parse(Hq.CrossExaminationData[11]) || !Hq.tw.isTyping && !base.gameObject.GetComponent<Button>().interactable && !this.PreviousDelayed && Hq.lines.Length > 24 && Hq.lines[25] == "{SummationExamination}")

        {
            base.gameObject.GetComponent<Button>().interactable = true;
        }
        if (Hq.lines.Length > 24 && Hq.lines[25] == "Statement" && !base.gameObject.GetComponent<Image>().enabled || Hq.lines.Length > 24 && Hq.lines[25] == "{SummationExamination}" && !base.gameObject.GetComponent<Image>().enabled)
        {
            base.gameObject.GetComponent<Image>().enabled = true;
            FCServices.FindChildWithName(base.gameObject.transform.parent.gameObject, "Image (1)").GetComponent<Image>().enabled = true;
        }
        
            if (base.gameObject.GetComponent<Image>().enabled && Hq.lines.Length < 24 || base.gameObject.GetComponent<Image>().enabled && Hq.lines.Length > 24 && Hq.lines[25] != "Statement" && Hq.lines[25] != "{SummationExamination}")
            {
                base.gameObject.GetComponent<Image>().enabled = false;
                FCServices.FindChildWithName(base.gameObject.transform.parent.gameObject, "Image (1)").GetComponent<Image>().enabled = false;
            }
        base.gameObject.GetComponent<Animator>().SetBool("isActive", base.gameObject.GetComponent<Button>().interactable);
    }

    public void GoBack()
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
        this.PreviousDelayed = true;
        Invoke("ActuallyGoBack", 0.25f);
    }

    public void ActuallyGoBack()
    {
        this.PreviousDelayed = false;
        Hq.FRMPreviousCE();
       
    }
}
