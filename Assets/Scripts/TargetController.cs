using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private bool isTriggered = false;
    private float timer = 0f;
    private float detectionDelay = 15f; // Time in seconds to detect no triggers


    private void Update()
    {
        if (!isTriggered)
        {
            timer += Time.deltaTime;
            if (timer >= detectionDelay)
            {   
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.DecreaseScore();
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            isTriggered = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            isTriggered = false;
        }
    }
}
