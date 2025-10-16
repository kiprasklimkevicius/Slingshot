using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody playerRigidBody;
    
    
    public RawImage fuelGaugeArrow;
    

    public TextMeshProUGUI speedText;

    public GameObject victoryTexts;
    public TextMeshProUGUI victorySpeedText;
    public TextMeshProUGUI gameOverText;

    public TextMeshProUGUI speedOnImpactText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
    }
    
}
