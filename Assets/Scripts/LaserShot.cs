using System;
using System.Collections;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public float speed = 200;
    private Rigidbody rb;
    
    private float spawnLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed *= rb.mass; // speed is 200 if mass = 1, we then scale in comparison to mass
        spawnLocation = transform.position.z;
        rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z - spawnLocation >50) Destroy(gameObject);
        Vector3 velocityVector = rb.linearVelocity.normalized;
        // switching the z and y is important for transform.LookAt because the capsule is rotated 90degrees around x-axis
        velocityVector = new Vector3(velocityVector.x, velocityVector.z, velocityVector.y);
        transform.LookAt(transform.position + velocityVector);
    }
    
    
}
