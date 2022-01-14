using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBWright : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public GameObject JumpSound;

    public bool isGrounded;
    Rigidbody rb;
    public float rotspeed;
    public GameObject PointFinger;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }


    public void OnCollisionStay()
    {
        isGrounded = true;
    }
   

    public void Jumped()
    {
        if (isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.VelocityChange);
         
            isGrounded = false;
        }
    }

   
    public void Update()
    {
        if (Input.GetAxis("Mouse X") != 0f)
        {
            base.gameObject.transform.Rotate(0, rotspeed * Input.GetAxis("Mouse X") * Time.deltaTime, 0);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            base.gameObject.GetComponent<Animator>().SetBool("isAttacking", true);
           

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            base.gameObject.GetComponent<Animator>().SetBool("isAttacking", false);
        }

        if (base.gameObject.GetComponent<Animator>().GetBool("isAttacking"))
        {
            if (GameObject.FindObjectOfType<BBController>().currentBomb != null)
            { GameObject bomb = GameObject.FindObjectOfType<BBController>().currentBomb;
                Debug.Log(Vector3.Distance(PointFinger.transform.position, bomb.transform.position));
                if (Vector3.Distance(PointFinger.transform.position, bomb.transform.position) <= 7.5f)
                {
                    bomb.GetComponent<Rigidbody>().AddForce(PointFinger.transform.up * -750f + PointFinger.transform.forward * 250f);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumped();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
            
            if (!base.gameObject.GetComponent<Animator>().GetBool("isWalking"))
            {
                base.gameObject.GetComponent<Animator>().SetBool("isWalking", true);

            }

        }
        else
        {

            if (base.gameObject.GetComponent<Animator>().GetBool("isWalking"))
            {
                base.gameObject.GetComponent<Animator>().SetBool("isWalking", false);

            }
        }


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 1.5f;


           
            
        }
        else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed * 1.5f;
            
        }
        else if (Input.GetKey("s") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
            
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed * 1.5f;
            
        }
        else if (Input.GetKey("a") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
           
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("d"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed * 1.5f;
            
           
        }
        else if (Input.GetKey("d") && !Input.GetKey(KeyCode.LeftShift))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
           
        }
    }
}
