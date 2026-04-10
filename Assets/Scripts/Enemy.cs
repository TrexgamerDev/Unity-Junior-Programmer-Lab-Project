using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float speed = 8.5f;
    float bulletSpeed = 30f;
    float enemyShotRate = 2f;
    float enemyShootingStartDelay = 3f;
    float zBound = 14f;
    GameObject player;
    PlayerControl playerControl;
    GameManager gameManager;
    public GameObject bulletPrefab;
    public Animator enemyAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
        StartCoroutine(ShootRoutine());
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
    IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(enemyShootingStartDelay);
        while (gameObject.CompareTag("EnemyShooter") && gameManager.isGameActive)
        {
            enemyAnimator.SetBool("Shoot_b", true);
            Instantiate(bulletPrefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
            enemyAnimator.SetBool("Shoot_b", false);
            yield return new WaitForSeconds(enemyShotRate);
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
