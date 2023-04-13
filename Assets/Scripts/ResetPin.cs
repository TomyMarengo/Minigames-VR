using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPin : MonoBehaviour
{
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rigidBody;

    public ScoreBoard scoreBoard;
    private bool knocked = false;

    void Start()
    {
        // Store the original position and rotation
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;

        // Get the rigidbody component
        rigidBody = GetComponent<Rigidbody>();
    }

    // Function to reset the ball's position, rotation, and velocity
    public void ResetP()
    {
        // Set the ball's position and rotation to their original values
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;

        // Reset the ball's velocity and angular velocity
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        scoreBoard.score += 1;
        scoreBoard.UpdateScore();
        knocked = false;
    }

    // Coroutine to wait for 3 seconds before resetting the ball
    IEnumerator ResetPinAfterDelay()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3f);

        
        // Reset the ball
        ResetP();
    }

    public float RotationDifference()
    {
        return Quaternion.Angle(gameObject.transform.rotation, originalRotation);
    }

    // Function to detect when the throw is finished
    void Update()
    {
        // Check if the ball has fallen off the lane or reached the pins
        if (!knocked && RotationDifference() > 5)
        {
            knocked = true;
            // Start the coroutine to reset the ball after a delay
            StartCoroutine(ResetPinAfterDelay());
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        /*if(other.gameObject.name == "Pin" || other.gameObject.name == "Ball" || other.gameObject.tag == "border" || other.gameObject.tag == "outerfloor")
        {
            StartCoroutine(ResetPinAfterDelay());
        }*/
    }
}