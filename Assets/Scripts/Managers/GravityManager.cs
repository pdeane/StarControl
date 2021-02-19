using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] private float gravityForce;

    private List<Rigidbody> Rigidbodies { get; } = new List<Rigidbody>();

    private void Awake()
    {
        EventBroker<Rigidbody>.Subscribe(EventWithArgs.RigidBodyAdded, OnRigidBodyAdded);
        EventBroker<Rigidbody>.Subscribe(EventWithArgs.RigidBodyRemoved, OnRigidBodyRemoved);
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody rigidbody in Rigidbodies)
        {
            Vector3 delta = transform.position - rigidbody.position;
            Vector3 force = delta.normalized * (1 / delta.magnitude) * gravityForce;
            if (!float.IsNaN(force.x))
            {
                rigidbody.AddForce(force * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }

    private void OnDestroy()
    {
        EventBroker<Rigidbody>.Unsubscribe(EventWithArgs.RigidBodyAdded, OnRigidBodyAdded);
        EventBroker<Rigidbody>.Unsubscribe(EventWithArgs.RigidBodyRemoved, OnRigidBodyRemoved);
    }

    private void OnRigidBodyAdded(Rigidbody rigidbody)
    {
        Rigidbodies.Add(rigidbody);
    }

    private void OnRigidBodyRemoved(Rigidbody rigidbody)
    {
        Rigidbodies.Remove(rigidbody);
    }
}
