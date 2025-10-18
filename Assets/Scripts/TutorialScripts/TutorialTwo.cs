using System;
using UnityEngine;

public class TutorialTwo : MonoBehaviour
{
    private GameManager gameManager;
    private bool tutorialActive;
    public GameObject tutorial3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            gameManager.ShowTutorial(2, false);
            gameManager.ResumeGame();
            //Should be able to just return because in player its on GetKey()
            //player.UseGas();
            tutorial3.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.PauseGame();
            gameManager.ShowTutorial(2, true);
        }
    }
}
