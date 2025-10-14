using System;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public GameObject powerUp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float timeChecker = 0;
    private bool timeCheckerZero = true;
    private float timeToMove = 1.5f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Lerp from position a to b. and back.
        if (timeCheckerZero)
        {
            timeChecker += Time.deltaTime;
            if (timeChecker > timeToMove)
            {
                timeCheckerZero = false;
            }
        }
        else
        {
            timeChecker -= Time.deltaTime;
            if (timeChecker <= 0)
            {
                timeCheckerZero = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("innadepowerup");
        Destroy(gameObject);
        Destroy(other.gameObject);
        Instantiate(powerUp, transform.position, powerUp.transform.rotation);
    }

    private void OnCollisionEnter(Collision other)
    {
        
    }
}
