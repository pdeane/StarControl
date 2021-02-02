using UnityEngine;

public class PointDefenseProjectile : Projectile
{
    public PointDefenseProjectile(int damage, Vector3 firedFrom)
        : base(damage, firedFrom, null, 0, 0, 0)
    { }
}
