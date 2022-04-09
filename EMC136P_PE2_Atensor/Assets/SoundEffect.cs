using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoundEffect: MonoBehaviour{

    public AudioClip triggerSound;
    AudioSource audioSource;


    void Start(){
        audioSource = GetComponent<AudioSource>();

    }
    void Update(){

    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerSound != null)
        {
            audioSource.PlayOneShot(triggerSound,0.7f);
        }
    }
}
