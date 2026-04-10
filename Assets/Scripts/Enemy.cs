using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 8.5f;
    float bulletSpeed = 30f;
    float enemyShotRate = 3f;
    float enemyShootingStartDelay = 5f;
    float zBound = 14f;
    GameObject player;
    PlayerControl playerControl;
    GameManager gameManager;
    public GameObject bulletPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        InvokeRepeating("EnemyShooting", enemyShotRate, enemyShootingStartDelay);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOutOfBounds();
        MoveObjects();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            gameManager.score++;
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Sensor") && gameObject.CompareTag("EnemySeeker"))
        {
            gameManager.score--;
            Destroy(gameObject);
        }
    }
    void MoveObjects()
    {
        if (gameObject.CompareTag("EnemySeeker") && gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        else if (gameObject.CompareTag("EnemyShooter") && gameManager.isGameActive)
        {
            transform.LookAt(player.transform);
        }
        else if ((gameObject.CompareTag("Bullet") || gameObject.CompareTag("PlayerBullet")) && gameManager.isGameActive)
        {
            transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        }
    }
    void EnemyShooting()
    {
        if (gameObject.CompareTag("EnemyShooter") && gameManager.isGameActive)
        {
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }
    void DestroyOutOfBounds()
    {
        // Set the X and Z axis bounds and destroy object if it goes out of bounds
        if (transform.position.x > playerControl.xBound || transform.position.x < -playerControl.xBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z > zBound || transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }
    }
}
