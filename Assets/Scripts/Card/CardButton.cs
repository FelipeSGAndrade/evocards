using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardButton : MonoBehaviour
{
    public UnityEvent OnClick;

    [SerializeField]
    private CardUI parentCardUI = default;

    private Vector2 mouseClickPosition;

    void OnMouseDown() {
        mouseClickPosition = Utils.GetMousePosition();
        parentCardUI.OnMouseDown();
    }

    void OnMouseUp() {
        Vector2 currentMousePosition = Utils.GetMousePosition();

        if (mouseClickPosition == currentMousePosition)
        {
            OnClick.Invoke();
        }

        if (parentCardUI)
            parentCardUI.OnMouseUp();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
