using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    private Animator _buttonAnimator;
    // Start is called before the first frame update
    void Start()
    {
        _buttonAnimator = GetComponent<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        _buttonAnimator.SetTrigger("ButtonPressed");
    }
}
