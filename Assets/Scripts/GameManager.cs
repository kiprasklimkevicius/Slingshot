using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody playerRigidBody;
    public bool gameWon;
    
    public RawImage fuelGaugeArrow;
    

    public TextMeshProUGUI speedText;

    public GameObject victoryTexts;
    public TextMeshProUGUI victorySpeedText;
    public TextMeshProUGUI gameOverText;

    public GameObject asteroid;

    public TextMeshProUGUI speedOnImpactText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameWon = false;
        // TODO: only if this is the main scene
        SpawnAsteroids(60);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (gameWon && Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("Scenes/MainScene");
        UpdateSpeedText();
        float arrowRotation = Mathf.Lerp(120, 0, player.fuelGauge/100);
        fuelGaugeArrow.transform.rotation = Quaternion.Euler(Vector3.forward * arrowRotation);
    }

    void UpdateSpeedText()
    {
        float pSpeed = playerRigidBody.linearVelocity.z;
        speedText.text = "Speedometer: " + pSpeed;
    }

    public void ShowGameOverText(float speedOnImpact)
    {
        speedOnImpactText.text = "Your Speed on Impact: " +speedOnImpact;
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowVictoryScreen()
    {
        victorySpeedText.text = "Your top speed:" + Mathf.Floor(player.topSpeed) +  "LY/s";
        victoryTexts.SetActive(true);
        gameWon = true;
    }

    private void SpawnAsteroids(int numberToBeSpawned)
    {
        // zpos of the first asteroid
        float zPos = 24;
        for (int i = 0; i < numberToBeSpawned; i++)
        {
            Instantiate(asteroid, new Vector3(0, 0, zPos + i * 10), Quaternion.Euler(RandomRotationVector()));
        }
    }

    private Vector3 RandomRotationVector()
    {
        float x = Random.Range(0.0f, 360.0f);
        float y = Random.Range(0.0f, 360.0f);
        float z = Random.Range(0.0f, 360.0f);
        return new Vector3(x, y, z);
    }
    
}
