using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnScroll : MonoBehaviour, 
    IBeginDragHandler, IEndDragHandler
{
    public GameObject SEParent;
    public int ASId;

    public void OnBeginDrag(PointerEventData eventData)
    {
        SEParent.GetComponents<AudioSource>()[ASId].Play();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        SEParent.GetComponents<AudioSource>()[ASId].Stop();
    }



}
