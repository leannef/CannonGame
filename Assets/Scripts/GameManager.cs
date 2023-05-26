using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    public GameObject targetPrefab;
    public TextMeshProUGUI scoreText;
    public delegate void TargetHitDelegate();
    public static event TargetHitDelegate OnTargetHitEvent;
    
    private int score; 

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

    public void SpawnTargets()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();
        GameObject target = Instantiate(targetPrefab, spawnPoint, Quaternion.Euler(35, 0, 0));
        Destroy(target, 12f); //Destroy the target after 10 seconds;
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
        score++;
    }

    private void OnDisable()
    {
        OnTargetHitEvent -= HandleTargetHit;
    }
}
