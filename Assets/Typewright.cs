using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewright : MonoBehaviour
{
   

    public float delay;
    public int currentchar;
    public bool isTyping;
    public AudioSource src;
    public bool silent;
    public string currenttext;

    public void SkipTypewriter()
    {
        base.CancelInvoke();
        base.gameObject.GetComponent<Text>().text = currenttext;
        isTyping = false;
    }

    public void TypeNextLetter()
    {
        char c = currenttext.ToCharArray()[currentchar];
        if (c != char.Parse("<") && c != char.Parse(@"\"))
        {
            base.gameObject.GetComponent<Text>().text += c;
        }
        else
        {
            if (c == char.Parse("<"))
            {
                base.gameObject.GetComponent<Text>().text += currenttext.Substring(currentchar, 24);
            }

            if (c == char.Parse(@"\"))
            {
                base.gameObject.GetComponent<Text>().text += Environment.NewLine;
            }
        }
       
      
        if (c != char.Parse(" ") && !silent && c != char.Parse(@"\"))
        {
            if (!src.isPlaying)
            {
                src.Play();
            }
        }
        if (GameObject.FindObjectOfType<ModifyStrings>().lines.Length > 9 && GameObject.FindObjectOfType<ModifyStrings>().lines[9] != "")
        {
            if (this.currentchar == int.Parse(GameObject.FindObjectOfType<ModifyStrings>().lines[9]))
            {

                GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().clip = GameObject.FindObjectOfType<SEController>().FindSE(GameObject.FindObjectOfType<ModifyStrings>().lines[10]);
                if (!GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().isPlaying)
                {
                    GameObject.FindObjectOfType<SEController>().gameObject.GetComponent<AudioSource>().Play();
                }


                Instantiate(PrefabController.Instance().FindPrefab("Flash"));

                if (GameObject.FindObjectOfType<ModifyStrings>().lines[10] != "ayashii" && !GameObject.FindObjectOfType<ModifyStrings>().lines[10].Contains("diminish"))
                {
                    GameObject.FindObjectOfType<ModifyStrings>().RumbleController(15);
                    GameObject.FindObjectOfType<ModifyStrings>().ShakeCamera();
                }



            }

        }
        if (currentchar != currenttext.ToCharArray().Length - 1)
        {
            if (c != char.Parse(".") && c != char.Parse(",") && c != char.Parse(":") && c != char.Parse(";") && c != char.Parse("-") && c != char.Parse("!") && c != char.Parse("?") || c == char.Parse(".") && currenttext.ToCharArray()[currentchar + 1] == char.Parse(".") || c == char.Parse(".") && currenttext.ToCharArray()[currentchar - 1] == char.Parse(".") || c == char.Parse(".") && currenttext.ToCharArray()[currentchar - 3] == char.Parse(" ") && currenttext.ToCharArray()[currentchar - 2].ToString().ToLower() == char.Parse("M").ToString().ToLower() && currenttext.ToCharArray()[currentchar - 1] == char.Parse("r") || c == char.Parse(".") && currenttext.ToCharArray()[currentchar - 3] == char.Parse(" ") && currenttext.ToCharArray()[currentchar - 2].ToString().ToLower() == char.Parse("M").ToString().ToLower() && currenttext.ToCharArray()[currentchar - 1] == char.Parse("s") || c == char.Parse(".") && currenttext.ToCharArray()[currentchar - 4] == char.Parse(" ") && currenttext.ToCharArray()[currentchar - 3].ToString().ToLower() == char.Parse("M").ToString().ToLower() && currenttext.ToCharArray()[currentchar - 2] == char.Parse("r") && currenttext.ToCharArray()[currentchar - 1] == char.Parse("s"))
            {
               Invoke("TypeNextLetter", delay);
            }
            else
            {
                if (c == char.Parse(","))
                {
                    Invoke("TypeNextLetter", delay * 5f);
                }
                if (c == char.Parse(":") || c == char.Parse(";") || c == char.Parse("-"))
                {
                    Invoke("TypeNextLetter", delay * 6f);
                }
                if (c == char.Parse(".") || c == char.Parse("?") || c == char.Parse("!"))
                {

                    Invoke("TypeNextLetter", delay * 10f);
                }
            }
        }
        else
        {
            isTyping = false;
        }
        if (c != char.Parse("<"))
        {
            currentchar += 1;
        }
        else
        {
            currentchar += 24;
        }
    }
    public void Type(string text)
    {
        currenttext = text;
        base.gameObject.GetComponent<Text>().text = "";
        currentchar = 0;
        isTyping = true;
        TypeNextLetter();

    }

}
