using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ship")]
public class ShipScriptableObject : ScriptableObject
{
    public AbilityScriptableObject[] abilityScriptableObjects;
    public int acceleration;
    public int crew;
    public int energy;
    public float energyRegeneration;
    public int maximumVelocity;
    public GameObject prefabGameObject;
    public int rotateSpeed;
}
