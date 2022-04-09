using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HotSpotTrigger : MonoBehaviour
{
    [SerializeField] GameObject TargetEnemy;
    private void OnTriggerStay(Collider other)
    {

        other.gameObject.GetComponent<FieldOfView>().playerRef = TargetEnemy;
    }

    private void OnTriggerEnter(Collider other)
    {
       other.gameObject.GetComponent<FieldOfView>().playerRef = TargetEnemy;
    }


    private void OnTriggerExit(Collider other)
    {
       other.gameObject.GetComponent<FieldOfView>().playerRef = TargetEnemy;
    }
}
