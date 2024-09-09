using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingEnemy : Enemy
{
    public void SonarDrop(Transform loc)
    {
        // move agent to the location of the sonar
        if (boundaries.Contains(loc.position))
        {
            agent.SetDestination(loc.position);
            agent.speed = 5;
        }
            
    }
}
