using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraRectTweenQueue : MonoBehaviour
{
    public List<float> Queue;
    public float endvalue;
    public Camera Opponent;
    public void Start()
    {
        endvalue = base.gameObject.GetComponent<Camera>().rect.width;
      
    }

    public void Update()
    {
        if (base.gameObject.GetComponent<Camera>().rect.width < 0.5f)
        {
            Opponent.rect = new Rect(base.gameObject.GetComponent<Camera>().rect.width, Opponent.rect.y, Opponent.rect.width, Opponent.rect.height);
        }
        else
        {
            Opponent.rect = new Rect(0.5f, Opponent.rect.y, Opponent.rect.width, Opponent.rect.height);
        }
        
    }

    public void TweenRect(float endvaloffset, float time)
    {
        if (DOTween.IsTweening(base.gameObject.GetComponent<Camera>())){

            base.gameObject.GetComponent<Camera>().DOKill();

        }

        base.gameObject.GetComponent<Camera>().DORect(new Rect(0f, 0f, endvalue + endvaloffset, 1f), time);
        endvalue = base.gameObject.GetComponent<Camera>().rect.width + endvaloffset;
    }
   

}
