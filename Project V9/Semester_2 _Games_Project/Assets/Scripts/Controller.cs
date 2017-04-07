using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour
{

    //Vector3 movement;
    float currentSpeed, movementSpeed = 3, sprintSpeed = 6, crouchSpeed = 1;
    public int noise = 0;
    private float rotationSpeed = 360;
    private int jumpForce = 300, downforce = 300;

    private float minHearDistance = 7;
    private float middleHearDistance = 35;
    private float highHearDistance = 70;
    public GameObject flashlight;
    public Texture2D crosshairImage;


    public String prompt = "E to Pick Up";
    

    // Use this for initialization
    void Start()
    {


        transform.localScale = new Vector3(1, 2, 1);
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = new Quaternion(0, 0, 0, 0);



        var camera = Camera.main.transform;

        camera.parent = transform;

        camera.localPosition = new Vector3(0, 0.5f, 0);
        camera.localRotation = new Quaternion(0, 0, 0, 0);
        currentSpeed = movementSpeed;


        



    }




    // Update is called once per frame
    void Update()
    {


        lookingAtDoor();



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

    public void lookingAtDoor()
    {
        Vector3 centre = new Vector3(Screen.height / 2, Screen.width / 2);
        RaycastHit info;
        Ray ray =  new Ray(this.transform.position, Camera.main.transform.forward);// Camera.main.ScreenPointToRay(centre);
        
        if (Physics.Raycast(ray, out info))
        {
            print(info.collider.gameObject.name);
            if (info.collider.gameObject.name == "Cube")
            {
                
                 if(Input.GetKey(KeyCode.E))
                Destroy (info.collider.gameObject);
                
            }
        }
   }
   
        public void OnGUI() {

        float xMin = (Screen.width / 2) - (crosshairImage.width/2);
        float yMin = (Screen.height / 2) - (crosshairImage.height/2);
        GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
        // GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), prompt);
        GUI.Label(new Rect(0,0, 500f, 500f), "Health 100");
    }
                

 }




    
   









