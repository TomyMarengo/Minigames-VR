using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{

    [SerializeField] private float threshold = 0.001f;
    [SerializeField] private float deadZone = 0.05f;
    private Keypad keypad;
    public int value;

    private ConfigurableJoint joint;
    public UnityEvent onPress, onRelease;
    private Vector3 initialPosition;
    AudioSource sound;
    bool isPressed;
    Vector3 startPosition;
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
        keypad = GetComponentInParent<Keypad>();
        startPosition = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
        isPressed = false;
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPosition, transform.localPosition) / joint.linearLimit.limit;

        if (Mathf.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() >= threshold)
            Pressed();

        if (isPressed && GetValue() <=  1 - threshold)
            Released();
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
