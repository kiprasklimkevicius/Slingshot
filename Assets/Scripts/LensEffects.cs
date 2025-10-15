using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LensEffects : MonoBehaviour
{
    private Rigidbody player;

    public Volume volume;

    private LensDistortion lensDistortion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
    }

    // Update is called once per frame
    void Update()
    {
        // converting players speed into lens distortion
        float playerSpeed = player.linearVelocity.z;
        float t = playerSpeed / 50;
        float lensDistortionIntensityValue = Mathf.Lerp(0, -1, t);
        
        lensDistortion.intensity.value = lensDistortionIntensityValue;
        
    }
}
