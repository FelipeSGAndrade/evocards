using System;
using UnityEngine;

public class ActionButtonUI : MonoBehaviour
{
    public Action<ActionType> OnClick = delegate { };

    private ActionType actionType = default;

    public void initialize(ActionType type)
    {
        this.actionType = type;
    }

    void OnMouseUp() {
        OnClick(actionType);
    }
}
