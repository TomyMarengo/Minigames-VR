using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSarchophagus : MonoBehaviour
{


    public Renderer renderer;

    private bool hasInteracted = false;
    public float fadeSpeed = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has interacted with the pedestal");
            StartCoroutine(fadeOut());
            hasInteracted = true;
        }
    }

    private IEnumerator fadeOut()
    {

        while (this.renderer.material.color.a > 0)
        {
            Color objectColor = this.renderer.material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.renderer.material.color = objectColor;
            Debug.Log("fading out " + this.renderer.material.color.a);
            yield return null;
        }

        

    }

    // on trigger exit to reset the pedestal at same speed as it was lifted
    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has left the pedestal");
            StartCoroutine(fadeIn());
            hasInteracted = false;
        }
    }

    private IEnumerator fadeIn()
    {

        while (this.renderer.material.color.a < 1)
        {
            Color objectColor = this.renderer.material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.renderer.material.color = objectColor;
            Debug.Log("fading in" + this.renderer.material.color.a);
            yield return null;
        }

    }
}
