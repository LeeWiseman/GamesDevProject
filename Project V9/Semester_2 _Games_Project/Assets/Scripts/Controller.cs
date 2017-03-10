using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour {

    //Vector3 movement;
    float currentSpeed, movementSpeed = 3, sprintSpeed = 6, crouchSpeed = 1;
    public int noise = 0;
    private float rotationSpeed = 360;
    private int jumpForce = 300, downforce = 300;

    private float minHearDistance=7;
    private float middleHearDistance=35;
    private float highHearDistance = 70;
    public GameObject flashlight;

    public String prompt = "E to Open";
    // Use this for initialization
    void Start () {


      transform.localScale = new Vector3(1, 2, 1);
      transform.localPosition = new Vector3(13, 2, -1);
      
     

        var camera = Camera.main.transform;
        
        camera.parent =  transform;
        
        camera.localPosition = new Vector3(0, 1, .2f);
        currentSpeed = movementSpeed;
        
        


    }




    // Update is called once per frame
    void Update() {


        isLookingAtDoor();
        
        //w
        if (Input.GetKey(KeyCode.W))
        {
            
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
            noise = 1;
        }

        //a
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * (movementSpeed - 1) * Time.deltaTime);
            noise = 1;
        }

        //s
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.forward * (movementSpeed - 1) * Time.deltaTime);
            noise = 1;
        }

        //d
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * (movementSpeed - 1) * Time.deltaTime);
            noise = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            noise = 2;

        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = movementSpeed;
            
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            currentSpeed = crouchSpeed;
            noise = 0;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            currentSpeed = movementSpeed;
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
            noise = 1;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * downforce);
            noise = 0;

        }



        Camera.main.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
    }

    internal bool canIhearYou(Vector3 position)
    {
        float dist = Vector3.Distance(position, transform.position);
        if (dist < minHearDistance) return true;

        else if
            (dist < middleHearDistance && noise > 0)
        {
            return true;

        }

        else if (dist < highHearDistance && noise > 1) return true;

        else return false;
    }

    internal void AddHealth(int v)
    {
        print("Health Added");
    }

    public void isLookingAtDoor()
    {
        if (Physics.Raycast(transform.position, Camera.main.transform.forward, 2))
        {
            print("Something in front!");
        }
           
    }
    void onGUI()
    {
        prompt = (GUI.TextField(new Rect(10, 10, 15, 15), prompt));
    }

    }







