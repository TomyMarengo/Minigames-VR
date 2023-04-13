using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class ResetBall : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rigidBody;

    private XRGrabInteractable grabInteractable;
    public bool isGrabbed = false;

    void Start()
    {
        // Store the original position and rotation
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;

        // Get the rigidbody component
        rigidBody = GetComponent<Rigidbody>();

        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    // Function to reset the ball's position, rotation, and velocity
    public void ResetB()
    {
        // Set the ball's position and rotation to their original values
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;

        // Reset the ball's velocity and angular velocity
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    // Coroutine to wait for 3 seconds before resetting the ball
    IEnumerator ResetBallAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        // Reset the ball
        ResetB();
    }




    private void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Pin")
        {
            StartCoroutine(ResetBallAfterDelay());
        }
        else if (other.gameObject.tag == "border")
        {
            ResetB();
        }
    }
}
