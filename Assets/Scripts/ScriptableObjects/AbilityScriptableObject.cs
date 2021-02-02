using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Ability")]
public class AbilityScriptableObject : ScriptableObject
{
    public AbilityType abilityType;
    public AudioClip audioClip;
    public float cooldown;
    public int energyCost;
    public ProjectileScriptableObject projectileScriptableObject;
}
