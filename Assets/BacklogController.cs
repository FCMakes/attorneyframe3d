using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BacklogController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BacklogButton;
   
    public void AddMessage(string name, string text, Color color, TextAnchor alignment)
    {
        GameObject newMessage = Instantiate(PrefabController.Instance().FindPrefab("BacklogUnit"), base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
        newMessage.transform.GetChild(0).gameObject.GetComponent<Text>().text = name;
        newMessage.transform.GetChild(1).gameObject.GetComponent<Text>().text = text;
        newMessage.transform.GetChild(1).gameObject.GetComponent<Text>().color = color;
        newMessage.transform.GetChild(1).gameObject.GetComponent<Text>().alignment = alignment;
        newMessage.SetActive(false);
      

       
        }

    public void CommitPrevious()
    {
        if (base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.childCount > 1)
        {
            base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.GetChild(base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.childCount - 1).gameObject.SetActive(true);

        }
        if (base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.childCount == 2 && FCServices.FindChildWithName(base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject, "NoPreviousMessages") != null)
        {
            Destroy(FCServices.FindChildWithName(base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject, "NoPreviousMessages"));
        }
        if (base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.childCount > 50)
        {
            Destroy(base.gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform.GetChild(0).gameObject);

        }
    }

    public void Show()
    {
        base.gameObject.GetComponent<AudioSource>().Play();
        GameObject.Find("MainUI").GetComponent<Canvas>().enabled = false;
        GameObject.Find("TopButtons").GetComponent<Canvas>().enabled = false;
        GameObject.Find("ChoiceButtons").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Penalty").GetComponent<Canvas>().enabled = false;
        GameObject.Find("CEButtons").GetComponent<Canvas>().enabled = false;
        GameObject.Find("PendingExamination").GetComponent<Canvas>().enabled = false;
        base.gameObject.GetComponent<Canvas>().enabled = true;
    }

    public void Hide()
    {
        base.gameObject.GetComponents<AudioSource>()[1].Play();
        GameObject.Find("MainUI").GetComponent<Canvas>().enabled = true;
        GameObject.Find("TopButtons").GetComponent<Canvas>().enabled = true;
        if (GameObject.FindObjectOfType<ModifyStrings>().lines[0] == "[Choice]")
        {
            GameObject.Find("ChoiceButtons").GetComponent<Canvas>().enabled = true;
        }
        GameObject.Find("Penalty").GetComponent<Canvas>().enabled = true;
        if (GameObject.FindObjectOfType<ModifyStrings>().CELine() != -1)
        {
            GameObject.Find("CEButtons").GetComponent<Canvas>().enabled = true;
        }
        GameObject.Find("PendingExamination").GetComponent<Canvas>().enabled = true;
        base.gameObject.GetComponent<Canvas>().enabled = false;
    }

    public void Update()
    {
        if (GameObject.FindObjectOfType<ModifyStrings>().NameBox.enabled == false && GameObject.FindObjectOfType<ModifyStrings>().EmptyBox.enabled == false)
        {
            if (BacklogButton.activeSelf)
            {
                BacklogButton.SetActive(false);
            }
        }
        if (GameObject.FindObjectOfType<ModifyStrings>().NameBox.enabled || GameObject.FindObjectOfType<ModifyStrings>().EmptyBox.enabled)
        {
            if (!BacklogButton.activeSelf)
            {
                BacklogButton.SetActive(true);
            }
        }

        if (GameObject.FindObjectOfType<ModifyStrings>().tw.isTyping == BacklogButton.GetComponent<Button>().interactable)
        
            {

                BacklogButton.GetComponent<Button>().interactable = !GameObject.FindObjectOfType<ModifyStrings>().tw.isTyping;
            }


           
        
        if (base.gameObject.GetComponent<Canvas>().enabled)
        {

            base.gameObject.GetComponentInChildren<ScrollRect>().content.transform.position += new Vector3(0f, Input.GetAxis("Vertical") * Time.deltaTime * 250f, 0f);
        }
    }
}
