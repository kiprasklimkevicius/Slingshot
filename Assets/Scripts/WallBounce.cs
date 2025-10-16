using System;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    private float bounceSpeed = 1.5f;
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
        Rigidbody rigidBody = other.gameObject.GetComponent<Rigidbody>();
        if (rigidBody == null) return;
        float currentXSpeed = rigidBody.linearVelocity.x; // change the
        rigidBody.AddForce(new Vector3(-currentXSpeed*bounceSpeed,0,0), ForceMode.Impulse);
    }
}
