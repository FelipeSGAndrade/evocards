using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    [SerializeField]
    private CardUI cardUIPrefab = default;

    [SerializeField]
    private List<Card> initialCards = default;

    [SerializeField]
    private Transform cardSpawn = default;

    public static CardsManager instance;

    private List<Card> cardsOnTable = new List<Card>();
    private List<Card> selectedCards = new List<Card>();
    private Card currentAction = null;

    void Start()
    {
        if (instance)
            throw new Exception("Cant have more than one CardsManager in Scene");
        
        instance = this;

        foreach (Card card in initialCards)
        {
            CreateCard(card);
        }
    }

    public void CreateCard(Card cardPrefab, int amount)
    {
        for (int i = 0; i < amount; i++) {
            CreateCard(cardPrefab);
        }
    }

    public void CreateCard(Card cardPrefab)
    {
        Vector3 cardPosition;
        if (cardSpawn)
            cardPosition = cardSpawn.position;
        else
            cardPosition = new Vector3(0, 0, 0);

        cardPosition.z -= cardsOnTable.Count;

        CardUI cardUI = CardUI.Instantiate(cardUIPrefab, cardPosition, Quaternion.identity);

        Card card = Card.Instantiate(cardPrefab);
        card.name = cardPrefab.name;

        cardUI.Display(card);
        cardsOnTable.Add(card);
    }

    public void DestroyCard(Card card)
    {
        cardsOnTable.Remove(card);
        if (selectedCards.Contains(card)) 
            selectedCards.Remove(card);

        Destroy(card);
    }

    public void Highlight(List<CardType> types)
    {
        foreach (Card card in cardsOnTable)
            card.Highlight(types);
    }

    public void ClearHighlight()
    {
        foreach (Card card in cardsOnTable)
            card.ClearLight();

        selectedCards.Clear();
    }

    public bool IsActionHappening()
    {
        return currentAction != null;
    }

    public void RegisterAction(Card card)
    {
        if (IsActionHappening()) {
            Debug.LogError("There is already an action in course");
            return;
        }

        currentAction = card;
    }

    public void AddSelection(Card card)
    {
        selectedCards.Add(card);
    }

    public void RemoveSelection(Card card)
    {
        selectedCards.Remove(card);
    }

    public List<Card> GetSelection()
    {
        return selectedCards;
    }
}
