using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerSlowMotion : MonoBehaviour
{
    public PostProcessVolume PostProcessVolume;
    private Vignette Vignette;

    void Start()
    {
        PostProcessVolume.profile.TryGetSettings(out Vignette);
        Vignette.smoothness.value = 0.6f;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vignette.smoothness.value = 1f;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vignette.smoothness.value = 0.6f;
        }
    }
}
