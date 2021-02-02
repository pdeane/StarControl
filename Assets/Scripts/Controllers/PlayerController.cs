using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    public GameObject GameObject => ShipManager.gameObject;

    protected string HorizontalAxis { get; set; }
    protected string TargetGameObjectTag { get; set; }
    protected string VerticalAxis { get; set; }

    private AudioSource AudioSource { get; set; }
    private ICommand Command { get; set; }
    private bool[] AbilitiesTurnedOn { get; set; } = new bool[2];
    private ShipManager ShipManager { get; set; }

    public virtual void LoadPlayer(ShipScriptableObject shipScriptableObject)
    {
        GameObject shipGameObject = Instantiate(shipScriptableObject.prefabGameObject, transform);
        AudioSource = GetComponent<AudioSource>();
        ShipManager = shipGameObject.GetComponent<ShipManager>();
        ShipManager.CreateShip(shipScriptableObject);
        _ = StartCoroutine(UseAbility(AbilityId.Primary));
        _ = StartCoroutine(UseAbility(AbilityId.Segundary));
    }

    protected virtual void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis(VerticalAxis);
        if (verticalAxis != 0)
        {
            Command = new ForceCommand(ShipManager.Ship, verticalAxis);
            Command.Execute();
        }
    }

    protected void StartAbility(AbilityId abilityId)
    {
        AbilitiesTurnedOn[(int)abilityId] = true;
    }

    protected void StopAbility(AbilityId abilityId)
    {
        AbilitiesTurnedOn[(int)abilityId] = false;
    }

    protected virtual void Update()
    {
        float horizontalAxis = Input.GetAxis(HorizontalAxis);
        if (horizontalAxis != 0)
        {
            Command = new RotateCommand(ShipManager.Ship, horizontalAxis);
            Command.Execute();
        }
    }

    private GameObject FindNearestTarget()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        gameObjects.Add(GameObject.FindGameObjectWithTag(TargetGameObjectTag).GetComponent<PlayerController>().GameObject);
        gameObjects.AddRange(GameObject.FindGameObjectsWithTag("Projectile"));
        float nearestDistance = float.MaxValue;
        GameObject nearestTarget = null;
        foreach (GameObject gameObject in gameObjects)
        {
            float targetDistance = Vector3.Distance(GameObject.transform.position, gameObject.transform.position);
            if (targetDistance < nearestDistance)
            {
                nearestDistance = targetDistance;
                nearestTarget = gameObject;
            }
        }
        return nearestTarget;
    }

    private IEnumerator UseAbility(AbilityId abilityId)
    {
        Ability ability = ShipManager.Ship.Abilities[(int)abilityId];
        while (GameManager.isGameRunning)
        {
            yield return new WaitUntil(() => AbilitiesTurnedOn[(int)abilityId]);
            switch (ability.AbilityType)
            {
                case AbilityType.Projectile:
                    switch (ability.ProjectileScriptableObject.type)
                    {
                        case ProjectileType.Homing:
                            ShipManager.TargetGameObject = GameObject.FindGameObjectWithTag(TargetGameObjectTag).GetComponent<PlayerController>().GameObject;
                            break;
                        case ProjectileType.PointDefense:
                            ShipManager.TargetGameObject = FindNearestTarget();
                            break;
                    }
                    bool fired = ShipManager.FireProjectile(abilityId);
                    if (fired)
                    {
                        AudioSource.PlayOneShot(ability.AudioClip);
                    }
                    break;
            }
            yield return new WaitForSeconds(ability.Cooldown);
        }
    }
}
