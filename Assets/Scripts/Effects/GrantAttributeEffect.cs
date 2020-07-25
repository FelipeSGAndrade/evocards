using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrantAttributeEffect : CardEffect
{
    [SerializeField]
    private AttributeType type = default;

    [SerializeField]
    private int amount = default;

    public override int Verify()
    {
        return 0;
    }

    public override void Use()
    {
        AttributeManager.instance.Increase(type, amount);
    }
}
