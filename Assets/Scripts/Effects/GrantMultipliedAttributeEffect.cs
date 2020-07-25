using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantMultipliedAttributeEffect : CardEffect
{
    [SerializeField]
    private AttributeType grantingType = default;

    [SerializeField]
    private AttributeType multiplierType = default;

    [SerializeField]
    private float factor = default;

    [SerializeField]
    private int minIncrease = default;

    public override int Verify()
    {
        return 0;
    }

    public override void Use()
    {
        int multiplierValue = AttributeManager.instance.GetCurrentValue(multiplierType);
        int increaseAmount = Mathf.FloorToInt(multiplierValue * factor);

        if (minIncrease > 0 && increaseAmount < minIncrease)
            increaseAmount = minIncrease;

        AttributeManager.instance.Increase(grantingType, increaseAmount);
    }
}
