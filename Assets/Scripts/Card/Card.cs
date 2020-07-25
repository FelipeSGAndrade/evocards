using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public Action OnCardChanged = delegate { };
    public static Action<Card> OnCardDestroyed = delegate { };
    public static Action<Card> OnCardUsed = delegate { };

    [SerializeField]
    private string description = default;
    public string Description => description;

    [SerializeField]
    private Sprite artwork = default;
    public Sprite Artwork => artwork;

    [SerializeField]
    private CardType type = default;
    public CardType Type => type;

    [SerializeField]
    private bool isSingleUseDestroy = default;
    public bool IsSingleUseDestroy => isSingleUseDestroy;

    [SerializeField]
    private bool isSingleUseDisable = default;
    public bool IsSingleUseDisable => isSingleUseDisable;

    [SerializeField]
    private List<CardType> usableWithTypes = default;

    [SerializeField]
    private List<EffectSettings> cardEffects = default;

    public bool IsHighlighted { get; private set; }
    public bool IsCurrentAction { get; private set; }
    public bool IsDepressed { get; private set; }
    public bool IsSelected { get; private set; }
    public bool IsDisabled { get; private set; }

    public void Use()
    {
        if (IsDisabled || Type != CardType.ACTION)
            return;

        if (!IsCurrentAction)
            PrepareAction();
        else
            ExecuteAction();
    }

    private void PrepareAction()
    {
        IsCurrentAction = true;
        if (usableWithTypes.Count > 0)
            CardsManager.instance.Highlight(usableWithTypes);
        else
            CardsManager.instance.Highlight(new List<CardType>() { CardType.NONE });

        OnCardChanged();
    }

    private void ExecuteAction()
    {
        bool success = ApplyEffects();

        if (success)
        {
            if (isSingleUseDestroy)
                Destroy(this);
            else if (isSingleUseDisable)
                IsDisabled = true;

            OnCardUsed(this);
        }

        IsCurrentAction = false;
        CardsManager.instance.ClearHighlight();

        OnCardChanged();
    }

    private bool ApplyEffects()
    {
        int max = 0;
        EffectSettings highestEffect = null;
        foreach (EffectSettings effect in cardEffects) {
            int score = effect.Verify();
            if (score == -1)
                continue;

            if (score > max || !highestEffect)
            {
                max = score;
                highestEffect = effect;
            }
        }

        if (!highestEffect) {
            Debug.Log("Requirements not met");
            return false;
        }

        highestEffect.Use();
        return true;
    }

    public void ClearLight()
    {
        if (IsCurrentAction || IsDisabled)
            return;

        IsHighlighted = false;
        IsDepressed = false;
        IsSelected = false;

        OnCardChanged();
    }

    public void Highlight(List<CardType> types)
    {
        if (IsCurrentAction || IsDisabled)
            return;

        if (types.Contains(this.Type)) {
            IsHighlighted = true;
        } else {
            IsDepressed = true;
        }

        OnCardChanged();
    }

    public void Select()
    {
        if (!IsHighlighted)
            return;

        if (!IsSelected) {
            CardsManager.instance.AddSelection(this);
            IsSelected = true;
        } else {
            CardsManager.instance.RemoveSelection(this);
            IsSelected = false;
        }

        OnCardChanged();
    }

    public void Enable() {
        IsDisabled = false;
        OnCardChanged();
    }

    void OnDestroy()
    {
        Destroy(gameObject);
        OnCardDestroyed(this);
    }
}
