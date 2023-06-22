using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSarchophagus : MonoBehaviour
{
    // Start is called before the first frame update
    Light[] lights;
    bool hasInteracted = false;
    private void Start()
    {
        // Find all lights in the scene
        lights = FindObjectsOfType<Light>();

        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has interacted with the sarchophagus");
            StartCoroutine(turnOffLights());
            hasInteracted = true;
        }
    }

    private IEnumerator turnOffLights()
    {
        // Loop through each light and turn it off
        foreach (Light light in lights)
        {
            light.intensity = 0f;
        }
        yield return null;
    }

    // on trigger exit to reset the pedestal at same speed as it was lifted
    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has left the sarchophagus");
            StartCoroutine(resetLights());
            hasInteracted = false;
        }
    }

    private IEnumerator resetLights()
    {
        // Loop through each light and turn it on
        foreach (Light light in lights)
        {
            light.intensity = 1f;
        }
        yield return null;
    }
}
