using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    // public Animator[] titleScreen;
    public GameObject introText;
    public GameObject startGame;
    public Animator camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            introText.SetActive(false);
            camera.speed = 1000;
            startGame.SetActive(true);
            // for (int i = 0; i < titleScreen.Length; i++) titleScreen[i].SetBool("introFinished", true);
        }
        
    }

    IEnumerator ShowTitleScreen()
    {
        yield return new WaitForSeconds(0.1f);
        
    }
}
