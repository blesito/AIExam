using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FieldOfView))]
public class EnemySeek : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] float decisionDelay = 3f;
    //[SerializeField] Transform objectToChase;
    private FieldOfView fieldOfView;
    [SerializeField] private bool randomSeek;

    [SerializeField] Transform[] waypoints; int currentWaypoint = 0;
    enum EnemyStates {
        Patrolling, Chasing
    }

    [SerializeField] EnemyStates currentState;
 
    [SerializeField] Animator animController;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>(); 
        fieldOfView = GetComponent<FieldOfView>();
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);  
        if(currentState == EnemyStates.Patrolling) {
            currentWaypoint = Random.Range(0, waypoints.Length);
            animController.Play("Walking", 0, 0.25f);
            agent.speed = 2.5f;
            agent.SetDestination(waypoints[currentWaypoint].position); 
        }

    }

    float at = 0;
    void Update() 
    { 
        if (fieldOfView.canSeePlayer == false)
        {
            if (randomSeek == true){
                if (at <= 10){
                    if (at > 9){
                        currentState = EnemyStates.Patrolling; 
                    }
                    at += Time.deltaTime * 1.1f;
                }
            }
            else {
                currentState = EnemyStates.Patrolling; 
            }
        } 
        else 
        { 
            currentState = EnemyStates.Chasing;
            at = 0;
        }

        if(currentState == EnemyStates.Patrolling) 
        {
            Debug.Log (currentWaypoint + " ; "+ Vector3.Distance(transform.position, waypoints[currentWaypoint].position));
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.8f)
            {
                //currentWaypoint++;
                currentWaypoint = Random.Range(0, waypoints.Length);
                if (currentWaypoint == waypoints.Length)
                {
                    currentWaypoint = 0;
                }
                animController.Play("Walking", 0, 0.25f);
                agent.speed = 2.5f;
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
     
    void SetDestination()
    {         
        if(currentState == EnemyStates.Chasing ){
            if (randomSeek == false && fieldOfView.playerRef != null){
                agent.SetDestination(fieldOfView.playerRef.transform.position);
            }else{
                fieldOfView.playerRef = fieldOfView.hitRef;
                agent.SetDestination(fieldOfView.playerRef.transform.position);
            }
            animController.Play("Run", 0, 0.25f);
            agent.speed = 4.3f;       
        }      
    }

     private void OnCollisionEnter(Collision collision)
     {
        if (collision.transform.name == "Player")
         {    
            Debug.Log ("Game Over");
         }
     }
}
