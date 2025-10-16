using System;
using System.Collections;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public float speed = 200;
    
    private float spawnLocation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnLocation = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        if (transform.position.z - spawnLocation >50) Destroy(gameObject);
    }
    
    
}
