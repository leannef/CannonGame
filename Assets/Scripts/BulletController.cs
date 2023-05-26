using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, 4f);
    }

    private void Update()
    {
        if (rb != null)
        {
            rb.AddForce(Vector3.down * 9.8f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            GameManager.Instance.boom.transform.position = other.transform.position;
            Destroy(other.gameObject);
            GameManager.Instance.TargetHit();             
            Destroy(gameObject);
            
        }
    }

}
