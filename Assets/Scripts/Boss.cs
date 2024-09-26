using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Boss : MonoBehaviour
{

    GameObject player;

    [SerializeField] float shotCooldown, shotTime;
    [SerializeField] GameObject bulletPrefab, damagePrefab;
    public float shotForce = 10f;

    [SerializeField] private ParticleSystem pSystem;

    public float Health { get { return health; } private set { health = value; } }
    [SerializeField] private float health = 10;

    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameManager.Instance.player;
        //dead = true;
        //StartCoroutine(DeathAnimation());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dead) return;
        // Shoot bullet at player after a certain amount of time
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (shotCooldown >= shotTime)
        {
            shotCooldown = 0;
            //Shoot
            GameObject g = Instantiate(bulletPrefab, new Vector3(transform.position.x, player.transform.position.y, transform.position.z), transform.rotation);
            g.GetComponent<Sonar>().Throw(transform.forward, shotForce);
        } else
        {
            shotCooldown += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            // spawn the sonar
            StartCoroutine(spawnDamageWaves(collision.contacts[0].point));
            Health -= 1;
            collision.gameObject.SendMessage("Explode");
            if (Health <= 0)
            {
                dead = true;
                StartCoroutine(DeathAnimation());
            }
        }
    }

    private IEnumerator spawnDamageWaves(Vector3 location)
    {
        for (int i = 0; i < 5; i++)
        {

            Instantiate(damagePrefab, location, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator DeathAnimation()
    {
        // change the loop time of the particle system
        pSystem.Stop();

        yield return new WaitForSeconds(2.5f);

        ParticleSystem.MainModule main = pSystem.main;
        main.duration = 0.5f;

        yield return new WaitForSeconds(1f);

        pSystem.Play();

        // change the colour of the particle system over time
        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[]
        {
            new GradientColorKey(Color.red, 0.0f),
            new GradientColorKey(Color.yellow, 0.16f),
            new GradientColorKey(Color.green, 0.33f),
            new GradientColorKey(Color.cyan, 0.5f),
            new GradientColorKey(Color.blue, 0.66f),
            new GradientColorKey(Color.magenta, 0.83f),
            new GradientColorKey(Color.white, 1.0f)
        },
            new GradientAlphaKey[]
            {
                new GradientAlphaKey(1.0f, 0.0f),
                new GradientAlphaKey(1.0f, 1.0f) });


        float elapsedTime = 0f;

        while (elapsedTime < 5f)
        {
            // Get the current time normalized (0 - 1) over the total cycle time
            float normalizedTime = elapsedTime / 5f;
            Color newColor = grad.Evaluate(normalizedTime);

            // Set the start color of the particle system
            main.startColor = newColor;

            // Wait for the next frame
            yield return null;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the final color is set after the loop completes
        main.startColor = grad.Evaluate(1.0f);

        pSystem.Stop();

        yield return new WaitForSeconds(2f);
        // Destroy the GameObject after the cycle completes
        GameManager.Instance.GameWin();
        GetComponent<ExplodingCube>().Explode();
    }
}
