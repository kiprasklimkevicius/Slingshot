using UnityEngine;

public class TutorialOne : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController player;
    public GameObject tutorial2;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("In Start Tutorial 1");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.ShowTutorial(1, false);
            gameManager.ResumeGame();
            player.ShootProjectile();
            tutorial2.SetActive(true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PauseGame();
            gameManager.ShowTutorial(1, true);
        }
    }
}
