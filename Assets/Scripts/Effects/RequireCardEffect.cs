﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireCardEffect : CardEffect
{
    [SerializeField]
    private Card card = default;

    [SerializeField]
    private int amount = default;

    public override int Verify()
    {
        List<Card> selection = CardsManager.instance.GetSelection();

        int count = 0;
        foreach (Card card in selection)
        {
            if (card.name == this.card.name)
                count++;

            if (count >= amount)
                return amount;
        }

        return -1;
    }

    public override void Use()
    {
    }
}
