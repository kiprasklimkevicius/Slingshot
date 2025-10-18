using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public GameObject powerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timeChecker = 0;
    private bool timeCheckerZero = true;
    private float timeToMove = 1.5f;
    private Vector3 initSpeed = new Vector3(10, 0, -2);
    private Rigidbody rb;
    public GameObject smokeEffects;
    public bool tutorialAsteroid; // TODO: This is not proper but works I do quick changes later maybe I fix.
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddTorque(RandomTorque(), ForceMode.Impulse);
        if (tutorialAsteroid) return;
        int randomSign = Random.Range(0, 2) * 2 - 1;
        initSpeed.x *= randomSign;
        rb.AddForce(initSpeed, ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), ForceMode.Impulse);
        float negativeXRange = GameObject.Find("Wall-Left").transform.position.x + 3;
        float positiveXRange = GameObject.Find("Wall-Right").transform.position.x - 3;
        transform.position = new Vector3(Random.Range(negativeXRange, positiveXRange), 
            transform.position.y, transform.position.z);

    }

    private Vector3 RandomTorque()
    {
        Vector3 result = new Vector3(Random.Range(0f,360f), Random.Range(0f,360f), Random.Range(0f,360f));
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LaserShot"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(powerUp, transform.position, powerUp.transform.rotation);
            Instantiate(smokeEffects, transform.position, smokeEffects.transform.rotation);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
