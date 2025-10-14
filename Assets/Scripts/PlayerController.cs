using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initSpeed = 10;
    public Vector3 speed;
    public Vector3 lateralSpeed = new Vector3(5,0,0); // lateral speed
    public GameObject projectile;
    private float xPosProjectileSpawn = 0.5f;
    public float powerupSpeed = 3;
    public float blackHoleMass = 20;
    private Rigidbody rigidBody;
    public float gravityConstant = 0.1f;
    public bool gameOver;
    private GameManager gameManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3.up is moving forward, because capsule is rotated 90 around x-axis;
        speed = new Vector3(0, initSpeed, 0);
        rigidBody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;
        LateralMovement();
        transform.Translate(speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void LateralMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed += lateralSpeed;
        } if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed -= lateralSpeed;
        }
    }
    

    void ShootProjectile()
    {
        if (xPosProjectileSpawn == 0.5f) xPosProjectileSpawn--;
        else xPosProjectileSpawn++;
        Instantiate(projectile, transform.position + Vector3.right * xPosProjectileSpawn, projectile.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Deadly"))
        {
            Debug.Log("Shoula gameoverd");
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
            speed += new Vector3(0, powerupSpeed, 0);
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
        //mass Of Object allows me to control the pull through the rigidbody of gameObject that pulls it closer
        Vector3 forceToApply = vectorToObject.normalized * forceIntensity;
        // Vector3 pullVector = directionToObject.normalized * massOfObject * Time.deltaTime;
        //Object is rotated 90 degrees along x axis making y its forward direction
        forceToApply = new Vector3(forceToApply.x, forceToApply.z, forceToApply.y);
        speed += forceToApply * Time.deltaTime;
        Debug.Log("ForceApplied: " + forceToApply);
    }
    
    float GetObjectMass(GameObject obj)
    {
        Rigidbody rb = obj.GetComponentInParent<Rigidbody>();
        rb.mass = blackHoleMass;
        return rb.mass;
    }
}
