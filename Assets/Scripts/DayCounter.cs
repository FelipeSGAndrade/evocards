using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    public static Action OnDayChanged;

    [SerializeField]
    private TextMeshPro text = default;

    private int day = 1;

    void Start()
    {
        Card.OnCardUsed += CardUsed;
        OnDayChanged += UpdateDay;
        UpdateDay();
    }

    void CardUsed(Card usedCard) {
        if (usedCard.name != "Sleep") return;

        day++;
        OnDayChanged();
    }

    void UpdateDay() {
        text.text = "Day: " + day;
    }
}
