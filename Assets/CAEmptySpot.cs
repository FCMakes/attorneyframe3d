using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAEmptySpot : MonoBehaviour
{
    public string CorrectPiece;
    public int HintFRM;
    public bool filled;
    public void Update()
    {
        if (!filled && Input.GetMouseButtonDown(0) && RectTransformUtility.RectangleContainsScreenPoint(base.gameObject.GetComponent<RectTransform>(), Input.mousePosition))
        {
            GameObject.FindObjectOfType<ModifyStrings>().LoadFromFRM(HintFRM);
        }
    }

}
