using UnityEngine;

public class HomingProjectile : Projectile
{
    public GameObject GameObject { get; }
    public float RotateSpeed { get; }

    public HomingProjectile(int damage, Vector3 firedFrom, GameObject gameObject, int maxDistance, float rotateSpeed, float timeToLive, int velocity)
        : base(damage, firedFrom, gameObject, maxDistance, timeToLive, velocity)
    {
        GameObject = gameObject;
        RotateSpeed = rotateSpeed;
    }
}
