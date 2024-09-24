using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    public float time = 5f;
    public void DestroyAfterTime()
    {
        Destroy(gameObject,  time);
    }
}
