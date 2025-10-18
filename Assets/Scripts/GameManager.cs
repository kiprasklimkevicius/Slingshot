using System;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private PlayerController player;
    private Rigidbody playerRigidBody;
    public bool gameWon;
    
    public RawImage fuelGaugeArrow;
    

    public TextMeshProUGUI speedText;

    public GameObject victoryTexts;
    public TextMeshProUGUI victorySpeedText;
    public TextMeshProUGUI gameOverText;

    private int difficulty;
    
    public GameObject asteroid;

    public TextMeshProUGUI speedOnImpactText;
    
    public bool startGame;
    public GameObject startGameUI;
    public GameObject FuelGaugeUI;
    public bool gamePaused;

    public GameObject tutorial1;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;

    public float trackLength = 600;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            startGame = false;
            difficulty = 0;
            gamePaused = false;
        } else Destroy(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!startGame) return;
        
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerRigidBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        gameWon = false;
        // TODO: only if this is the main scene
        if (difficulty  != 0) SpawnAsteroids(20 * difficulty);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCanvas(scene.name);
        if (!startGame) return;
        
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerRigidBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
        gameWon = false;
        
        // TODO: only if this is the main scene
        if (difficulty  != 0) SpawnAsteroids(20 * difficulty);
    }

    void InitializeCanvas(string sceneName)
    {
        if (sceneName.Equals("StartingScene")) {
            startGameUI.SetActive(true);
            speedText.gameObject.SetActive(false);
            FuelGaugeUI.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            victoryTexts.SetActive(false);
        } else {
            startGameUI.SetActive(false);
            speedText.gameObject.SetActive(true);
            FuelGaugeUI.SetActive(true);
            gameOverText.gameObject.SetActive(false);
            victoryTexts.SetActive(false);
        }
    }

    public void StartGame(int difficulty)
    {
        this.difficulty = difficulty;
        startGame = true;
        startGameUI.SetActive(false);
        FuelGaugeUI.SetActive(true);
        speedText.gameObject.SetActive(true);
        switch (difficulty)
        {
            case 0:
                SceneManager.LoadScene("Scenes/TutorialScene1");
                break;
            case 1: 
                SceneManager.LoadScene("MainScene");
                break;
            case 2: 
                SceneManager.LoadScene("MainScene");
                break;
            case 3: 
                SceneManager.LoadScene("MainScene");
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!startGame) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gamePaused) PauseGame();
            if (gamePaused) ResumeGame();
        }
        
        if (gamePaused) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (gameWon && Input.GetKeyDown(KeyCode.Space))
        {
            startGame = false;
            SceneManager.LoadScene("Scenes/StartingScene");
        }
        UpdateSpeedText();
        float arrowRotation = Mathf.Lerp(120, 0, player.fuelGauge/100);
        fuelGaugeArrow.transform.rotation = Quaternion.Euler(Vector3.forward * arrowRotation);
    }

    public void PauseGame()
    {
            Time.timeScale = 0;
            gamePaused = true;
            player.PauseSounds();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        player.ResumeSounds();
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
        float spacingBetweenObjects = trackLength / numberToBeSpawned;
        for (int i = 0; i < numberToBeSpawned; i++)
        {
            // 600 is the distance from start to finish
            // TODO: change magic number to what it should be^^^
            Instantiate(asteroid, new Vector3(0, 0, zPos + i * spacingBetweenObjects), Quaternion.Euler(RandomRotationVector()));
        }
    }

    public void ShowTutorial(int tutorial, bool active)
    {
        Debug.Log("Show tutorial " + tutorial + " " + active);
        switch (tutorial)
        {
            case 1:
                tutorial1.SetActive(active);
                break;
            case 2:
                tutorial2.SetActive(active);
                break;
            case 3: 
                tutorial3.SetActive(active);
                break;
            case 4:
                tutorial4.SetActive(active);
                break;
            default:
                Debug.Log("missed case");
                break;
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
