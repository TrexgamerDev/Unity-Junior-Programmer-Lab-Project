using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    float maxHealth;
    PlayerControl playerControl;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    [SerializeField]
    GameObject titleScreen;
    [SerializeField]
    GameObject pauseScreen;
    [SerializeField]
    GameObject gameOverScreen;
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
        playerControl.health = 3;
        isGameActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfIsAlive();
        if (isGameActive && playerControl.health > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        else if (!isGameActive && playerControl.health > 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            UnpauseGame();
        }
        LimitHealth();
        scoreText.text = "Score: " + score;
        healthText.text = "Health: " + playerControl.health;
    }
    // ABSTRACTION
    void CheckIfIsAlive()
    {
        if (playerControl.health <= 0 || score < 0)
        {
            gameOverScreen.gameObject.SetActive(true);
            isGameActive = false;
        }
    }
    void LimitHealth()
    {
        if (playerControl.health > maxHealth && maxHealth != 0)
        {
            playerControl.health = maxHealth;
        }
    }
    public void StartGame(int difficulty)
    {
        if (difficulty == 1) { SetDifficultyMedium(); }
        else if (difficulty == 2) { SetDifficultyHard(); }
        else
        {
            playerControl.health = 3;
            maxHealth = 1000;
        }
        InvokeRepeating("SpawnEnemySeeker", enemySeekerSpawnStart, enemySeekerSpawnInterval);
        InvokeRepeating("SpawnEnemyShooter", enemyShooterSpawnStart, enemyShooterSpawnInterval);
        InvokeRepeating("SpawnPowerUp", powerUpSpawnStart, powerUpSpawnInterval);
        isGameActive = true;
        titleScreen.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void PauseGame()
    {
        isGameActive = false;
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
    }
    void UnpauseGame()
    {
        isGameActive = true;
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
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
    public void SetDifficultyMedium()
    {
        maxHealth = 3;
        playerControl.health = maxHealth;
        enemySeekerSpawnInterval = 1f;
        enemyShooterSpawnInterval = 3f;
        powerUpSpawnInterval = 10f;
        if (playerControl.health > maxHealth)
        {
            playerControl.health = 3;
        }
    }
    public void SetDifficultyHard()
    {
        maxHealth = 1;
        playerControl.health = maxHealth;
        enemySeekerSpawnInterval = 1f;
        enemyShooterSpawnInterval = 2f;
        powerUpSpawnInterval = 15f;
    }
}
