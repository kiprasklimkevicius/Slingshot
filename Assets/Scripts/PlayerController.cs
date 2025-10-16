using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initSpeed = 10;
    public Vector3 lateralSpeed = new Vector3(5,0,0); // lateral speed
    public float lateralSpeedInGravityField = 4;
    public float topSpeed;
    public GameObject projectile;
    private float xPosProjectileSpawn = 0.5f;
    public float powerUpFuel = 30;
    public float blackHoleMass = 20;
    private Rigidbody rigidBody;
    public float gravityConstant = 0.1f;
    public bool gameOver;
    private GameManager gameManager;
    public float xPull = 1.5f;
    public float fuelGauge;
    public float fuelDischargeRatePerSecond = 50;
    public float fuelSpeed = 10;
    private float timeSpentFueling = 0;
    private AudioSource laserShotAudio;
    private AudioSource engineOffAudio;
    private AudioSource engineOnAudio;
    private AudioSource engineCrashAudio;
    private AudioSource rocketCrashAudio;
    private AudioSource[] sounds;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3.up is moving forward, because capsule is rotated 90 around x-axis;
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(new Vector3(0, 0, initSpeed), ForceMode.Impulse);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        laserShotAudio = GetComponent<AudioSource>();
        fuelGauge = Mathf.Clamp(fuelGauge, 0, 100);
        fuelGauge = 50;

        topSpeed = 0;
        
        sounds = GetComponents<AudioSource>();
        // 0, 1, 2... is the order that these audio sources are assigned to the 'Player' Game Object
        laserShotAudio = sounds[0];
        engineOffAudio = sounds[1];
        engineOnAudio = sounds[2];
        engineCrashAudio = sounds[3];
        rocketCrashAudio = sounds[4];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        
        LateralMovement();
        
        if (Input.GetKeyDown(KeyCode.Space)) ShootProjectile();
        
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) SwitchToEngineOnAudio();
        if (Input.GetKeyUp(KeyCode.LeftShift)) SwitchToEngineOffAudio();
        if (Input.GetKey(KeyCode.LeftShift)) UseFuel();

        if (rigidBody.linearVelocity.z > topSpeed) topSpeed = rigidBody.linearVelocity.z;

    }

    void LateralMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rigidBody.AddForce(lateralSpeed, ForceMode.Impulse);
        } if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rigidBody.AddForce(-lateralSpeed, ForceMode.Impulse);
        }
    }

    void UseFuel()
    {
        if (fuelGauge > 0)
        {
            rigidBody.AddForce(Vector3.forward * fuelSpeed * Time.deltaTime, ForceMode.Force);
            engineOffAudio.pitch += 0.5f * Time.deltaTime;
            fuelGauge -= fuelDischargeRatePerSecond * Time.deltaTime;
            timeSpentFueling += Time.deltaTime;
            Debug.Log("Time spent using Fuel: " + timeSpentFueling);
        }

    }
    

    void ShootProjectile()
    {
        if (xPosProjectileSpawn == 0.5f) xPosProjectileSpawn--;
        else xPosProjectileSpawn++;
        Instantiate(projectile, transform.position + Vector3.right * xPosProjectileSpawn, projectile.transform.rotation);
        laserShotAudio.Play();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameOver) return;
        if (other.gameObject.CompareTag("Deadly"))
        {
            GameOver();
        } else WinGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            //Destroy the power up and pick up fuel
            Destroy(other.gameObject);
            fuelGauge += powerUpFuel;
        }
        if (other.CompareTag("Gravity"))
        {
            lateralSpeed.x -= lateralSpeedInGravityField;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            lateralSpeed.x += lateralSpeedInGravityField;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            GravityPull(other.gameObject);
        }
    }

    void GravityPull(GameObject gravity)
    {
        Vector3 vectorToObject = (gravity.transform.position - transform.position);
        float distance = vectorToObject.magnitude;
        
        float forceIntensity = gravityConstant * GetObjectMass(gravity) / (distance);
        Vector3 forceToApply = vectorToObject.normalized * forceIntensity;
        forceToApply.x *= xPull; // Test maybe feels better 
        rigidBody.AddForce(forceToApply * Time.deltaTime, ForceMode.Force);
    }
    
    float GetObjectMass(GameObject obj)
    {
        Rigidbody rb = obj.GetComponentInParent<Rigidbody>();
        rb.mass = blackHoleMass;
        return rb.mass;
    }
    
    void SwitchToEngineOnAudio()
    {
        //engineOffAudio.pitch = 1.5f;
        // engineOnAudio.Play();
    }

    void SwitchToEngineOffAudio()
    {
        engineOffAudio.pitch = 1;
        // engineOnAudio.Pause();
        // engineOffAudio.Play();
    }

    void GameOver()
    {
        engineOffAudio.pitch = 0.8f;
        rocketCrashAudio.Play();
        gameOver = true;
        gameManager.ShowGameOverText();
    }

    void WinGameOver()
    {
        engineOffAudio.pitch = 0.8f;
        gameManager.ShowVictoryScreen();
        gameOver = true;
    }
}
