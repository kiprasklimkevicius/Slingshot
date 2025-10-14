using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private Rigidbody playerRigidBody;

    public TextMeshProUGUI speedText;

    public TextMeshProUGUI gameOverText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        Debug.Log("player speed = " + player.initSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        UpdateSpeedText();
    }

    void UpdateSpeedText()
    {
        float pSpeed = player.speed.y + playerRigidBody.linearVelocity.z;
        speedText.text = "Speedometer: " + pSpeed;
    }

    public void ShowGameOverText()
    {
        gameOverText.gameObject.SetActive(true);
    }
}
