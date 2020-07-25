using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttributeManager : MonoBehaviour
{
    public UnityEvent onChange;

    public static AttributeManager instance;

    private int HP;
    private int MaxHP = 10;

    private int Stamina;
    private int MaxStamina = 10;

    private int Reason;
    private int MaxReason = 3;

    void Start()
    {
        if (instance)
            throw new Exception("Cant have more than one AttributesManager in Scene");

        instance = this;
        HP = MaxHP;
        Stamina = MaxStamina;
        Reason = MaxReason;

        onChange.Invoke();
    }
    public int GetCurrentValue(AttributeType attribute)
    {
        switch (attribute)
        {
            case AttributeType.HP:
                return HP;
            case AttributeType.STAMINA:
                return Stamina;
            case AttributeType.REASON:
                return Reason;
            default:
                return 0;
        }
    }

    public void Increase(AttributeType attribute, int amount)
    {
        switch (attribute)
        {
            case AttributeType.HP:
                HP += amount;
                if (HP > MaxHP) HP = MaxHP;
                break;
            case AttributeType.STAMINA:
                Stamina += amount;
                if (Stamina > MaxStamina) Stamina = MaxStamina;
                break;
            case AttributeType.REASON:
                Reason += amount;
                if (Reason > MaxReason) Reason = MaxReason;
                break;
        }

        onChange.Invoke();
    }

    public void Decrease(AttributeType attribute, int amount)
    {
        switch (attribute)
        {
            case AttributeType.HP:
                HP -= amount;
                if (HP < 0) HP = 0;
                break;
            case AttributeType.STAMINA:
                Stamina -= amount;
                if (Stamina < 0) Stamina = 0;
                break;
            case AttributeType.REASON:
                Reason -= amount;
                if (Reason < 0) Reason = 0;
                break;
        }

        onChange.Invoke();
    }
}
