
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
    private bool hasReached = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.gameObject.tag == "Player")
        {
            transform.Rotate(rotation, 0f, 0f, Space.Self);
            if ((Mathf.Abs(transform.localRotation.eulerAngles.x) % 360f) < rotationSnapThreshold)
            {
                GreekRomanPuzzleManager.mapPuzzle +=1;
                hasReached = true;

                if (!GreekRomanPuzzleManager.leverPuzzle || GreekRomanPuzzleManager.mapPuzzle == 1) {
                    audioSource.Play();
                }
                else {
                    GreekRomanPuzzleManager.CheckStatus();
                }
                
            }
            else
            {
                if (hasReached) {
                    GreekRomanPuzzleManager.mapPuzzle -= 1;
                    hasReached = false;
                }
                
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

