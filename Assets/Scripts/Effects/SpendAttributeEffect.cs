using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpendAttributeEffect : CardEffect
{
    [SerializeField]
    private AttributeType type = default;

    [SerializeField]
    private int amount = default;

    public override int Verify()
    {
        int current = AttributeManager.instance.GetCurrentValue(type);
        if (current < amount)
            return -1;

        return 0;
    }

    public override void Use()
    {
        AttributeManager.instance.Decrease(type, amount);
    }
}
