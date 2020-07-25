using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCardEffect : CardEffect
{
    [SerializeField]
    private Card card = default;

    [SerializeField]
    private int amount = default;

    public override int Verify()
    {
        return 0;
    }
    public override void Use()
    {
        CardsManager.instance.CreateCard(card, amount);
    }
}
