using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSettings : MonoBehaviour
{
    [SerializeField]
    private ActionType actionType = default;
    public ActionType ActionType => actionType;

    public int Verify() {
        CardEffect[] effects = GetComponents<CardEffect>();
        return Verify(effects);
    }

    private int Verify(CardEffect[] effects)
    {
        int score = 0;
        foreach (CardEffect effect in effects) {
            int effectScore = effect.Verify();
            if (effectScore == -1)
                return -1;

            score += effectScore;
        }

        return score;
    }

    public void Use()
    {
        CardEffect[] effects = GetComponents<CardEffect>();
        foreach (CardEffect effect in effects) {
            effect.Use();
        }
    }
}
