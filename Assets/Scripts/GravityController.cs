using UnityEngine;

public class GravityController : MonoBehaviour
{
    public float xPull = 1.5f;
    public float gravityConstant = 0.1f;

    public int blackHoleMass = 50000;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Deadly") || other.CompareTag("SpeedUp")) return;
        
        Rigidbody otherRb = other.GetComponent<Rigidbody>();
        GravityPull(other.gameObject, otherRb);
    }

    void GravityPull(GameObject otherObject, Rigidbody rigidBody)
    {
        Vector3 vectorToObject = (transform.position - otherObject.transform.position);
        float distance = vectorToObject.magnitude;
        // 1/rigidBody.mass so that the less mass the other object has, the more it is affected AND vice versa 
        float forceIntensity = gravityConstant * blackHoleMass * (1/rigidBody.mass) / (distance);
        
        Vector3 forceToApply = vectorToObject.normalized * forceIntensity;
        forceToApply.x *= xPull; // Test maybe feels better 
        rigidBody.AddForce(forceToApply * Time.deltaTime, ForceMode.Force);
    }
    
}
