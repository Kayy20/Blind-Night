using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject sonarShot;
    public GameObject sonar;
    FirstPersonController fpc;

    [SerializeField] private float minThrowForce = 0f;
    [SerializeField] private float maxThrowForce = 15f;
    private float throwForce;
    public Image circleBar;


    // Walk Cycle for sonar to drop at the feet
    [SerializeField] private float walkCycle = 1f;
    private float walkCycleTimer = 0f;

    private void Start()
    {
        fpc = GetComponent<FirstPersonController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            throwForce = Mathf.Clamp(throwForce + Time.deltaTime * 50, minThrowForce, maxThrowForce);
            circleBar.fillAmount = throwForce / maxThrowForce;    
            // update a circle slider to show the throw force

        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 throwDirection = fpc.playerCamera.transform.forward; // Direction from you
            GameObject thrownSonar = Instantiate(sonarShot, fpc.playerCamera.transform.position, Quaternion.identity);
            thrownSonar.GetComponent<Sonar>().Throw(throwDirection, throwForce);
            circleBar.fillAmount = 0;
            throwForce = 0;
        }
        //if (Input.GetMouseButtonUp(1))
        //{
        //    GameObject thrownSonar = Instantiate(sonar, transform.position, Quaternion.identity);
        //}

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            walkCycleTimer += Time.deltaTime;
            if (walkCycleTimer >= walkCycle)
            {
                walkCycleTimer = 0;
                Instantiate(sonar, transform.position, Quaternion.identity);
            }
        }

    }
}
