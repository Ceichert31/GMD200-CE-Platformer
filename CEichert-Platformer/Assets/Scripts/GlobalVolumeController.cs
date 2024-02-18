using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class GlobalVolumeController : MonoBehaviour
{
    //This script allows volume profile values to be changed through animation and code

    private VolumeProfile volume;

    private Bloom bloom;

    private ChromaticAberration aberration;

    [SerializeField] private float bloomIntensity = 1f;

    [SerializeField] private float aberrationIntensity = 0f;

    [SerializeField] private bool isEnabled = true;

    private bool isVisuals;

    public delegate void SlowTimeEffects();
    public static SlowTimeEffects timeEffects;
    void Start()
    {
        volume = GetComponent<Volume>().profile;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnabled) return;
        if (volume == null) return;

        //Get Bloom from volume profile
        if (bloom == null) volume.TryGet(out bloom);
        if (bloom == null) return;

        //Get Chromatic Aberration from volume profile
        if (aberration == null) volume.TryGet(out aberration);
        if (aberration == null) return;

        //Set Values
        bloom.intensity.value = bloomIntensity;
        aberration.intensity.value = aberrationIntensity;
    }

    void SlowTimeVisuals()
    {
        isVisuals = !isVisuals;
        if (isVisuals)
        {
            bloomIntensity = 3f;
            aberrationIntensity = 0.2f;
        }
        else
        {
            bloomIntensity = 1f;
            aberrationIntensity = 0;
        }
    }
    private void OnEnable()
    {
        timeEffects += SlowTimeVisuals;
    }
    private void OnDisable()
    {
        timeEffects -= SlowTimeVisuals;
    }
}
