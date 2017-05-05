using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{

    //Vector3 movement;
    float currentSpeed, movementSpeed = 3, sprintSpeed = 6, crouchSpeed = 1;
    public int noise = 0;
    private float rotationSpeed = 360;
    private int jumpForce = 300, downforce = 300;
    private int health=100;
    private float minHearDistance = 7;
    private float middleHearDistance = 35;
    private float highHearDistance = 70;
    public GameObject flashlight;
    public Texture2D crosshairImage;
    public Boolean lookingDoor = false;
    public Boolean lookingCube = false;
    public Boolean locked = false;
    public Boolean takenCube = false;
    public Boolean escape = false;
    public String prompt = "E to Pick Up";

    public float hitTime = 0; 
    
    EnemyController enemy;
    

    // Use this for initialization
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;


        if (sceneName == "MainScene")
        {
           
            transform.localPosition = new Vector3(14, 2.5f, -0.6f);
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }

        if (sceneName == "Room1")
        {

            transform.localPosition = new Vector3(2.2f, 2.5f, -0.6f);
            transform.localRotation = new Quaternion(0, -180, 0, 0);
                
        }
        if (sceneName == "Room2")
        {

            transform.localPosition = new Vector3(18f, 2.5f, 27.2f);
            transform.localRotation = new Quaternion(0, -88, 0, 0);

        }
        transform.localScale = new Vector3(1, 2, 1);

        var camera = Camera.main.transform;

        camera.parent = transform;

        camera.localPosition = new Vector3(0, 1f, 0);
        camera.localRotation = new Quaternion(0, 0, 0, 0);


            currentSpeed = movementSpeed;


        



    }




    // Update is called once per frame
    void Update()
    {

        hitTime += Time.deltaTime; //add time
        
        

        if (health < 0)
        {
            health = 0;
        }

        if (!enemy)
        {
            enemy = FindObjectOfType<EnemyController>();

            if (enemy)
            print("found Enemy script");


        }
        else 
            if  (hitTime > 2 && Vector3.Distance(enemy.transform.position, transform.position) < 2)
            {
                takeDamage();
            }
        
        if (health <0)
        {
           die();
            print("You Died");
        }
        
        lookingDoor = false;
        lookingCube = false;
        locked = false;
    
        lookingAtDoor();

        if (Time.time > 3 & Time.time < 6)
        {
            escape = true;
        }
       else
        {
            escape = false;
        }



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

    public void takeDamage()
    {
        this.health = this.health - 30; 
        hitTime = 0;
        Rigidbody rb = new Rigidbody();
        rb = this.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(50,30,0),ForceMode.Impulse);
        
        
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
        health += v;
    }

     public void die()
     {
         SceneManager.LoadScene("MainScene");
     }
     
    public void lookingAtDoor()
    {
        Vector3 centre = new Vector3(Screen.height / 2, Screen.width / 2);
        RaycastHit info;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward); //  new Ray(this.transform.position, Camera.main.transform.forward);//Camera.main.ScreenPointToRay(centre);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);

     
        if (Physics.Raycast(ray, out info,3.5f))
        {
            print(info.collider.gameObject.name);
            if (info.collider.gameObject.name == "Cube")
            {
                lookingCube = true;
                if (Input.GetKey(KeyCode.E))
                {
                    Destroy(info.collider.gameObject);
                    AddHealth(25);
                    takenCube = true;
                }
            }
         
            if (info.collider.gameObject.name == "DoorS")
            {

                lookingDoor = true;
                if (Input.GetKey(KeyCode.E))
                    locked = true;

            }
            if (info.collider.gameObject.name == "DoorR")
            {

                lookingDoor = true;
                if (Input.GetKey(KeyCode.E))
                    locked = true;

            }
            if (info.collider.gameObject.name == "DoorL")
            {

                lookingDoor = true;
                if (Input.GetKey(KeyCode.E))
                    SceneManager.LoadScene("Room1", LoadSceneMode.Single);

            }
            if (info.collider.gameObject.name == "DoorEnd")
            {

                lookingDoor = true;
                if (Input.GetKey(KeyCode.E))
                    locked = true;

            }
            if (info.collider.gameObject.name == "DoorRoom")
            {

                lookingDoor = true;
                if (Input.GetKey(KeyCode.E))
                    SceneManager.LoadScene("MainScene", LoadSceneMode.Single);

            }
        }
   }
   
        public void OnGUI() {
        GUI.skin.label.fontSize = 12;
        float xMin = (Screen.width / 2) - (crosshairImage.width/2);
        float yMin = (Screen.height / 2) - (crosshairImage.height/2);
        //GUI.DrawTexture(new Rect(Screen.width/2.3f, Screen.height/3f, crosshairImage.width/2, crosshairImage.height/2), crosshairImage);
        //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200f, 200f), prompt);
        GUI.Label(new Rect(0,0, 500f, 500f), "Health "+health);

        if(lookingDoor == true)
        {
            GUI.Label(new Rect(Screen.width/2, Screen.height/2, 2000f, 2000f), "E To Open");
        }
        if(locked == true)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 3, 2000f, 2000f), "Locked!");
        }
        if(lookingCube == true)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 2000f, 2000f), "E To Take");
        }
        if(escape == true)
        {
            GUI.skin.label.fontSize = 300;
            GUI.skin.label.font = Resources.Load<Font>("Fonts/HELVETICA"); ;
            GUI.Label(new Rect(0,0, 2000f, 2000f), "Escape");
        }
       
    }
                

 }
