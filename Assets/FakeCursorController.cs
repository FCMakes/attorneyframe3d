using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FakeCursorController : MonoBehaviour
{
    public float speed;
    public FCServices.Interval ClampX;
    public FCServices.Interval ClampY;
    public float touchstarttime;
    public Vector2 touchstartpos;

    public Vector3 PointerPosition()
    {
        return base.gameObject.transform.position;
    }

    public void CursorStart()
    {
        base.gameObject.transform.localPosition = Vector3.zero;
        FCServices.FindChildWithName("Picture", "SelectionAppears").transform.SetSiblingIndex(FCServices.FindChildWithName("Picture", "SelectionAppears").transform.parent.childCount - 1);
        FCServices.FindChildWithName("Picture", "SelectionAppears").SetActive(true);
    }
   
    public void Update()
    {

        if (FCServices.Compare(GameObject.FindObjectOfType<ModifyStrings>().lines[0], "[Examine3d]", "[Investigate]"))
        {



            if (FCServices.GetPointTarget(PointerPosition()) != null && FCServices.GetPointTarget(PointerPosition()).GetComponent<InvestigationObject>())
            {
              
                    base.gameObject.GetComponent<Animator>().SetBool("isOverExaminable", true);
                    if (FCServices.GetPointTarget(PointerPosition()).GetComponent<InvestigationObject>().Investigated)
                    {
                        base.gameObject.GetComponent<Animator>().SetBool("isAlreadyExamined", true);
                    FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<Animator>().SetBool("isVisible", false);
                }
                    else
                    {
                        base.gameObject.GetComponent<Animator>().SetBool("isAlreadyExamined", false);
                    FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<Animator>().SetBool("isVisible", true);

                }

                
            }
            if (FCServices.GetPointTarget(PointerPosition()) == null || FCServices.GetPointTarget(PointerPosition()) != null && !FCServices.GetPointTarget(PointerPosition()).GetComponent<InvestigationObject>())
            {

                base.gameObject.GetComponent<Animator>().SetBool("isInExaminationMode", true);
                if (base.gameObject.GetComponent<Animator>().GetBool("isOverExaminable"))
                {
                    base.gameObject.GetComponent<Animator>().SetBool("isOverExaminable", false);

                    base.gameObject.GetComponent<Animator>().SetBool("isAlreadyExamined", false);
                    FCServices.FindChildWithName(base.gameObject, "Image (2)").GetComponent<Animator>().SetBool("isVisible", false);

                }
            }
        }
        else
        {
            base.gameObject.GetComponent<Animator>().SetBool("isInExaminationMode", false);
            if (base.gameObject.GetComponent<Animator>().GetBool("isOverExaminable"))
            {
                base.gameObject.GetComponent<Animator>().SetBool("isOverExaminable", false);

                base.gameObject.GetComponent<Animator>().SetBool("isAlreadyExamined", false);


            }

        }
        if (base.gameObject.GetComponentInParent<Canvas>().enabled && GameObject.FindObjectOfType<ModifyStrings>().lines[0] != "[PointInPicture]" && !GameObject.FindObjectOfType<ModifyStrings>().isExamining3d && !GameObject.FindObjectOfType<ModifyStrings>().canInvestigate  || base.gameObject.GetComponentInParent<Canvas>().enabled && GameObject.FindObjectOfType<BacklogController>().gameObject.GetComponent<Canvas>().enabled)
        {
            base.gameObject.GetComponentInParent<Canvas>().enabled = false;
            GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled = false;
        }
        if (!base.gameObject.GetComponentInParent<Canvas>().enabled && GameObject.FindObjectOfType<ModifyStrings>().lines[0] == "[PointInPicture]" && !GameObject.FindObjectOfType<BacklogController>().gameObject.GetComponent<Canvas>().enabled || !base.gameObject.GetComponentInParent<Canvas>().enabled && GameObject.FindObjectOfType<ModifyStrings>().isExamining3d && !GameObject.FindObjectOfType<BacklogController>().gameObject.GetComponent<Canvas>().enabled || !base.gameObject.GetComponentInParent<Canvas>().enabled && GameObject.FindObjectOfType<ModifyStrings>().canInvestigate && !GameObject.FindObjectOfType<BacklogController>().gameObject.GetComponent<Canvas>().enabled)
        {


            base.gameObject.GetComponentInParent<Canvas>().enabled = true;
         GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled = true;
          
        }

        if (GameObject.FindObjectOfType<ModifyStrings>().isAnyMenusOpen() && GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled && !FCServices.FindChildWithName("Picture", "SelectionAppears").activeSelf)
        {

            GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled = false;
        }
       if (base.gameObject.GetComponentInParent<Canvas>().enabled && !GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled && !GameObject.FindObjectOfType<ModifyStrings>().isAnyMenusOpen())
        {
            GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled = true;
        }

       if (FCServices.FindChildWithName("PresentExamine", "Reset").activeSelf && !GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "Reset").SetActive(false);
        }
        if (!FCServices.FindChildWithName("PresentExamine", "Reset").activeSelf && GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "Reset").SetActive(true);
        }

        if (FCServices.FindChildWithName("PresentExamine", "Image").activeSelf && GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "Image").SetActive(false);
        }
        if (!FCServices.FindChildWithName("PresentExamine", "Image").activeSelf && !GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "Image").SetActive(true);
        }
        float inversedlerp = Mathf.InverseLerp(GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamine").position.z, GameObject.FindObjectOfType<MainCameraController>().WithName("3dExamineZoomIn").position.z, GameObject.FindObjectOfType<MainCameraController>().gameObject.transform.position.z);
        if (FCServices.FindChildWithName("PresentExamine", "ZoomedInArrows").activeSelf && inversedlerp == 0 && GameObject.FindObjectOfType<ModifyStrings>().isExamining3d || !GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "ZoomedInArrows").SetActive(false);
        }
        if (!FCServices.FindChildWithName("PresentExamine", "ZoomedInArrows").activeSelf && inversedlerp > 0 && GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
        {
            FCServices.FindChildWithName("PresentExamine", "ZoomedInArrows").SetActive(true);
        }

        if (FCServices.Compare(GameObject.FindObjectOfType<ModifyStrings>().lines[0], "[PointInPicture]", "[PointIn3dEvidence]")){

            if (FCServices.FindChildWithName("PresentExamine", "PresentButton").GetComponentInChildren<Text>().text != "Present")
            {
                FCServices.FindChildWithName("PresentExamine", "PresentButton").GetComponentInChildren<Text>().text = "Present";
            }
            if (!FCServices.FindChildWithName("PresentExamine", "PresentButton").activeSelf)
            {
                FCServices.FindChildWithName("PresentExamine", "PresentButton").SetActive(true);
            }

        }
        if (FCServices.Compare(GameObject.FindObjectOfType<ModifyStrings>().lines[0], "[Examine3d]", "[Investigation]"))
        {

            if (FCServices.FindChildWithName("PresentExamine", "PresentButton").GetComponentInChildren<Text>().text != "Examine")
            {
                FCServices.FindChildWithName("PresentExamine", "PresentButton").GetComponentInChildren<Text>().text = "Examine";
            }

           if (FCServices.GetPointTarget(PointerPosition()) != null && FCServices.GetPointTarget(PointerPosition()).GetComponent<InvestigationObject>() && !FCServices.FindChildWithName("PresentExamine", "PresentButton").activeSelf)
            {
                FCServices.FindChildWithName("PresentExamine", "PresentButton").SetActive(true);
            }
            if (FCServices.GetPointTarget(PointerPosition()) == null && FCServices.FindChildWithName("PresentExamine", "PresentButton").activeSelf|| FCServices.GetPointTarget(PointerPosition()) != null && !FCServices.GetPointTarget(PointerPosition()).GetComponent<InvestigationObject>() && FCServices.FindChildWithName("PresentExamine", "PresentButton").activeSelf)
            {
                FCServices.FindChildWithName("PresentExamine", "PresentButton").SetActive(false);
            }

        }
        
        if (!FCServices.Compare(GameObject.FindObjectOfType<ModifyStrings>().lines[0], "[Examine3d]", "[Investigate]", "[PointInPicture]", "[PointIn3dEvidence]"))
        {
            if (GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled)
            {
                GameObject.Find("PresentExamine").GetComponent<Canvas>().enabled = false;
            }
        }

        if (base.gameObject.GetComponentInParent<Canvas>().enabled && !GameObject.FindObjectOfType<ModifyStrings>().isAnyMenusOpen())
        {

            if (Application.platform == RuntimePlatform.Android)
            {

                if (Input.touchCount == 1)
                {
                    if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
                    {
                        touchstarttime = Time.time;
                        touchstartpos = Input.GetTouch(0).position;
                    }
                    if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Moved)
                    {
                        if (!GameObject.FindObjectOfType<ModifyStrings>().isExamining3d)
                        {
                            base.gameObject.GetComponent<RectTransform>().position += new Vector3(Input.GetTouch(0).deltaPosition.x / Screen.width * speed * 37.5f * Time.deltaTime, Input.GetTouch(0).deltaPosition.y / Screen.height * 37.55f * Time.deltaTime * speed);
                        }
                      
                    }
                }
                if (Input.GetTouch(0).phase == UnityEngine.TouchPhase.Ended)
                {
                    if (Time.time - touchstarttime <= 0.2f && Input.GetTouch(0).position == touchstartpos)
                    {
                        if (!FCServices.isPosOverClickableUI(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y)))
                        {
                            base.gameObject.GetComponent<RectTransform>().DOMove(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y), 0.25f);
                        }
                    }
                }
                base.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Clamp(base.gameObject.GetComponent<RectTransform>().localPosition.x, ClampX.min, ClampX.max), Mathf.Clamp(base.gameObject.GetComponent<RectTransform>().localPosition.y, ClampY.min, ClampY.max));

            }
            else
            {
                
                    base.gameObject.GetComponent<RectTransform>().position += new Vector3(Input.GetAxis("Horizontal") * speed * Time.deltaTime * (Screen.width / 1920f), Input.GetAxis("Vertical") * Time.deltaTime * speed * (Screen.height / 1080f));
               
                   
                
               

                if (Input.GetKeyDown(KeyCode.Mouse0) && !FCServices.isMouseOverClickableUI())
                {
                    
                    base.gameObject.GetComponent<RectTransform>().DOMove(Input.mousePosition, 0.25f);
                }
                base.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(Mathf.Clamp(base.gameObject.GetComponent<RectTransform>().localPosition.x, ClampX.min, ClampX.max), Mathf.Clamp(base.gameObject.GetComponent<RectTransform>().localPosition.y, ClampY.min, ClampY.max));


            }
        }
    }

}
