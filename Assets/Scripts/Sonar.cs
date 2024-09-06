using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class Sonar : MonoBehaviour
{
    public GameObject sonarPrefab;
    public void Throw(Vector3 direction, float throwForce)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }
        Debug.Log("Sonar hit: " + other.name);
        Instantiate(sonarPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
