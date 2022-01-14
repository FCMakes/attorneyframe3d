using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingUI : MonoBehaviour
{

    public Vector2 startpos;
    public float dir;
    public float length;
    public Vector2 axis;
    public float time;
    public void GoUp()
    {
        

        base.gameObject.GetComponent<RectTransform>().DOAnchorPos(base.gameObject.GetComponent<RectTransform>().anchoredPosition + axis * length * 0.01f * dir, time * 0.01f);
       
    }

    public void ToStartPos()
    {
        base.gameObject.GetComponent<RectTransform>().DOKill();
        base.gameObject.GetComponent<RectTransform>().anchoredPosition = startpos;
        GoUp();
    }
    public void Start()
    {
        startpos = base.gameObject.GetComponent<RectTransform>().anchoredPosition;


        Invoke("SelfClone", 0f);
        InvokeRepeating("ToStartPos", 0f, time);
        InvokeRepeating("GoUp", 0f, time * 0.01f);
       
    }

   public void SelfClone()
    {
        
            GameObject clone = Instantiate(base.gameObject.transform.GetChild(base.gameObject.transform.childCount - 1).gameObject, base.gameObject.transform);


        clone.GetComponent<RectTransform>().anchoredPosition -= axis * length * dir;
        
        if (base.gameObject.transform.childCount > 6){

           
            Destroy(base.gameObject.transform.GetChild(base.gameObject.transform.childCount - 6).gameObject);
        }






    }
}
