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
        float playerSpeed = player.linearVelocity.z;
        Debug.Log("player speed: " + playerSpeed);
        
        float t = playerSpeed / 50;
        Debug.Log("tvalue: " + t);
        float lensDistortionIntensityValue = Mathf.Lerp(0, -1, t);
        Debug.Log("Lens Distortion intensity: " + lensDistortionIntensityValue);
        lensDistortion.intensity.value = lensDistortionIntensityValue;
        
    }
}
