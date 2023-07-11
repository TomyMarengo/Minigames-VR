using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialsUp : MonoBehaviour
{
    private Vector3 initialPosition;
    public Material[] materials;
    public GameObject[] objects;
    public AudioClip audioClick;
    public AudioClip resolved;
    private bool canPressButton = true;
    public float cooldownTime = 0.3f;
    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        MaterialStateManager.Initialize(door, resolved);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canPressButton)
        {
            // Mover el botón hacia abajo
            AudioSource.PlayClipAtPoint(audioClick, transform.position);
            transform.Translate(new Vector3(-0.009f, 0f, -0.006f));

            MaterialStateManager.actualMaterial[0] = (MaterialStateManager.actualMaterial[0] + 3) % 5;
            MaterialStateManager.actualMaterial[1] = (MaterialStateManager.actualMaterial[1] + 1) % 5;
            MaterialStateManager.actualMaterial[2] = (MaterialStateManager.actualMaterial[2] + 3) % 5;
            MaterialStateManager.actualMaterial[3] = (MaterialStateManager.actualMaterial[3] + 0) % 5;

            Debug.Log(MaterialStateManager.actualMaterial);

            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].GetComponent<MeshRenderer>().material = materials[MaterialStateManager.actualMaterial[i]];
            }

            MaterialStateManager.CheckStatus();
            
            canPressButton = false;
            Invoke(nameof(ResetButton), cooldownTime);
        }
    }

    private void ResetButton()
    {
        canPressButton = true;
        transform.position = initialPosition;
    }
}
