using UnityEngine;

public class Player2Controller : PlayerController
{
    public override void LoadPlayer(ShipScriptableObject shipScriptableObject)
    {
        base.LoadPlayer(shipScriptableObject);
        HorizontalAxis = "Horizontal 2";
        VerticalAxis = "Vertical 2";
        TargetGameObjectTag = "Player1";
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartAbility(AbilityId.Primary);
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            StopAbility(AbilityId.Primary);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartAbility(AbilityId.Segundary);
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            StopAbility(AbilityId.Segundary);
        }
    }
}
