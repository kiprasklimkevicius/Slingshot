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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3.up is moving forward, because capsule is rotated 90 around x-axis;
        speed = new Vector3(0, initSpeed, 0);

    }

    // Update is called once per frame
    void Update()
    {
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.CompareTag("SpeedUp"))
        {
            Destroy(other.gameObject);
            //gain speed
            speed += new Vector3(0, powerupSpeed, 0);
        }
    }
}
