using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    private int[] passwordBuffer = new int[4]; // Password buffer to store the pressed keys
    private int bufferIndex = 0; // Current index in the password buffer
    private SphereCollider sphereLeft;
    private SphereCollider sphereRight;
    private BoxCollider boxLeft;
    private BoxCollider boxRight;

    public TextMeshProUGUI text;
    public GameObject canvas;
    public AudioClip audioClick;

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
        AudioSource.PlayClipAtPoint(audioClick, transform.position);
        if (keyValue == 10)
        {
            CheckPassword();
            return;
        }
        if (bufferIndex < passwordBuffer.Length)
        {
            // Check if the password buffer is full
            

            passwordBuffer[bufferIndex] = keyValue;
            bufferIndex++;
            ChangeScreenText(passwordBuffer);

        }
    }

    public string ConvertIntArrayToString(int[] digits)
    {
        if (digits == null || digits.Length == 0)
        {
            return "0";
        }

        string numberString = string.Join("", digits);

        return numberString;
    }

    private void ChangeScreenText(int[] new_text)
    {

        text.text = ConvertIntArrayToString(new_text);
    }

    private void CheckPassword()
    {
        // Compare the entered password with the correct password
        int[] correctPassword = { 1, 3, 4, 2 };
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
            EgyptianPuzzleManager.keypadPuzzle = true;
            EgyptianPuzzleManager.CheckStatus();
            Destroy(canvas);
            Destroy(gameObject);

            // Perform actions when the correct password is entered
        }
        else
        {
            ClearBuffer();
            // Perform actions when the incorrect password is entered
        }
    }

    private void ClearBuffer()
    {
        bufferIndex = 0;
        for (int i = 0; i < passwordBuffer.Length; i++)
        {
            passwordBuffer[i] = 0;
        }
        ChangeScreenText(passwordBuffer);
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
