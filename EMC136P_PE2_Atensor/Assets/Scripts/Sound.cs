using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource Bump;

    void OnCollisionEnter(Collision collision)
    {
        Bump.Play();
    }
  
}