using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed = 8.5f;
    protected float bulletSpeed = 30f;
    protected float enemyShotRate = 1f;
    protected float enemyShootingStartDelay = 2f;
    protected float zBound = 14f;
    protected float xBound = 10f;
    protected GameObject player;
    protected PlayerControl playerControl;
    protected GameManager gameManager;
    // ENCAPSULATION
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject gun;
    [SerializeField] protected AudioSource gunShot;
    [SerializeField] protected Animator enemyAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player");
        playerControl = player.GetComponent<PlayerControl>();
    }
    protected void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
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
    protected void MoveObjects()
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
    protected void DestroyOutOfBounds()
    {
        // Set the X and Z axis bounds and destroy object if it goes out of bounds
        if (transform.position.x > xBound || transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }
        if (transform.position.z > zBound || transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }
    }
    protected IEnumerator ShootRoutine()
    {
        yield return new WaitForSeconds(enemyShootingStartDelay);
        while (gameObject.CompareTag("EnemyShooter") && gameManager.isGameActive)
        {
            enemyAnimator.SetBool("Shoot_b", true);
            gunShot.PlayOneShot(gunShot.clip, 1f);
            Instantiate(bulletPrefab, gun.transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
            enemyAnimator.SetBool("Shoot_b", false);
            yield return new WaitForSeconds(enemyShotRate);
        }
    }
}
