using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initSpeed = 10;
    public Vector3 speed;
    public Vector3 lateralSpeed = new Vector3(5,0,0); // lateral speed
    public GameObject projectile;
    private float xPosProjectileSpawn = 0.5f;
    public float powerUpFuel = 30;
    public float blackHoleMass = 20;
    private Rigidbody rigidBody;
    public float gravityConstant = 0.1f;
    public bool gameOver;
    private GameManager gameManager;
    private AudioSource laserShotAudio;
    public float xPull = 1.5f;
    public float fuelGauge;
    public float fuelDischargeRatePerSecond = 50;
    public float fuelSpeed = 10;
    private float timeSpentFueling = 0;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        
        LateralMovement();
        
        if (Input.GetKeyDown(KeyCode.Space)) ShootProjectile();

        if (Input.GetKey(KeyCode.LeftShift)) UseFuel();
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
        if (other.gameObject.CompareTag("Deadly"))
        {
            gameOver = true;
            gameManager.ShowGameOverText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            //gain speed
            fuelGauge += powerUpFuel;
        }
        if (other.CompareTag("Gravity"))
        {
            lateralSpeed.x = 2;
        }
    }
    

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Gravity"))
        {
            lateralSpeed.x = 5;
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
}
