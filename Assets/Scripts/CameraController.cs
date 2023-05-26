using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    
    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation = this.transform.rotation;
    }

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            transform.RotateAround(this.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed);
            transform.RotateAround(this.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * rotationSpeed);
        }
        
        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetCamera();
        }
    }

    void ResetCamera()
    {
        // Reset camera rotation to original values
        transform.rotation = originalRotation;
    }
}
