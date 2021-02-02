using UnityEngine;

public class Player1Controller : PlayerController
{
    public override void LoadPlayer(ShipScriptableObject shipScriptableObject)
    {
        base.LoadPlayer(shipScriptableObject);
        HorizontalAxis = "Horizontal 1";
        VerticalAxis = "Vertical 1";
        TargetGameObjectTag = "Player2";
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Period))
        {
            StartAbility(AbilityId.Primary);
        }
        if (Input.GetKeyUp(KeyCode.Period))
        {
            StopAbility(AbilityId.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            StartAbility(AbilityId.Segundary);
        }
        if (Input.GetKeyUp(KeyCode.Comma))
        {
            StopAbility(AbilityId.Segundary);
        }
    }
}
