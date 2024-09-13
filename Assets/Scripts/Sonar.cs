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
        if (other.CompareTag("Player") !! other.CompareTag("Enemy"))
        {
            return;
        }
        Debug.Log("Sonar hit: " + other.name);
        Instantiate(sonarPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        // signal to all tracking enemies that the sonar has been dropped
        foreach (TrackingEnemy enemy in FindObjectsOfType<TrackingEnemy>())
        {
            enemy.SonarDrop(transform);
        }
    }
}
