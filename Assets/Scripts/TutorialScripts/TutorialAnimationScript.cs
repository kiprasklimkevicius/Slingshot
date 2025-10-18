using System;
using UnityEngine;

public class TutorialAnimationScript : MonoBehaviour
{
    private Rigidbody rigidbody;

    public ParticleSystem moveLeft;
    public ParticleSystem moveRight;

    private bool disable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        disable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LateralMovementLeft()
    {
        rigidbody.AddForce(Vector3.left * 4, ForceMode.Impulse);
        moveLeft.Play();
    }
    public void LateralMovementRight()
    {
        rigidbody.AddForce(Vector3.right * 8, ForceMode.Impulse);
        moveRight.Play();
    }

    public void StartMovement()
    {
        rigidbody.AddForce(Vector3.forward * 5, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (disable) return;
        Debug.Log("entered Trigger");
        if (other.CompareTag("Respawn"))
        {
            Debug.Log("animationMarker Triggered");
            LateralMovementRight();
        }

        if (other.CompareTag("Earth")) disable = true;
    }
}
