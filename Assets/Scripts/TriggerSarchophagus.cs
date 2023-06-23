using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSarchophagus : MonoBehaviour
{


    public Renderer renderer;

    private bool hasInteracted = false;
    public float fadeSpeed = 1f;

    public Texture2D[] darkLightmapDir, darkLightmapColor;
    public Texture2D[] brightLightmapDir, brightLightmapColor;
    public Texture2D[] singleLightmapDir, singleLightmapColor;

    public Renderer rendererNew;

    public AudioSource audioSource;
    public Cubemap[] brightReflectionProbes;
    public Cubemap[] darkReflectionProbes;
    public Cubemap[] singleReflectionProbes;

    bool singleLight = false;
    private float elapsedTime = 0f;
    public float changeDuration = 1f;

    private LightmapData[] darkLightmap, brightLightmap, singleLightmap;

    public GameObject pedestal;
    public Transform initialPosition;
    public Transform targetPosition;
    public float liftingDuration = 2f;

    private Vector3 initPos;
    private Vector3 targetPos;

    private void Start()
    {
        List<LightmapData> dlightmap = new List<LightmapData>();

        initPos = pedestal.transform.position;
        targetPos = targetPosition.transform.position;

        for (int i = 0; i < darkLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = darkLightmapDir[i];
            lmdata.lightmapColor = darkLightmapColor[i];

            dlightmap.Add(lmdata);
        }

        darkLightmap = dlightmap.ToArray();

        List<LightmapData> blightmap = new List<LightmapData>();

        for (int i = 0; i < brightLightmapDir.Length; i++)
        {
            LightmapData lmdata = new LightmapData();

            lmdata.lightmapDir = brightLightmapDir[i];
            lmdata.lightmapColor = brightLightmapColor[i];

            blightmap.Add(lmdata);
        }

        brightLightmap = blightmap.ToArray();

        List<LightmapData> slightmap = new List<LightmapData>();


        for (int i = 0; i < singleLightmapDir.Length; i++)
        {
            LightmapData smdata = new LightmapData();

            smdata.lightmapDir = singleLightmapDir[i];
            smdata.lightmapColor = singleLightmapColor[i];

            slightmap.Add(smdata);
        }

        singleLightmap = slightmap.ToArray();
    }

    private IEnumerator changeLightMap(LightmapData[] lightmapData, Cubemap[] probes)
    {
        LightmapSettings.lightmaps = lightmapData;
        Cubemap[] reflectionProbes = FindObjectsOfType<Cubemap>();
        for(int i = 0; i < probes.Length; i++)
        {
            Destroy(reflectionProbes[i]);
            Instantiate(probes[i]);
        }
        yield return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has interacted with the pedestal");
            audioSource.Play();
            StartCoroutine(fadeOut(renderer));
            StartCoroutine(LiftPedestal());
            StartCoroutine(changeLightMap(darkLightmap, darkReflectionProbes));
            hasInteracted = true;
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("entro");
            if (elapsedTime > changeDuration)
            {
                if (singleLight)
                {
                    StartCoroutine(changeLightMap(darkLightmap, darkReflectionProbes));
                    singleLight = false;
                }
                else
                {
                    singleLight = true;
                    StartCoroutine(changeLightMap(singleLightmap, singleReflectionProbes));
                }
                elapsedTime = 0f;
            }
            elapsedTime += Time.deltaTime;
        }
    }

    private IEnumerator fadeOut(Renderer rend)
    {

        while (rend.material.color.a > 0)
        {
            Color objectColor = rend.material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            rend.material.color = objectColor;
            Debug.Log("fading out " + rend.material.color.a);
            yield return null;
        }

        

    }

    // on trigger exit to reset the pedestal at same speed as it was lifted
    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has left the pedestal");
            audioSource.Stop();
            StartCoroutine(fadeIn(renderer));
            StartCoroutine(ResetPedestal());
            StartCoroutine(changeLightMap(brightLightmap, brightReflectionProbes));
            hasInteracted = false;
        }
    }

    private IEnumerator fadeIn(Renderer rend)
    {

        while (rend.material.color.a < 1)
        {
            Color objectColor = rend.material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            rend.material.color = objectColor;
            Debug.Log("fading in" + rend.material.color.a);
            yield return null;
        }

    }

    private IEnumerator LiftPedestal()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = initialPosition.position;
        Vector3 targetPosition = this.targetPosition.position;

        while (elapsedTime < liftingDuration)
        {
            float t = elapsedTime / liftingDuration;
            pedestal.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the pedestal reaches the target position precisely
        pedestal.transform.position = targetPosition;
    }

    private IEnumerator ResetPedestal()
    {
        float elapsedTime = 0f;
        Vector3 startPosition = this.targetPos;
        Vector3 targetPosition = this.initPos;

        while (elapsedTime < liftingDuration)
        {
            float t = elapsedTime / liftingDuration;
            pedestal.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the pedestal reaches the target position precisely
        pedestal.transform.position = targetPosition;
    }
}
