using UnityEngine;

public class TutorialFour : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject tutorialGuy;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.ShowTutorial(4, false);
            gameManager.ResumeGame();
            tutorialGuy.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PauseGame();
            gameManager.ShowTutorial(4, true);
        }
    }
}
