using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(FieldOfView))]
public class EnemyAvoid : MonoBehaviour
{
    
    NavMeshAgent agent;
    [SerializeField] float decisionDelay = 3f;
    //[SerializeField] Transform objectToChase;
    private FieldOfView fieldOfView;

    [SerializeField] Transform[] waypoints; int currentWaypoint = 0;
 
    enum EnemyStates {
        Moving, Avoid
    }

    [SerializeField] EnemyStates currentState;
     [SerializeField] Animator animController;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>(); 
        fieldOfView = GetComponent<FieldOfView>();
        InvokeRepeating("SetDestination", 0.5f, decisionDelay);  
        if(currentState == EnemyStates.Moving) {
            agent.SetDestination(waypoints[currentWaypoint].position); 
        }

    }

    void Update() 
    {   
        if (fieldOfView.canSeePlayer == false)
        {
            currentState = EnemyStates.Moving; 
            animController.Play("Run");
        } 
        else 
        { 
            currentState = EnemyStates.Avoid;
            animController.Play("Walking");
        }

        if(currentState == EnemyStates.Moving) 
        {
            if(Vector3.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.6f)
            {
                    currentWaypoint++;
                    if (currentWaypoint == waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }
            }
            agent.SetDestination(waypoints[currentWaypoint].position);
        }
    }
    private Transform startTransform;
    void SetDestination()
    {         
        if(currentState == EnemyStates.Avoid && fieldOfView.hitRef != null){
            // Set Distance 
            transform.rotation = Quaternion.LookRotation(transform.position - fieldOfView.hitRef.transform.position);
            Vector3 runTo = transform.position + transform.forward * 3;
            NavMeshHit hit;    // stores the output in a variable called hit
            NavMesh.SamplePosition(runTo, out hit, 5, 1 << NavMesh.GetNavMeshLayerFromName("Default")); 
           
            agent.SetDestination(hit.position);
        }          
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ememy"){
            Debug.Log (other.gameObject.name);
        }
    }
}
