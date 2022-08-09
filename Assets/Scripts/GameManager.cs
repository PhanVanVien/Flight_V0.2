using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> obstacles;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private PlayerController playerControllerScript;
    public GameObject gameOver;
    public TextMeshProUGUI scoreText;
    private GameObject prefabsObstacles;
    public Button backToMenuButton;

    private float startDelay = 2;
    private float repeatRate = 5;
    public float score = 0;
    private float leftBound = 0.5f;
    public bool onetime;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        scoreText.text = "Score : " + score.ToString();
        backToMenuButton.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (playerControllerScript.gameOver == true)
        {
            GameOver();
        }
        if (prefabsObstacles != null)
        {
            UpdateScore();
        }
    }
    void SpawnObstacle()
    {
        if (!playerControllerScript.gameOver)
        {
            int randomIndex = Random.Range(0, obstacles.Count);
            prefabsObstacles = Instantiate(obstacles[randomIndex], spawnPos, obstacles[randomIndex].transform.rotation);
            onetime = false;
        }
    }

    void UpdateScore()
    {
        if (prefabsObstacles.transform.position.x < leftBound && playerControllerScript.gameOver == false && onetime == false)
        {
            score += prefabsObstacles.gameObject.GetComponent<DestroyOutOfBound>().value;
            scoreText.text = "Score : " + score.ToString();
            onetime = true;
        }
    }

    public void GameOver()
    {
        gameOver.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        backToMenuButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        Physics.gravity = playerControllerScript.physicGravity;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
