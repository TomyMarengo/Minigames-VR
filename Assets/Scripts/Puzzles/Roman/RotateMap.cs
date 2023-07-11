
using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class RotateMap : MonoBehaviour
{
    public float rotation = 10f;
    private bool hasInteracted = false;
    public float rotationSnapThreshold = 10f;
    public AudioSource audioSource;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.gameObject.tag == "Player")
        {
            transform.Rotate(rotation, 0f, 0f, Space.Self);
            Debug.Log("ROTATING to " + transform.localRotation.eulerAngles.x.ToString());
            if ((Mathf.Abs(transform.localRotation.eulerAngles.x) % 360f) < rotationSnapThreshold)
            {
                Debug.Log("Correct position reached");
                GreekRomanPuzzleManager.mapPuzzle = true;
                if (!GreekRomanPuzzleManager.leverPuzzle) {
                    audioSource.Play();
                }
                
            }
            else
            {
                GreekRomanPuzzleManager.mapPuzzle = false;
            }

            hasInteracted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            hasInteracted = false;
        }
    }
}

