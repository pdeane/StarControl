using UnityEngine;

public interface IForcible
{
    int Acceleration { get; }
    int MaximumVelocity { get; }
    Rigidbody Rigidbody { get; }
}
