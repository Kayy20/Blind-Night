using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    public float speed = 5f;
    public float waitTime;
    private bool movingToTarget;
    private Vector3 startPosition;

    public bool time;
    public float timeToClose;

    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + targetPosition;
        if (time)
        {
            StartCoroutine(CloseDoor());
        }
        else
        {
            StartCoroutine(MoveDoorConstantly());
        }
    }

    private IEnumerator MoveDoorConstantly()
    {
        while (true)
        {
            // Move to the target or start position depending on the current state
            if (movingToTarget)
            {
                yield return StartCoroutine(MoveToPosition(targetPosition));
            }
            else
            {
                yield return StartCoroutine(MoveToPosition(startPosition));
            }

            // Wait for the specified time at each position
            yield return new WaitForSeconds(waitTime);

            // Switch movement direction
            movingToTarget = !movingToTarget;
        }
    }
    
    private IEnumerator CloseDoor()
    {

        float elapsedTime = 0f;

        while (true)
        {
            // Move to the target or start position depending on the current state
            if (movingToTarget)
            {
                while (elapsedTime < timeToClose)
                {

                   // Increment the timer (elapsedTime)
                    elapsedTime += Time.deltaTime;

                    float t = Mathf.Clamp01(elapsedTime / timeToClose);

                    transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                    yield return null;
                }
            }
            else
            {
                while (elapsedTime > 0)
                {

                    // Increment the timer (elapsedTime)
                    elapsedTime -= Time.deltaTime;

                    float t = Mathf.Clamp01(elapsedTime / timeToClose);

                    transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                    yield return null;
                }
            }

            // Wait for the specified time at each position
            yield return new WaitForSeconds(waitTime);

            // Switch movement direction
            movingToTarget = !movingToTarget;
        }
    }

    private IEnumerator MoveToPosition(Vector3 destination)
    {
        // Move towards the target position at the given speed
        while (Vector3.Distance(transform.position, destination) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }

        // Ensure the door ends at the exact position
        transform.position = destination;
    }
}
