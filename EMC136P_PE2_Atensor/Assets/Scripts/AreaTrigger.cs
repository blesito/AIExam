using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] FieldOfView fieldOfView;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 0){
            fieldOfView.playerRef = other.gameObject;
            fieldOfView.canSeePlayer = true;
            fieldOfView.GetComponent<NavMeshAgent>().speed = 5;
        }
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 0){
            fieldOfView.playerRef = other.gameObject;
            fieldOfView.canSeePlayer = true;
            fieldOfView.GetComponent<NavMeshAgent>().speed = 5;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        fieldOfView.playerRef = null;
        fieldOfView.canSeePlayer = false;
        fieldOfView.GetComponent<NavMeshAgent>().speed = 3.5f;
    }
}
