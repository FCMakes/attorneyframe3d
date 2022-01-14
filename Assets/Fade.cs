using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{

    public void FadeIn()
    {
        base.gameObject.GetComponent<Graphic>().color = new Color(0f, 0f, 0f, 0f);
    }

    public void FadeOut()
    {
       
        base.gameObject.GetComponent<Graphic>().CrossFadeAlpha(255f, 2.5f, true);
    }
}
