using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCharacter : MonoBehaviour {
    public GameObject character;
    public AudioClip audioClip;
    public Transform targetPos;
    private AudioSource audioSource;
    private Transform initPos;
    
    public float velocidadMovimiento = 1.0f;
    public float duracionMovimiento = 1.0f;

    private bool moving = false;
    private bool hasInteracted = false;
    private float elapsedTime = 0.0f;

    private void Start()
    {
        initPos = character.transform;
        audioSource = GetComponent<AudioSource>();
        Debug.Log("initPos: " + initPos + "targetPos: " + targetPos);
    }


    private void OnTriggerEnter(Collider other) {
        if (!hasInteracted && other.CompareTag("Player")) {
            moving = true;
            hasInteracted = true;
            elapsedTime = 0.0f;
        } 
    }

    private void Update() {
        if (moving) {
            // Interpolar la posición del samurai entre initPos y targetPos
            float t = Mathf.Clamp01(elapsedTime / duracionMovimiento);
            character.transform.position = Vector3.Lerp(initPos.position, targetPos.position, t);

            // Incrementar el tiempo transcurrido
            elapsedTime += Time.deltaTime * velocidadMovimiento;

            // Comprobar si se ha alcanzado la posición objetivo
            if (t >= 1.0f) {
                moving = false;
                if (audioClip != null){
                    audioSource.PlayOneShot(audioClip);
                }
            }
        }
    }
}
