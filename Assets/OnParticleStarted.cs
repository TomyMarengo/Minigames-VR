using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnParticleStarted : MonoBehaviour
{
	public BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
		boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<ParticleSystem>().isPlaying) {
			boxCollider.enabled = true;
		}
		else {
			boxCollider.enabled = false;
		}
    }
}
