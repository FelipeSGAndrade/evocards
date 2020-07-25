using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableOnCardUsed : MonoBehaviour
{
    [SerializeField]
    private Card observedCard = default;

    private Card parentCard;

    void Start() {
        parentCard = GetComponent<Card>();

        Card.OnCardUsed += CardUsed;
    }

    void CardUsed(Card usedCard) {
        if (usedCard.name == observedCard.name) {
            parentCard.Enable();
        }
    }
}