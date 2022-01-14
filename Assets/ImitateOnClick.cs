using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImitateOnClick : MonoBehaviour
{

    public KeyCode forimitation;
    public KeyCode button2;
    public bool invokeOnClick;
  

  
    public bool isCovered()
    {

        foreach (Image img in GameObject.FindObjectsOfType<Image>())
        {
            if (img.raycastTarget && img.gameObject != base.gameObject && img.canvas != null && img.canvas.enabled && FCServices.GetWorldSpaceRect(base.gameObject.GetComponent<RectTransform>()).Overlaps(FCServices.GetWorldSpaceRect(img.gameObject.GetComponent<RectTransform>()), true))
            {
                Canvas imgcanvas = img.canvas;
                Canvas buttoncanvas = base.gameObject.GetComponentInParent<Canvas>();
                Debug.Log(base.gameObject.name + " " + img.name);
                if (imgcanvas == buttoncanvas)
                {
                    List<GameObject> alldescendants = new List<GameObject>();
                    FCServices.MakeListOfAllDescendants(imgcanvas.gameObject, alldescendants);
                    if (alldescendants.IndexOf(img.gameObject) > alldescendants.IndexOf(base.gameObject))
                    {
                       
                        return true;
                    }
                   


                }
                else
                {
                    if (imgcanvas.sortingOrder > buttoncanvas.sortingOrder)
                    {
                        
                        return true;
                    }
                    

                }

            }

        }
        return false;

    }

    public IEnumerator ImitateInvokeDo()
    {
        yield return new WaitForEndOfFrame();
        base.gameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();

    }

    public void Update()
    {
        if (base.gameObject.GetComponentInParent<Canvas>().enabled && base.gameObject.GetComponent<Button>().interactable &&  base.gameObject.activeInHierarchy && !isCovered())
        {
            if (Input.GetKeyDown(forimitation) || Input.GetKeyDown(button2) && button2 != KeyCode.None)
            {
                FCServices.ButtonDownEffect(base.gameObject);
            }
            if (Input.GetKeyUp(forimitation) || Input.GetKeyUp(button2) && button2 != KeyCode.None)
            {
                FCServices.ButtonUpEffect(base.gameObject);
                if (invokeOnClick)
                {
                    base.StartCoroutine(this.ImitateInvokeDo());
                }
              

            }
        }

       



    }
}
