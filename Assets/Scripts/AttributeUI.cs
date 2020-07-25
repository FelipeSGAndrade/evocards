using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour
{
    [SerializeField]
    private AttributeManager attributeManager = default;

    [SerializeField]
    private AttributeType attribute = default;

    [SerializeField]
    private Text valueText = default;

    void Start()
    {
        if (attributeManager && attribute != AttributeType.NONE)
        {
            Refresh();
            attributeManager.onChange.AddListener(Refresh);
        }
    }

    void Refresh()
    {
        valueText.text = attributeManager.GetCurrentValue(attribute).ToString();
    }
}
