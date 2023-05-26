using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finalText;
    public Transform endGamePanel;
    public GameObject targetPrefab;

    public delegate void TargetHitDelegate();
    public static event TargetHitDelegate OnTargetHitEvent;

    private bool isGameOver = false;
    private int score;
    public int Score { 
        get { return score; }
        set { score = value; }
    }
    private int targetScore = 10;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnEnable()
    {
        OnTargetHitEvent += HandleTargetHit;
    }

    private void Start()
    {
        score = 0; 
        UpdateScoreDisplay();
        InvokeRepeating("SpawnTargets", 1.0f, 6.5f);
    }

    private void UpdateScoreDisplay()
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if (score < -2)
        {
            isGameOver = true;
            Lose();
        }

        if (score >= targetScore)
        {
            isGameOver = true;
            Win();
        }
    }

    private void Lose()
    {
        finalText.text = "You Lost!";
        endGamePanel.gameObject.SetActive(true); 
    }

    private void Win()
    {
        finalText.text = "You Win!";
        endGamePanel.gameObject.SetActive(true);
    }

    public void SpawnTargets()
    {
        if (!isGameOver)
        {
            Vector3 spawnPoint = GetRandomSpawnPoint();
            Instantiate(targetPrefab, spawnPoint, Quaternion.Euler(35, 0, 0));
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        float randomX = Random.Range(-200f, 200f);
        float randomY = Random.Range(-90, 45);
        float randomZ = Random.Range(120, 155);
        Vector3 spawnPoint = new Vector3(randomX, randomY, randomZ);
        return spawnPoint;
    }

    public void TargetHit()
    {
        OnTargetHitEvent?.Invoke();
    }

    private void HandleTargetHit()
    {
        IncreaseScore();
        UpdateScoreDisplay();
        SpawnTargets();
    }

    private void IncreaseScore()
    {
        if (isGameOver)
        {
            return; 
        }
        score++;
    }

    public void DecreaseScore()
    {
        if (isGameOver)
        {
            return; 
        }
        score--;
        UpdateScoreDisplay();
    }

    private void OnDisable()
    {
        OnTargetHitEvent -= HandleTargetHit;
    }

    public void RestartGame()
    {
        score = 0;
        isGameOver = false; 
        endGamePanel.gameObject.SetActive(false);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
