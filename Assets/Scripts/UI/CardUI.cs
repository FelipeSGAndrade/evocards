using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CardUIEvent : UnityEvent<CardUI> {}
public class CardUI : MonoBehaviour
{
    public CardUIEvent onClicked;

    [SerializeField]
    private TextMesh title = default;

    [SerializeField]
    private SpriteRenderer artworkRenderer = default;

    [SerializeField]
    private Card card = default;
    public Card Card => card;

    [SerializeField]
    private SpriteRenderer selectHighlight = default;
    [SerializeField]
    private SpriteRenderer actionHighlight = default;

    [SerializeField]
    private SpriteRenderer lowlight = default;
    private ActionSelector actionSelector;

    private bool drag = false;
    private Vector2 mouseStartDrag;
    private Vector2 mouseLastDrag;

    void Start()
    {
        if(!card)
        {
            throw new Exception("No card for cardUI");
        }

        this.name = card.name + "UI";
        transform.SetParent(card.transform);
        actionSelector = GetComponent<ActionSelector>();
        Display(card);
    }

    void Update()
    {
        if (drag)
        {
            Vector2 mousePosition = Utils.GetMousePosition();
            transform.Translate(mousePosition - mouseLastDrag);
            mouseLastDrag = mousePosition;
        }
    }

    public void Display(Card card)
    {
        this.card = card;
        title.text = card.name;
        artworkRenderer.sprite = card.Artwork;

        Refresh();
        card.OnCardChanged += Refresh;
    }

    private void Refresh()
    {
        if (selectHighlight)
            selectHighlight.enabled = card.IsSelected;

        if (actionHighlight)
            actionHighlight.enabled = card.IsCurrentAction;

        if (lowlight)
            lowlight.enabled = card.IsDepressed || card.IsDisabled;
    }

    public List<ActionType> GetActionTypes()
    {
        return card.GetActionTypes();
    }

    public void OnMouseDown()
    {
        drag = true;
        mouseStartDrag = Utils.GetMousePosition();
        mouseLastDrag = mouseStartDrag;
    }

    public void OnMouseUp()
    {
        drag = false;

        if (!HasPositionChanged())
        {
            if (CardsManager.instance.IsActionHappening())
                card.SelectAsTarget();
            else {
                CardsManager.instance.RegisterAction(card);
                card.SelectAsAction();
                actionSelector.ShowSelection();
            }
        }
    }

    private bool HasPositionChanged()
    {
        Vector2 currentMousePosition = Utils.GetMousePosition();
        return mouseStartDrag != currentMousePosition;
    }

    public void ButtonClick()
    {
        onClicked.Invoke(this);
    }

    public void UseAction(ActionType actionType)
    {
        card.Use(actionType);
    }
}
