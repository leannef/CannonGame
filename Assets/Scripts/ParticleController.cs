using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public float playDuration = 1.5f;
    private ParticleSystem boom;
    private float timer;

    private void Start()
    {
        boom = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= playDuration)
        {
            boom.Stop();
            enabled = false;
        }
    }
}
