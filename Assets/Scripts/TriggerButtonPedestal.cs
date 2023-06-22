using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonPedestal : MonoBehaviour
{
    public GameObject pedestal;
    public Transform initialPosition;
    public Transform targetPosition;
    public float liftingDuration = 2f;

    private Vector3 initPos;
    private Vector3 targetPos;

    private bool hasInteracted = false;

    // Start is called before the first frame update
    void Start()
    {
        initPos = pedestal.transform.position;
        targetPos = targetPosition.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has interacted with the pedestal");
            StartCoroutine(LiftPedestal());
            hasInteracted = true;
        }
    }

    private IEnumerator LiftPedestal()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = initialPosition.position;
        Vector3 targetPosition = this.targetPosition.position;

        while (elapsedTime < liftingDuration)
        {
            float t = elapsedTime / liftingDuration;
            pedestal.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the pedestal reaches the target position precisely
        pedestal.transform.position = targetPosition;
    }

    // on trigger exit to reset the pedestal at same speed as it was lifted
    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has left the pedestal");
            StartCoroutine(ResetPedestal());
            hasInteracted = false;
        }
    }

    private IEnumerator ResetPedestal()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = this.targetPos;
        Vector3 targetPosition = this.initPos;

        while (elapsedTime < liftingDuration)
        {
            float t = elapsedTime / liftingDuration;
            pedestal.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the pedestal reaches the target position precisely
        pedestal.transform.position = targetPosition;
    }


}



