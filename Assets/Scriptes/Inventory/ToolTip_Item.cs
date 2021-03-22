using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip_Item : MonoBehaviour
{
    public static ToolTip_Item instation;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _textType;
    [SerializeField] private TextMeshProUGUI _textChar;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField] private Image _image;

    private void Awake()
    {
        instation = this;
    }

    public void onTool(ItemObject itemObject)
    {
        _panel.SetActive(true);
        _textType.text = String.Format(itemObject._classItem._typeEquip.ToString() + "'\n Name - " + itemObject._classItem._name);
        _textChar.text = String.Format(itemObject._classItem._typeChar.ToString() + " - " + itemObject._classItem._value);
        _textPrice.text = "Price - " + itemObject._classItem.price.ToString("F0");
        _image.sprite =  itemObject._classItem._sprite;
    }

    public void offTool()
    {
        _panel.SetActive(false);
    }

}
