using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour  {

    enum NPCBehaviour { Patroling, Search, Attack}
    NPCBehaviour currentMode = NPCBehaviour.Patroling;
    float currentSpeed, standStill = 0f, walkSpeed = 3.5f, sprintSpeed = 8;
    NavMeshAgent agent;
    int patrolDestinationIndex = 1;
    Controller player;
    public int count= 2;
    Vector3 HearMovementAt;
    public Light light;

    Vector3 patrolPoint2=new Vector3(0,2,50), patrolPoint1 = new Vector3(50,2,50);
    List<Vector3> waypoints;
    private Vector3 agentDestination;



    // Use this for initialization
    void Start () {
        waypoints = new List<Vector3>();

        waypoints.Add(new Vector3(0, 2, 50));
        waypoints.Add(new Vector3(50, 2, 50));


        transform.localPosition = waypoints[0];
        patrolDestinationIndex = 1;
        player = FindObjectOfType<Controller>();
        currentSpeed = walkSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = currentSpeed;
        agent.SetDestination(waypoints[patrolDestinationIndex]);

        GameObject hand = new GameObject();
        hand.transform.position = transform.position + 2* Vector3.up;
        hand.transform.rotation = transform.rotation;
        hand.transform.parent = transform;
        hand.AddComponent<Light>();
        light = hand.GetComponent<Light>();
        light.range = 50;
        light.color = Color.red;
        light.type = LightType.Spot;
        light.intensity = 3;
        light.range = 220;
        light.bounceIntensity = 0;
        light.enabled = false;
        light.shadows = LightShadows.Soft;
        agent.autoBraking = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentMode)
        {

            case NPCBehaviour.Patroling:

                light.enabled = false;
                currentSpeed = walkSpeed;

                if (NPChasReachedWaypoint())
                {
                    chooseNextWaypoint();

                }

                if (player.canIhearYou(transform.position) == true)
                {
                    currentMode = NPCBehaviour.Search;
                    print("I CAN HEAR YOU");
                    light.enabled = true;
                    currentSpeed = sprintSpeed;
                    HearMovementAt = player.transform.position;
                    agent.SetDestination(HearMovementAt);
                }


                break;

            case NPCBehaviour.Search:

                if (ICanSeeYou())
                {
                    currentMode = NPCBehaviour.Attack;
                }


                if (Vector3.Distance(HearMovementAt, transform.position) < 1.0)
                {
                    agent.SetDestination(waypoints[patrolDestinationIndex]);
                    currentMode = NPCBehaviour.Patroling;
                }

                break;

            case NPCBehaviour.Attack:




             

                break;


        }





        //if (player.canIhearYou(transform.position) == false )
        //{
        //    light.enabled = false;
        //    currentSpeed = walkSpeed;

        //    if (agentAtPoint1() == true)
        //    {
        //        agent.SetDestination(patrolPoint2);
        //    }
            
        //    if (agentAtPoint2() == true)
        //    {
        //        agent.SetDestination(patrolPoint1);
        //    }

        //}

    }

    private bool ICanSeeYou()
    {
        return Vector3.Distance(player.transform.position, transform.position) < 2.5f;
    }

    private void chooseNextWaypoint()
    {
        //patrolDestinationIndex = (patrolDestinationIndex + 1) % waypoints.Count;

        patrolDestinationIndex++;

        if (patrolDestinationIndex == waypoints.Count) patrolDestinationIndex = 0;
        agent.SetDestination(waypoints[patrolDestinationIndex]);

    }

    private bool NPChasReachedWaypoint()
    {
        return (Vector3.Distance(transform.position, waypoints[patrolDestinationIndex]) < 1.0f);
    }

    public bool agentAtPoint1()
    {
        if (agent.destination == patrolPoint1)
            return true;

        else return false;
    }

    public bool agentAtPoint2()
    {
        if (agent.destination == patrolPoint2)
            return true;

        else return false;
    }

    
    
    }
    
    
   

