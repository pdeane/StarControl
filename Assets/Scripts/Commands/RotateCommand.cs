using UnityEngine;

public class RotateCommand : ICommand
{
    private float Axis { get; }
    private IRotatable Rotatable { get; }

    public RotateCommand(IRotatable rotatable, float axis)
    {
        Axis = axis;
        Rotatable = rotatable;
    }

    public void Execute()
    {
        Rotatable.GameObject.transform.Rotate(Vector3.up, Axis * Time.deltaTime * Rotatable.RotateSpeed);
    }
}
