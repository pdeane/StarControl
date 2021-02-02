using System.Collections.Generic;
using UnityEngine;

public class AsteroidBeltManager : MonoBehaviour
{
    [SerializeField] private int asteroidCount;
    [SerializeField] private GameObject asteroidPrefab;

    private List<(Rigidbody Rigidbody, float Orbit)> Asteroids { get; set; }
    private int InstantiateDistance { get; } = 500;

    private void FixedUpdate()
    {
        foreach ((Rigidbody Rigidbody, float Orbit) asteroid in Asteroids)
        {
            Vector3 delta = Vector3.zero - asteroid.Rigidbody.position;
            asteroid.Rigidbody.AddForce(delta.normalized * asteroid.Orbit, ForceMode.Acceleration);
            Vector3 degree90 = Vector3.Cross(delta.normalized, Vector3.up);
            asteroid.Rigidbody.AddForce(degree90.normalized * asteroid.Orbit, ForceMode.Acceleration);
        }
    }

    private void Start()
    {
        Asteroids = new List<(Rigidbody Rigidbody, float Orbit)>();
        for (int it = 0; it < asteroidCount; it++)
        {
            float angle = Random.Range(0f, 360f);
            float positionX = Mathf.Cos(angle);
            float positionZ = Mathf.Sin(angle);
            GameObject asteroidGameObject = Instantiate(asteroidPrefab, new Vector3(positionX * InstantiateDistance, 0, positionZ * InstantiateDistance), asteroidPrefab.transform.rotation, transform);
            Rigidbody asteroidRigidbody = asteroidGameObject.GetComponent<Rigidbody>();
            float orbit = Random.Range(30f, 40f);
            Asteroids.Add((asteroidRigidbody, orbit));
        }
    }
}
