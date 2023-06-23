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
    public Cubemap[] brightReflectionProbes;
    public Cubemap[] darkReflectionProbes;

    private LightmapData[] darkLightmap, brightLightmap;

    private void Start()
    {
        List<LightmapData> dlightmap = new List<LightmapData>();


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
            StartCoroutine(fadeOut());
            StartCoroutine(changeLightMap(darkLightmap, darkReflectionProbes));
            hasInteracted = true;
        }
    }

    private IEnumerator fadeOut()
    {

        while (this.renderer.material.color.a > 0)
        {
            Color objectColor = this.renderer.material.color;
            float fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);
            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.renderer.material.color = objectColor;
            Debug.Log("fading out " + this.renderer.material.color.a);
            yield return null;
        }

        

    }

    // on trigger exit to reset the pedestal at same speed as it was lifted
    private void OnTriggerExit(Collider other)
    {
        if (hasInteracted && other.CompareTag("Player"))
        {
            Debug.Log("Player has left the pedestal");
            StartCoroutine(fadeIn());
            StartCoroutine(changeLightMap(brightLightmap, brightReflectionProbes));
            hasInteracted = false;
        }
    }

    private IEnumerator fadeIn()
    {

        while (this.renderer.material.color.a < 1)
        {
            Color objectColor = this.renderer.material.color;
            float fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

            objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
            this.renderer.material.color = objectColor;
            Debug.Log("fading in" + this.renderer.material.color.a);
            yield return null;
        }

    }
}
