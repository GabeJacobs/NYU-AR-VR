using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public Camera mainCamera;

    public Animator rabbitAnimator;

    public bool hasADestination = false;

    //raycast stuff
    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {

        //get reference to nav mesh agent component and camera
        agent = gameObject.GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;

        rabbitAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit))
            {
                //move player
                agent.SetDestination(hit.point);
            }

        }

        if(agent.velocity.magnitude != 0) 
        {
            hasADestination = true;
        }
        else
        {
            hasADestination = false;
        }

        rabbitAnimator.SetBool("isMoving", hasADestination);
    }
}
