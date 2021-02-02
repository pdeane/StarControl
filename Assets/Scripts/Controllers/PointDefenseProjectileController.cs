using UnityEngine;

public class PointDefenseProjectileController : ProjectileController
{
    public GameObject TargetGameObject { get; set; }

    protected override void Start()
    {
        base.Start();
        Vector3 delta = TargetGameObject.transform.position - transform.position;
        transform.localScale = new Vector3(1, 1, delta.magnitude / 2);
        transform.position = (TargetGameObject.transform.position + transform.position) / 2;
    }
}
