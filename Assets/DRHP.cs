using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DRHP : MonoBehaviour
{
   public void TakePenalty(float amount)
    {
        base.gameObject.GetComponent<Slider>().DOValue(base.gameObject.GetComponent<Slider>().value - amount, 0.5f);
    }

    public void RestoreHealth(float amount)
    {
        base.gameObject.GetComponent<Slider>().DOValue(base.gameObject.GetComponent<Slider>().value + amount, 0.5f);
    }


}
