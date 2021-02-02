using UnityEngine;

public class ForceCommand : ICommand
{
    private float Axis { get; }
    private IForcible Forcible { get; }

    public ForceCommand(IForcible forcible, float axis)
    {
        Axis = axis;
        Forcible = forcible;
    }

    public void Execute()
    {
        if (Forcible.Rigidbody.velocity.magnitude <= Forcible.MaximumVelocity)
        {
            Forcible.Rigidbody.AddRelativeForce(Vector3.forward * Axis * Time.deltaTime * Forcible.Acceleration);
        }
        else
        {
            Forcible.Rigidbody.velocity = Forcible.Rigidbody.velocity.normalized * (Forcible.MaximumVelocity - 0.01f);
        }
    }
}
