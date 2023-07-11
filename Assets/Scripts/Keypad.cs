using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    private int[] passwordBuffer = new int[4]; // Password buffer to store the pressed keys
    private int bufferIndex = 0; // Current index in the password buffer
    private SphereCollider sphereLeft;
    private SphereCollider sphereRight;
    private BoxCollider boxLeft;
    private BoxCollider boxRight;

    private void Start()
    {
        ClearBuffer();
        sphereLeft = null;
        sphereRight = null;

        boxLeft = null;
        boxRight = null;
    }
    public void PressKey(int keyValue)
    {
        if (keyValue == 10)
        {
            CheckPassword();
        }
        if (bufferIndex < passwordBuffer.Length)
        {
            // Check if the password buffer is full
            

            passwordBuffer[bufferIndex] = keyValue;
            bufferIndex++;

            Debug.Log("Key pressed: " + keyValue);

        }
    }

    private void CheckPassword()
    {
        // Compare the entered password with the correct password
        int[] correctPassword = { 1, 2, 3, 4 };
        bool isPasswordCorrect = true;

        for (int i = 0; i < passwordBuffer.Length; i++)
        {
            if (passwordBuffer[i] != correctPassword[i])
            {
                isPasswordCorrect = false;
                break;
            }
        }

        if (isPasswordCorrect)
        {
            Debug.Log("Password correct!");
            // Perform actions when the correct password is entered
        }
        else
        {
            Debug.Log("Password incorrect!");
            // Perform actions when the incorrect password is entered
        }

        // Clear the password buffer
        ClearBuffer();
    }

    private void ClearBuffer()
    {
        bufferIndex = 0;
        for (int i = 0; i < passwordBuffer.Length; i++)
        {
            passwordBuffer[i] = 0;
        }
    }

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Right Direct Controller")
        {
            // Check if the collider is a sphere collider
            sphereRight = other.GetComponent<SphereCollider>();
            sphereRight.enabled = false;

            boxRight = other.GetComponent<BoxCollider>();
            boxRight.enabled = true;
        }

        if (other.gameObject.name == "Left Direct Controller")
        {
            // Check if the collider is a sphere collider
            sphereLeft = other.GetComponent<SphereCollider>();
            sphereLeft.enabled = false;

            boxLeft = other.GetComponent<BoxCollider>();
            boxLeft.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Left Direct Controller")
        {
            boxLeft.enabled = false;
            sphereLeft.enabled = true;
        }

        if (other.gameObject.name == "Right Direct Controller")
        {
            // Check if the collider is a mesh collider
            boxRight.enabled = false;
            sphereRight.enabled = true;
            
        }
    }


}
