using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCharacterController : MonoBehaviour
{
    public bool canTalk;
    public string CharacterName;
    public Quaternion originalrotation;

    public void Start()
    {
        originalrotation = base.gameObject.transform.rotation;
    }
    public void Update()
    {
        if (canTalk)
        {
            CheckForTalking();
        }
    }


    public void CheckForTalking()
    {
        ModifyStrings HQ = GameObject.FindObjectOfType<ModifyStrings>();
        Typewright tw = HQ.tw;
        Text nametag = HQ.nametag;

        if (tw.isTyping && tw.gameObject.GetComponent<Text>().text != "" && !base.gameObject.GetComponent<Animator>().GetBool("isTalking") && nametag.text == CharacterName && tw.gameObject.GetComponent<Text>().color != HQ.Blue)        {
            base.gameObject.GetComponent<Animator>().SetBool("isTalking", true);

        }
        if (!tw.isTyping && base.gameObject.GetComponent<Animator>().GetBool("isTalking") && nametag.text == CharacterName)
        {
            base.gameObject.GetComponent<Animator>().SetBool("isTalking", false);
        }
    }

}
