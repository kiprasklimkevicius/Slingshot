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
    private Rigidbody rigidBody;
    public bool gameOver;
    private GameManager gameManager;
    public float xPull = 1.5f;
    public float fuelGauge;
    public float fuelDischargeRatePerSecond = 50;
    public float fuelSpeed = 10;
    private float timeSpentFueling = 0;
    private AudioSource laserShotAudio;
    private AudioSource engineOffAudio;
    private AudioSource rocketCrashAudio;
    private AudioSource fuelUpAudio;
    private AudioSource[] sounds;
    private float speedOnLastFrame;
    private float timeKeeperForLastFrame;
    private float negativeXRange;
    private float positiveXRange;
    
    
    
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
        rocketCrashAudio = sounds[2];
        fuelUpAudio = sounds[3];
        
        negativeXRange = GameObject.Find("Wall-Left").transform.position.x;
        positiveXRange = GameObject.Find("Wall-Right").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        
        LateralMovement();
        
        if (Input.GetKeyDown(KeyCode.Space)) ShootProjectile();
        
        
        if (Input.GetKeyUp(KeyCode.LeftShift)) SwitchToEngineOffAudio();
        if (Input.GetKey(KeyCode.LeftShift)) UseFuel();

        if (rigidBody.linearVelocity.z > topSpeed) topSpeed = rigidBody.linearVelocity.z;
        if (transform.position.x > positiveXRange || transform.position.x < negativeXRange)
        {
            Debug.Log(Mathf.Sign(transform.position.x));
            transform.position += new Vector3((-Mathf.Sign(transform.position.x))*2,0,0); 
        } 

    }

    private void LateUpdate()
    {
        timeKeeperForLastFrame += Time.deltaTime;
        if (timeKeeperForLastFrame >= 0.1f)
        {
            timeKeeperForLastFrame = 0;
            speedOnLastFrame = rigidBody.linearVelocity.z;
        }
        
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
            // TODO: maybe delete but could be cool to see the stats
            //  timeSpentFueling += Time.deltaTime;
        } else engineOffAudio.pitch = 1;

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
        } if (other.gameObject.CompareTag("Earth")) WinGameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            //Destroy the power up and pick up fuel
            Destroy(other.gameObject);
            fuelGauge += powerUpFuel;
            fuelUpAudio.Play();
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


    void SwitchToEngineOffAudio()
    {
        engineOffAudio.pitch = 1;
    }

    void GameOver()
    {
        engineOffAudio.pitch = 0.8f;
        rocketCrashAudio.Play();
        gameOver = true;
        gameManager.ShowGameOverText(Mathf.Floor(speedOnLastFrame));
    }

    void WinGameOver()
    {
        engineOffAudio.pitch = 0.8f;
        gameManager.ShowVictoryScreen();
        gameOver = true;
    }
}
