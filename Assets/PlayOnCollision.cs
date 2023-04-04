using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnCollision : MonoBehaviour
{
	public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
    }

    void OnCollisionEnter(Collision other) {
		GetComponent<AudioSource>().Play();
	} 
}
