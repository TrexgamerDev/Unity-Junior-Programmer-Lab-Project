using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    bool canShoot = true;
    float speed = 15f;
    public float health = 3;
    public float xBound = 10f;
    public GameObject bulletPrefab;
    public GameManager gameManager;
    [SerializeField]
    Animator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Shoot();
        CheckBounds();
    }
    void CheckBounds()
    {
        // Set the X axis bounds
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }
    void MovePlayer()
    {
        // Gets player input
        float horizontalInput = Input.GetAxis("Horizontal");
        if (gameManager.isGameActive)
        {
            //Moves the player based on input
            transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        }
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && gameManager.isGameActive)
        {
            StartCoroutine(ShootRoutine());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemySeeker"))
        {
            gameManager.score++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            health--;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("LifeUp"))
        {
            health++;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SpeedUp"))
        {
            StartCoroutine(SpeedUpRoutine());
            Destroy(other.gameObject);
        }
    }
    IEnumerator SpeedUpRoutine()
    {
        speed = 15f;
        yield return new WaitForSeconds(5f);
        speed = 10f;
    }
    IEnumerator ShootRoutine()
    {
        canShoot = false;
        playerAnimator.SetBool("Shoot_b", true);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("Shoot_b", false);
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }
}
