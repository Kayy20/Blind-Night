using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingCube : MonoBehaviour
{
    public int cubesPerAxis = 8;
    public float force = 100f;
    public float radius = 2f;
    public float upMod = -0.5f;

    public float yDiff = 0f;

    public GameObject cubePrefab;

    public void Explode()
    {
        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for(int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        Destroy(gameObject);
    }

    private void CreateCube(Vector3 coordinates)
    {
        GameObject cube = Instantiate(cubePrefab);

        cube.transform.localScale = transform.localScale / cubesPerAxis;

        Vector3 cubeLocation = (transform.position + Vector3.up * yDiff) - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = cubeLocation + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.GetComponent<Rigidbody>();


        rb.AddExplosionForce(force, (transform.position + Vector3.up*yDiff), radius, upMod);

        cube.SendMessage("DestroyAfterTime");
    }

}
