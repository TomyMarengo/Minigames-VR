using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    private enum Status {UP=1, DOWN, MIDDLE};
    public float upRotation = 40f; // Adjust this value based on the lever's "up" rotation
    public float downRotation = -40f; // Adjust this value based on the lever's "down" rotation

    public XRGrabInteractable[] leverInteractables;
    private Transform[] leverTransforms;
    private Status[] leversStatus;

    public AudioSource audioSource;
    private bool hasInteracted = false;


    public GameObject door;
    public AudioClip resolved;


    // Start is called before the first frame update
    void Start()
    {
        GreekRomanPuzzleManager.Initialize(door, resolved);

        leverTransforms = new Transform[leverInteractables.Length];
        leversStatus = new Status[leverInteractables.Length];

        // Cache the lever transforms
        for (int i = 0; i < leverInteractables.Length; i++)
        {
            leverTransforms[i] = leverInteractables[i].transform;
            leversStatus[i] = Status.MIDDLE;
        }
    }

    private bool CheckLevers()
    {
        for (int i = 1; i < leversStatus.Length; i++)
        {
            if(leversStatus[i] != Status.DOWN)
            {
                return false;
            }

        }
        if (leversStatus[0] == Status.UP)
            return true;
        return false;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Iterate through each lever
        GreekRomanPuzzleManager.leverPuzzle = CheckLevers();
        
        if (!hasInteracted && GreekRomanPuzzleManager.leverPuzzle && GreekRomanPuzzleManager.mapPuzzle != 2) {
            audioSource.Play();
            hasInteracted = true;
        }
        else {
            GreekRomanPuzzleManager.CheckStatus();
        }

        for (int i = 0; i < leverInteractables.Length; i++)
        {
            XRGrabInteractable leverInteractable = leverInteractables[i];
            Transform leverTransform = leverTransforms[i];

            // Check the lever's rotation using local rotation
            float leverRotation = leverTransform.localRotation.x * 100; // Adjust this line based on your implementation

            // Determine if the lever is up or down based on the rotation threshold
            bool isLeverUp = (leverRotation >= upRotation);
            bool isLeverDown = (leverRotation <= downRotation);

            // Use the lever's rotation status for your puzzle logic
            if (isLeverUp)
            {
                if (leversStatus[i] != Status.UP)
                {
                    leversStatus[i] = Status.UP;
                }
                // Lever is up
                // Perform actions or trigger events related to the lever being up
            }
            else if (isLeverDown)
            {
                if (leversStatus[i] != Status.DOWN)
                {
                    leversStatus[i] = Status.DOWN;
                }
                // Lever is down
                // Perform actions or trigger events related to the lever being down
            }
            else
            {
                if( leversStatus[i] != Status.MIDDLE)
                {
                    leversStatus[i] = Status.MIDDLE;
                }
            }
        }

        
    }
}
