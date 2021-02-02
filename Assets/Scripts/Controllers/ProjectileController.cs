using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Projectile Projectile { get; set; }

    protected virtual void FixedUpdate()
    {
        if (Projectile.Rigidbody != null)
        {
            Projectile.Rigidbody.velocity = transform.forward * Projectile.Velocity;
        }
    }

    protected virtual void Start()
    {
        if (Projectile.TimeToLive > 0)
        {
            StartCoroutine(SelfDestructTimer());
        }
    }

    protected virtual void Update()
    {
        if (Projectile.MaxDistance > 0)
        {
            float distance = Vector3.Distance(transform.position, Projectile.FiredFrom);
            if (distance > Projectile.MaxDistance)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Asteroid":
            case "GravityWell":
            case "Projectile":
                ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();
                if (particleSystem != null)
                {
                    particleSystem.transform.parent = null;
                    particleSystem.Stop();
                    Destroy(particleSystem.gameObject, 5f);
                }
                Destroy(gameObject);
                break;
        }
    }

    private IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(Projectile.TimeToLive);
        Destroy(gameObject);
    }
}
