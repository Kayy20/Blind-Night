using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarDestruction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // start coroutine to destroy the sonar
        StartCoroutine(DestroySonar());
    }

    // coroutine to destroy the sonar after 2 seconds
    IEnumerator DestroySonar()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
