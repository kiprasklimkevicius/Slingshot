using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initSpeed = 10;
    public float speed;
    public float lateralSpeed = 5;
    public GameObject projectile;
    private float[] xPosProjectileSpawn = {-0.5f, 0.5f};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = initSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        LateralMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void LateralMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //TODO: check behaviour of Time.deltaTime,
            // I think its fine without this because its on discrete Input not Continuous pressing.
            transform.Translate(Vector3.right * lateralSpeed);
        } if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.right * -lateralSpeed);
        }
    }
    

    void ShootProjectile()
    {
        Vector3 xPos = Vector3.right * xPosProjectileSpawn[Random.Range(0, 2)];
        Instantiate(projectile, transform.position + xPos, projectile.transform.rotation);
    }
}
