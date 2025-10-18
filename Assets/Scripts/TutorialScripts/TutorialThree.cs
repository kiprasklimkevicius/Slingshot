using UnityEngine;

public class TutorialThree : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController player;
    private bool tutorialActive;
    public GameObject tutorial4;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.LateralMovement())
        {
            gameManager.ShowTutorial(3, false);
            gameManager.ResumeGame();
            tutorial4.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PauseGame();
            gameManager.ShowTutorial(3, true);
        }
    }
}
