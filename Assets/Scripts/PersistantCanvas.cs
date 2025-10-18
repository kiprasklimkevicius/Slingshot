using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantCanvas : MonoBehaviour
{
    
    private static PersistantCanvas instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
