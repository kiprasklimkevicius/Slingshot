using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float initSpeed = 10;
    public Vector3 speed;
    public Vector3 lateralSpeed = new Vector3(5,0,0); // lateral speed
    public GameObject projectile;
    private float[] xPosProjectileSpawn = {-0.5f, 0.5f};
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Vector3.up is moving forward, because capsule is rotated 90 around x-axis;
        speed = new Vector3(0, initSpeed, 0);

    }

    // Update is called once per frame
    void Update()
    {
        LateralMovement();
        transform.Translate(speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void LateralMovement()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed += lateralSpeed;
        } if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed -= lateralSpeed;
        }
    }
    

    void ShootProjectile()
    {
        Vector3 xPos = Vector3.right * xPosProjectileSpawn[Random.Range(0, 2)];
        Instantiate(projectile, transform.position + xPos, projectile.transform.rotation);
    }
}
