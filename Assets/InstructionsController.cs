using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsController : MonoBehaviour
{
    public List<GameObject> sections;

    public void ShowSectionBase(string name)
    {
        foreach (GameObject section in sections)
        {
            if (section.name != name)
            {
                section.SetActive(false);
            }
            else
            {
                section.SetActive(true);
            }
        }
    }
    public void ShowSectionSelectSE(string name)
    {
        base.gameObject.GetComponents<AudioSource>()[2].Play();
        ShowSectionBase(name);

    }
    public void ShowSectionChangePageSE(string name)
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        ShowSectionBase(name);

    }

    public void ShowSectionBackSE(string name)
    {
        base.gameObject.GetComponents<AudioSource>()[0].Play();
        ShowSectionBase(name);

    }

    public void Show()
    {
        if (!FCServices.GetComponentFromObjectOfType<Canvas>(typeof(OptionsController)).enabled)
        {
            base.gameObject.GetComponent<Canvas>().enabled = true;
            ShowSectionSelectSE("Main");
        }
    }

    public void Hide()
    {
        base.gameObject.GetComponent<Canvas>().enabled = false;
        base.gameObject.GetComponents<AudioSource>()[0].Play();
    }

}
