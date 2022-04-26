using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionSelector : MonoBehaviour
{
    [SerializeField]
    private ActionButtonUI actionButtonPrefab = default;
    private CardUI cardUI;
    private List<ActionType> actionTypes;
    private GameObject menuContainer;


    void Start() {
        cardUI = GetComponent<CardUI>();

        if (!cardUI) {
            throw new Exception("No card found for action selector");
        }

        actionTypes = this.cardUI.GetActionTypes();

    }

    public void ShowSelection()
    {
        Debug.Log("Show Selection");
        if (!menuContainer){
            CreateSelection();
        }

        menuContainer.SetActive(true);
    }

    private void CreateSelection()
    {
        menuContainer = new GameObject();
        menuContainer.transform.SetParent(cardUI.transform);
        menuContainer.name = "Action Selector Container";

        Debug.Log("Action Types:" + actionTypes.Count);
        foreach (ActionType type in actionTypes)
        {
            var position = cardUI.transform.position + new Vector3(2, 0, 0);
            var button = ActionButtonUI.Instantiate(actionButtonPrefab, position, Quaternion.identity);
            button.OnClick += Select;
            button.transform.SetParent(menuContainer.transform);
            Debug.Log("Created " + type + " button");
        }
    }

    private void Select(ActionType type)
    {
        menuContainer.SetActive(false);
        cardUI.UseAction(type);
    }
}
