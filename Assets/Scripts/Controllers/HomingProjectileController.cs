using UnityEngine;

public class HomingProjectileController : ProjectileController
{
    public GameObject TargetGameObject { get; set; }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        HomingProjectile homingProjectile = Projectile as HomingProjectile;
        Quaternion rotation = Quaternion.LookRotation(TargetGameObject.transform.position - homingProjectile.GameObject.transform.position);
        Quaternion direction = Quaternion.RotateTowards(homingProjectile.GameObject.transform.rotation, rotation, homingProjectile.RotateSpeed);
        Projectile.Rigidbody.MoveRotation(direction);
    }
}
