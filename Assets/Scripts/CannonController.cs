using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform cannonballSpawnPoint;
    public float cannonballSpeed = 10f;
    public float rotationSpeed = 0.5f;

    public float maxRotation = 50f; 
    private float currentRotationHorizontal = 0f;
    private float currentRotationVertical = 17f;

    void Update()
    {        
        // Rotate the cannon horizontally based on arrow key inputs
        float rotationHorizontal = Input.GetAxis("Horizontal") * rotationSpeed;
        currentRotationHorizontal += rotationHorizontal;
        currentRotationHorizontal = Mathf.Clamp(currentRotationHorizontal, -maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(0f, 0f, currentRotationHorizontal);

        // Rotate the cannon vertically based on arrow key inputs
        float rotationVertical = Input.GetAxis("Vertical") * rotationSpeed;
        currentRotationVertical += rotationVertical;
        currentRotationVertical = Mathf.Clamp(currentRotationVertical, 5f, maxRotation);
        transform.localRotation *= Quaternion.Euler(0f, currentRotationVertical, 0f);

        // Fire the cannonball on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireCannonball();
        }
    }

    void FireCannonball()
    {
        // Instantiate a new cannonball prefab at the cannon's outer ring
        GameObject cannonball = Instantiate(cannonballPrefab, cannonballSpawnPoint.position, Quaternion.identity);
        Rigidbody cannonballRigidbody = cannonball.GetComponent<Rigidbody>();
        cannonballRigidbody.velocity = transform.forward * cannonballSpeed;
        // Destroy the cannonball after 1 second
        Destroy(cannonball, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the cannonball hits the target
        if (collision.gameObject.CompareTag("Target"))
        {
            Debug.Log("Congratulations! You hit the target!");
        }
    }
}
