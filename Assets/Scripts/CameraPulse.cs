using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraPulse : MonoBehaviour
{
    public GameObject slend;
    private Vector3 slender_position;
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!postProcessVolume.profile.TryGetSettings(out vignette))
        {
            Debug.LogError("No Vignette effect found in PostProcessVolume.");
            return;
        }
        if (!postProcessVolume.profile.TryGetSettings(out chromaticAberration))
        {
            Debug.LogError("No Vignette effect found in PostProcessVolume.");
            return;
        }
        if (!postProcessVolume.profile.TryGetSettings(out bloom))
        {
            Debug.LogError("No Vignette effect found in PostProcessVolume.");
            return;
        }
        vignette.intensity.value = 0;
        chromaticAberration.intensity.value = 0;
        bloom.intensity.value = 0;
        slender_position = GameObject.Find("Slender").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (slend.GetComponent<Movement>().IsInFront(slender_position)) {
            vignette.intensity.value += 0.2f * Time.deltaTime;
            chromaticAberration.intensity.value = Mathf.PingPong(Time.time * 2f, 1.0f);
            bloom.intensity.value += 5f * Time.deltaTime;
        }
        else {
            if (vignette.intensity.value > 0) {
                vignette.intensity.value -= 0.2f * Time.deltaTime ;  
            }
            if (chromaticAberration.intensity.value >=0 ) {
                chromaticAberration.intensity.value -= 0.01f * Time.deltaTime;
            }
            if (bloom.intensity.value >= 0) {
                bloom.intensity.value -= 20f * Time.deltaTime ;
            }
        }
    }
}
