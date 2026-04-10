using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameActive;
    public int score;
    float enemySeekerSpawnInterval = 1.5f;
    float enemySeekerSpawnStart = 5f;
    float enemyShooterSpawnInterval = 4f;
    float enemyShooterSpawnStart = 7f;
    float powerUpSpawnInterval = 7f;
    float powerUpSpawnStart = 15f;
    PlayerControl playerControl;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    [SerializeField]
    GameObject titleScreen;
    [SerializeField]
    GameObject enemySeekerPrefab;
    [SerializeField]
    GameObject enemyShooterPrefab;
    [SerializeField]
    GameObject speedUpPrefab;
    [SerializeField]
    GameObject lifeUpPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        isGameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.health <= 0)
        {
            isGameActive = false;
        }
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playerControl.health;
    }
    public void StartGame()
    {
        InvokeRepeating("SpawnEnemySeeker", enemySeekerSpawnStart, enemySeekerSpawnInterval);
        InvokeRepeating("SpawnEnemyShooter", enemyShooterSpawnStart, enemyShooterSpawnInterval);
        InvokeRepeating("SpawnPowerUp", powerUpSpawnStart, powerUpSpawnInterval);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
    }
    void SpawnEnemySeeker()
    {
        if (isGameActive)
        {
            Instantiate(enemySeekerPrefab, GetEnemySpawnPos(), enemySeekerPrefab.transform.rotation);
        }
    }
    void SpawnEnemyShooter()
    {
        if (isGameActive)
        {
            Instantiate(enemyShooterPrefab, GetShooterSpawnPos(), enemyShooterPrefab.transform.rotation);
        }
    }
    void SpawnPowerUp()
    {
        int pickRandomPowerUp = Random.Range(0, 2);
        if (pickRandomPowerUp == 0 && isGameActive)
        {
            Instantiate(lifeUpPrefab, GetPowerUpSpawnPos(), lifeUpPrefab.transform.rotation);
        }
        else if (pickRandomPowerUp == 1 && isGameActive)
        {
            Instantiate(speedUpPrefab, GetPowerUpSpawnPos(), speedUpPrefab.transform.rotation);
        }
    }
    Vector3 GetEnemySpawnPos()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-playerControl.xBound, playerControl.xBound), 0.5f, 7f);
        return spawnPos;
    }
    Vector3 GetShooterSpawnPos()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-playerControl.xBound, playerControl.xBound), 0.5f, 5f);
        return spawnPos;
    }
    Vector3 GetPowerUpSpawnPos()
    {
        Vector3 powerUpSpawnPos = new Vector3(Random.Range(-playerControl.xBound, playerControl.xBound), 0.7f, 7f);
        return powerUpSpawnPos;
    }
}
