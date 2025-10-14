using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject powerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("innadepowerup");
        Destroy(gameObject);
        Instantiate(powerUp, transform.position, powerUp.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
