using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class LensEffects : MonoBehaviour
{
    private Rigidbody player;

    public Volume volume;
    private GameManager gameManager;

    private LensDistortion lensDistortion;

    private LensEffects instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!gameManager.startGame) return;
        GameObject.FindWithTag("Player").TryGetComponent <Rigidbody>(out player);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("onSceneLoad");
        if (!gameManager.startGame) return;
        GameObject.FindWithTag("Player").TryGetComponent<Rigidbody>(out player);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.startGame) return;
        // converting players speed into lens distortion
        float playerSpeed = player.linearVelocity.z;
        float t = playerSpeed / 50;
        float lensDistortionIntensityValue = Mathf.Lerp(0, -1, t);
        
        lensDistortion.intensity.value = lensDistortionIntensityValue;
        
    }
}
