using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingHitText : MonoBehaviour
{
    public float duration = 1f;
    public float moveSpeed = 1f;
    public float fadeSpeed = 1f;

    private TextMeshPro hit;
    private Color originalColor;
    private float timer;

    private void Awake()
    {
        if (hit == null)
        {
            hit = GetComponent<TextMeshPro>();
            originalColor = hit.color;
        }

    }

    private void Update()
    {
        // Move the text upward
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Fade out the text over time
        float fadeAmount = Mathf.Lerp(1f, 0f, timer / duration);
        hit.color = new Color(originalColor.r, originalColor.g, originalColor.b, fadeAmount);

        // Update the timer
        timer += Time.deltaTime;

        // Destroy the game object when the duration is exceeded
        if (timer >= duration)
        {
            Destroy(gameObject);
        }
    }
}
