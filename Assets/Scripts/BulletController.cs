using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject boomPrefab;
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
            Instantiate(boomPrefab, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            GameManager.Instance.TargetHit();             
            Destroy(gameObject);
            
        }
    }

}
