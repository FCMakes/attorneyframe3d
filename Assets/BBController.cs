using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBController : MonoBehaviour
{
    public void Start()
    {
        InvokeRepeating("ThrowBomb", 2.5f, 2.5f);
    }

    public GameObject MonokumaBombPrefab;
    public Transform OriginalPos;
    public GameObject currentBomb;
    public void ThrowBomb()
    {
        if (currentBomb != null)
        {
            Destroy(currentBomb);
        }
        
        GameObject bomb = Instantiate(MonokumaBombPrefab);
        bomb.transform.SetPositionAndRotation(OriginalPos.position, OriginalPos.rotation);

        Vector3 targetPostition = new Vector3(GameObject.FindObjectOfType<BBWright>().gameObject.transform.position.x,
                                               bomb.transform.position.y,
                                                 GameObject.FindObjectOfType<BBWright>().gameObject.transform.position.z);


        bomb.transform.LookAt(targetPostition);
        bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.forward * 1000f);

        currentBomb = bomb;
    }


   
   
}
