using System.Collections;
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
    private CardButton actionButton = default;

    [SerializeField]
    private Card card = default;

    [SerializeField]
    private SpriteRenderer selectHighlight = default;
    [SerializeField]
    private SpriteRenderer actionHighlight = default;

    [SerializeField]
    private SpriteRenderer lowlight = default;

    private bool drag = false;
    private Vector2 mouseStartDrag;
    private Vector2 mouseLastDrag;

    void Start()
    {
        if (card)
        {
            this.name = card.name + "UI";
            transform.SetParent(card.transform);
            Display(card);
        }
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

        if (card.Type != CardType.ACTION)
        {
            actionButton.Deactivate();
        }

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

    public void OnMouseDown()
    {
        drag = true;
        mouseStartDrag = Utils.GetMousePosition();
        mouseLastDrag = mouseStartDrag;
    }

    public void OnMouseUp()
    {
        drag = false;

        Vector2 currentMousePosition = Utils.GetMousePosition();
        if (mouseStartDrag == currentMousePosition)
            card.Select();
    }

    public void ButtonClick()
    {
        onClicked.Invoke(this);
    }

    public void UseAction()
    {
        if (card)
        {
            card.Use();
        }
    }
}
