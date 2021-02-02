using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Projectile")]
public class ProjectileScriptableObject : ScriptableObject
{
    public int damage;
    public LaunchPosition launchPosition;
    public int maxDistance;
    public ParticleSystem prefabParticleSystem;
    public GameObject prefabGameObject;
    public int range;
    public float rotateSpeed;
    public float timeToLive;
    public ProjectileType type;
    public int velocity;
}
