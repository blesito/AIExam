using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DelayOnStart : MonoBehaviour
{
    public UnityEvent InitOnStart;
    public float secs = 10;
    public void Start (){
        StartCoroutine(OnDelayEnabled());
    }
    IEnumerator OnDelayEnabled()
    {
        yield return new WaitForSeconds(secs);
        InitOnStart.Invoke();
    }
}
