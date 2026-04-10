using UnityEngine;

public class PowerUps : MonoBehaviour
{
    float speed = 8f;
    GameManager gameManager;
    PlayerControl playerControl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        DestroyOutOfBounds();
    }
    void DestroyOutOfBounds()
    {
        if (transform.position.x > playerControl.xBound || transform.position.x < -playerControl.xBound)
        {
            Destroy(gameObject);
        }
    }
}
