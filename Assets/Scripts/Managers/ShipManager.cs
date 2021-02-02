using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ShipManager : MonoBehaviour
{
    public Ship Ship { get; private set; }
    public GameObject TargetGameObject { get; set; }

    public void CreateShip(ShipScriptableObject shipScriptableObject)
    {
        Ship = new Ship
        {
            Abilities = shipScriptableObject.abilityScriptableObjects
                .Select(abilityScriptableObject => new Ability
                {
                    AbilityType = abilityScriptableObject.abilityType,
                    AudioClip = abilityScriptableObject.audioClip,
                    Cooldown = abilityScriptableObject.cooldown,
                    EnergyCost = abilityScriptableObject.energyCost,
                    ProjectileScriptableObject = abilityScriptableObject.projectileScriptableObject,
                })
                .ToArray(),
            Acceleration = shipScriptableObject.acceleration,
            Crew = shipScriptableObject.crew,
            Energy = shipScriptableObject.energy,
            EnergyRegeneration = shipScriptableObject.energyRegeneration,
            GameObject = gameObject,
            MaximumEnergy = shipScriptableObject.energy,
            MaximumVelocity = shipScriptableObject.maximumVelocity,
            Rigidbody = gameObject.GetComponent<Rigidbody>(),
            RotateSpeed = shipScriptableObject.rotateSpeed,
        };
        _ = StartCoroutine(RegenerateEnergy());
    }

    public bool FireProjectile(AbilityId abilityId)
    {
        Ability ability = Ship.Abilities[(int)abilityId];
        ProjectileScriptableObject projectileScriptableObject = ability.ProjectileScriptableObject;
        float targetDistance = projectileScriptableObject.range == 0 ? 0 : Vector3.Distance(transform.position, TargetGameObject.transform.position);
        if (Ship.Energy >= ability.EnergyCost && projectileScriptableObject.range >= targetDistance)
        {
            Ship.Energy -= ability.EnergyCost;
            CreateProjectile(projectileScriptableObject);
            return true;
        }
        return false;
    }

    private void CreateProjectile(ProjectileScriptableObject projectileScriptableObject)
    {
        Projectile projectile;
        GameObject projectileGameObject;
        switch (projectileScriptableObject.launchPosition)
        {
            case LaunchPosition.Around:
                Vector3 delta = TargetGameObject.transform.position - transform.position;
                projectileGameObject = Instantiate(projectileScriptableObject.prefabGameObject,
                                                   transform.position + delta.normalized * 5,
                                                   Quaternion.LookRotation(delta));
                break;
            default:
            case LaunchPosition.Front:
                projectileGameObject = Instantiate(projectileScriptableObject.prefabGameObject,
                                                   transform.position + transform.forward.normalized * 5,
                                                   transform.rotation);
                break;
        }
        switch (projectileScriptableObject.type)
        {
            case ProjectileType.Default:
                projectile = new Projectile(projectileScriptableObject.damage,
                                            transform.position,
                                            projectileGameObject,
                                            projectileScriptableObject.maxDistance,
                                            projectileScriptableObject.timeToLive,
                                            projectileScriptableObject.velocity);
                ProjectileController projectileController = projectileGameObject.GetComponent<ProjectileController>();
                projectileController.Projectile = projectile;
                break;
            case ProjectileType.Homing:
                projectile = new HomingProjectile(projectileScriptableObject.damage,
                                                  transform.position,
                                                  projectileGameObject,
                                                  projectileScriptableObject.maxDistance,
                                                  projectileScriptableObject.rotateSpeed,
                                                  projectileScriptableObject.timeToLive,
                                                  projectileScriptableObject.velocity);
                HomingProjectileController homingProjectileController = projectileGameObject.GetComponent<HomingProjectileController>();
                homingProjectileController.Projectile = projectile;
                homingProjectileController.TargetGameObject = TargetGameObject;
                break;
            case ProjectileType.PointDefense:
                projectile = new PointDefenseProjectile(projectileScriptableObject.damage,
                                                        transform.position);
                PointDefenseProjectileController pointDefenseProjectileController = projectileGameObject.GetComponent<PointDefenseProjectileController>();
                pointDefenseProjectileController.Projectile = projectile;
                pointDefenseProjectileController.TargetGameObject = TargetGameObject;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Asteroid":
                Ship.Crew -= 1;
                Debug.Log(Ship.Crew);
                break;
            case "GravityWell":
                Ship.Crew -= 1;
                Debug.Log(Ship.Crew);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Projectile":
                ProjectileController projectileController = other.GetComponent<ProjectileController>();
                if (projectileController != null)
                {
                    Ship.Crew -= projectileController.Projectile.Damage;
                    ParticleSystem particleSystem = projectileController.GetComponentInChildren<ParticleSystem>();
                    if (particleSystem != null)
                    {
                        particleSystem.transform.parent = null;
                        particleSystem.Stop();
                        Destroy(particleSystem.gameObject, 5f);
                    }
                    Destroy(other.gameObject, 0.1f);
                    Debug.Log(Ship.Crew);
                }
                break;
        }
    }

    private IEnumerator RegenerateEnergy()
    {
        while (GameManager.isGameRunning)
        {
            Ship.Energy = Math.Min(Ship.MaximumEnergy, Ship.Energy + Ship.EnergyRegeneration);
            yield return new WaitForSeconds(1);
        }
    }
}
