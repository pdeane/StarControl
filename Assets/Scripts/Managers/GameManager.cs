using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool isGameRunning = true;

    [SerializeField] private Player1Controller player1Controller;
    [SerializeField] private Player2Controller player2Controller;
    [SerializeField] private ShipScriptableObject[] shipScriptableObjects;

    private void Awake()
    {
        player1Controller.LoadPlayer(shipScriptableObjects[0]);
        player2Controller.LoadPlayer(shipScriptableObjects[0]);
    }
}
