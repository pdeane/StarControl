using UnityEngine;

public class Ability
{
    public AbilityType AbilityType { get; set; }
    public AudioClip AudioClip { get; set; }
    public float Cooldown { get; set; }
    public int EnergyCost { get; set; }
    public ProjectileScriptableObject ProjectileScriptableObject { get; set; }
}
