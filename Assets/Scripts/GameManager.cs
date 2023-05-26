using System.Collections;
using System.Collections.Generic;
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
    public ParticleSystem boom;
    public delegate void TargetHitDelegate();
    public static event TargetHitDelegate OnTargetHitEvent;

    

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
        StopParticleEffect();
        OnTargetHitEvent += HandleTargetHit;
    }

    private void Start()
    {
        InvokeRepeating("SpawnTargets", 1.0f, 6.5f);
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
        PlayParticleEffect();
        SpawnTargets();
    }

    public void PlayParticleEffect()
    {
        boom.Play();
    }

    public void StopParticleEffect()
    {        
        boom.Stop();
    }

    private void OnDisable()
    {
        OnTargetHitEvent -= HandleTargetHit;
    }
}
