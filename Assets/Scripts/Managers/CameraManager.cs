using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Player1Controller player1Controller;
    [SerializeField] private Player2Controller player2Controller;

    private float Tangent { get; set; }

    private void Start()
    {
        Tangent = Mathf.Tan(Mathf.Deg2Rad * (Camera.main.fieldOfView / 2));
    }

    private void Update()
    {
        float distance = Vector3.Distance(player1Controller.GameObject.transform.position, player2Controller.GameObject.transform.position);
        distance = Mathf.Max(distance + 20, 90);
        Vector3 middle = (player1Controller.GameObject.transform.position + player2Controller.GameObject.transform.position) / 2;
        transform.position = middle + Vector3.up * (distance / (2 * Tangent));
    }
}
