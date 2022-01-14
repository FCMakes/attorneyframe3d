using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonImageDisabled : MonoBehaviour
{
    public Color originalcolor;

    public void Start()
    {
        originalcolor = base.gameObject.GetComponent<Image>().color;
    }

    public void Update()
    {
        if (!base.gameObject.GetComponentInParent<Button>().interactable && base.gameObject.GetComponent<Image>().color == originalcolor)
        {
            base.gameObject.GetComponent<Image>().color = base.gameObject.GetComponentInParent<Button>().colors.disabledColor;
        }
        if (base.gameObject.GetComponentInParent<Button>().interactable && base.gameObject.GetComponent<Image>().color != originalcolor)
        {
            base.gameObject.GetComponent<Image>().color = originalcolor;
        }
    
    }
}
