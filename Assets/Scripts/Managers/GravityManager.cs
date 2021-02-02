using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] private float gravityForce;

    private void FixedUpdate()
    {
        Rigidbody[] rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            Vector3 delta = transform.position - rigidbody.position;
            Vector3 force = delta.normalized * (1 / delta.magnitude) * gravityForce;
            if (!float.IsNaN(force.x))
            {
                rigidbody.AddForce(force * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }
}
