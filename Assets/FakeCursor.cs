using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCursor : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    public void Update()
    {
        base.gameObject.transform.GetChild(0).position = GameObject.FindObjectOfType<PendingExaminationEffect>().gameObject.transform.position + offset;

        if (GameObject.FindObjectOfType<MoveController>().enabled && Cursor.lockState == CursorLockMode.Locked)
        {
            base.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            base.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }



        base.gameObject.GetComponent<Animator>().SetBool("hasExaminable", GameObject.FindObjectOfType<PendingExaminationEffect>().gameObject.GetComponent<Animator>().GetBool("isVisible")) ;
        
    }
}
