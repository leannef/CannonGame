using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform cannonballSpawnPoint;
    public float cannonSpeed = 5f;
    public float cannonballSpeed = 200f;
    public float rotationSpeed = 0.5f;
    public float maxRotation = 50f; 

    private float currentRotationHorizontal = 0f;
    private float currentRotationVertical = -10f;
    private float maxMoveDistance = 50f;

    private Vector3 initialPosition; //Cannon Initial Position

    private void Start()
    {
        initialPosition = transform.parent.position;
    }

    void Update()
    {        
        // Rotate the cannon horizontally based on arrow key inputs
        float rotationHorizontal = Input.GetAxis("Horizontal") * rotationSpeed;
        currentRotationHorizontal += rotationHorizontal;
        currentRotationHorizontal = Mathf.Clamp(currentRotationHorizontal, -maxRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(0f, currentRotationHorizontal, 0f);

        // Rotate the cannon vertically based on arrow key inputs
        float rotationVertical = Input.GetAxis("Vertical") * rotationSpeed;
        currentRotationVertical -= rotationVertical;
        currentRotationVertical = Mathf.Clamp(currentRotationVertical, -30f, 20f);
        transform.localRotation *= Quaternion.Euler(0f, 0f, currentRotationVertical);

        // Fire the cannonball on spacebar press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireCannonball();
        }

        // Move the cannon left when 'Q' key is pressed
        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveCannonLeft();
        }

        // Move the cannon right when 'E' key is released
        if (Input.GetKeyUp(KeyCode.Q))
        {
            MoveCannonRight();
        }
    }

    void FireCannonball()
    {
        // Instantiate a new cannonball prefab at the cannon's outer ring
        GameObject cannonball = Instantiate(cannonballPrefab);
        cannonball.transform.position = cannonballSpawnPoint.position;
        Rigidbody rb = cannonball.GetComponent<Rigidbody>();
        Vector3 direction = cannonballSpawnPoint.forward;
        rb.AddForce(direction * cannonballSpeed, ForceMode.Impulse);
    }

    void MoveCannonLeft()
    {
        Vector3 newPos = transform.position + Vector3.left * cannonSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, initialPosition.x - maxMoveDistance, initialPosition.x);
        transform.position = newPos;
    }

    void MoveCannonRight()
    {
        Vector3 newPos = transform.position + Vector3.right * cannonSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, initialPosition.x, initialPosition.x + maxMoveDistance);
        transform.position = newPos;
    }

}
