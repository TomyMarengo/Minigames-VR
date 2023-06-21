using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVr : MonoBehaviour
{

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private ConfigurableJoint joint;
    public UnityEvent onPress, onRelease;
    private Vector3 initialPosition;
    AudioSource sound;
    bool isPressed;
    Vector3 startPosition;
    
    void Start()
    {
        sound = GetComponent<AudioSource>();
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
        if (!isPressed && GetValue() + threshold >= 1)
            Pressed();

        if (isPressed && GetValue() - threshold <= 0)
            Released();
    }
    

    private void Pressed()
    {
        isPressed = true;
        onPress.Invoke();
        sound.Play();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onRelease.Invoke();
        sound.Play();
        Debug.Log("Released");
    }


}
