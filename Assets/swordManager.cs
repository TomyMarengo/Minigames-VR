using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class swordManager : MonoBehaviour
{

    public XRDirectInteractor rHand;
    public XRDirectInteractor lHand;

    public GameObject swordPrefab;
    public GameObject hoja;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.name == "Hoja") {
            Destroy(gameObject);
            Destroy(hoja);
            GameObject sword = Instantiate(swordPrefab, transform.position, transform.rotation);

            XRBaseInteractable swordInteractable = sword.GetComponent<XRBaseInteractable>();

            if (rHand.selectTarget.name == "Hoja") {
                lHand.interactionManager.ForceSelect(lHand, swordInteractable);
            }
            if (lHand.selectTarget.name == "Hoja") {
                rHand.interactionManager.ForceSelect(rHand, swordInteractable);
            }
        }
    }
}
