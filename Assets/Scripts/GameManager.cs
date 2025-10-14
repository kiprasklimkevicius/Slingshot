using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController player;

    public TextMeshProUGUI speedText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
        float pSpeed = player.speed.y;
        speedText.text = "Speedometer: " + pSpeed;
    }
}
