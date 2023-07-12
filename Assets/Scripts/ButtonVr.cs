using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.05f;
    public GameObject button;
    private Keypad keypad;
    public int value;

    public UnityEvent onPress, onRelease;
    AudioSource sound;
    bool isPressed;
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
        keypad = GetComponentInParent<Keypad>();
        isPressed = false;
    }
 

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject.CompareTag("Player"))
        {
            button.transform.Translate(new Vector3(0, 0f, 0.015f));
            Pressed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed && other.gameObject.CompareTag("Player"))
        {
            button.transform.Translate(new Vector3(0, 0f, -0.015f));
            Released();
        }
    }


    private void Pressed()
    {
        isPressed = true;
        keypad.PressKey(value);
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        Debug.Log("Released");
    }


}
