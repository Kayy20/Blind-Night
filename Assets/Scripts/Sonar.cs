using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    public GameObject sonarPrefab;

    public bool isBoss = false;

    public void Throw(Vector3 direction, float throwForce)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(direction * throwForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isBoss)
        {
            if (other.CompareTag("Player") || other.CompareTag("Enemy"))
            {
                return;
            }
            // signal to all tracking enemies that the sonar has been dropped
            foreach (TrackingEnemy enemy in FindObjectsOfType<TrackingEnemy>())
            {
                enemy.SonarDrop(transform);
            }
        }
        else
        {
            if (other.CompareTag("Enemy"))
            {
                return;
            }

            if (other.CompareTag("Button"))
            {
                // Call GameManager to spawn in a thing to drop on the boss
                Destroy(other.gameObject);
                GameManager.Instance.DamageBoss();
            }

            if (other.CompareTag("Player"))
            {
                GameManager.Instance.ReloadScene(true);
            }

        }

        Debug.Log("Sonar hit: " + other.name);
        Instantiate(sonarPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
