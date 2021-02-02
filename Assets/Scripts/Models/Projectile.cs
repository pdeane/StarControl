using UnityEngine;

public class Projectile
{
    public int Damage { get; }
    public Vector3 FiredFrom { get; }
    public int MaxDistance { get; }
    public Rigidbody Rigidbody { get; }
    public float TimeToLive { get; }
    public int Velocity { get; }

    public Projectile(int damage, Vector3 firedFrom, GameObject gameObject, int maxDistance, float timeToLive, int velocity)
    {
        Damage = damage;
        FiredFrom = firedFrom;
        MaxDistance = maxDistance;
        Rigidbody = gameObject?.GetComponent<Rigidbody>();
        TimeToLive = timeToLive;
        Velocity = velocity;
    }
}
