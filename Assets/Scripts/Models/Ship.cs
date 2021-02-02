using UnityEngine;

public class Ship : IForcible, IRotatable
{
    public Ability[] Abilities { get; set; }
    public int Acceleration { get; set; }
    public int Crew { get; set; }
    public float Energy { get; set; }
    public float EnergyRegeneration { get; set; }
    public GameObject GameObject { get; set; }
    public int MaximumEnergy { get; set; }
    public int MaximumVelocity { get; set; }
    public Rigidbody Rigidbody { get; set; }
    public int RotateSpeed { get; set; }
}
