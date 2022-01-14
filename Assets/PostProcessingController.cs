using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using DG.Tweening;

public class PostProcessingController : MonoBehaviour
{
    public ColorGrading cg;
    public MotionBlur mb;
    public Bloom b;
    public DepthOfField dof;
    public float satorha;
    // Start is called before the first frame update
    void Start()
    {
        base.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out cg);
        base.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out mb);
        base.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out b);
        base.gameObject.GetComponent<PostProcessVolume>().profile.TryGetSettings(out dof);
    }

   public void MBOff()
    {
        mb.enabled.value = false;
    }

    public void MBOn()
    {
        mb.enabled.value = true;
    }

    public void DepthOfFieldOn()
    {
        DOTween.To(() => dof.focusDistance.value, x => dof.focusDistance.value = x, 0, 1f);
    }

    public IEnumerator DoBloomInSession()
    {
        yield return new WaitForSeconds(1f);

        DOTween.To(() => b.intensity.value, x => b.intensity.value = x, 60, 2f);

        yield return new WaitForSeconds(2f);
        
        DOTween.To(() => b.intensity.value, x => b.intensity.value = x, 0, 3.9f);
    }
    public void BloomInSession()
    {
        b.intensity.value = 0f;
        base.StartCoroutine(this.DoBloomInSession());
    }

    // Update is called once per frame
    public void PastEffect()
    {
        cg.saturation.value = 0f;
        DOTween.To(() => cg.saturation.value, x => cg.saturation.value = x, -100, 150f/60f);

    }

    public void PastEffectOffTween()
    {
        cg.saturation.value = -100f;
        DOTween.To(() => cg.saturation.value, x => cg.saturation.value = x, 0, 150f/60f);
    }

    public void PastEffectOn()
    {
        cg.saturation.value = -100f;
    }

    public void PastEffectOff()
    {
        cg.saturation.value = 0f;
    }

}
