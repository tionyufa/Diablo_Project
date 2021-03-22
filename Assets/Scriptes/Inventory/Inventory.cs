using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instation;
    [SerializeField] private List<MyCell_Item> _myCellItems;
    [SerializeField] private Transform InventoryList;
    [SerializeField] private Cell_Item _cell;

    private void Start()
    {
        instation = this;
    }

    public void AddItem(ItemObject _item)
    {
       var cell = Instantiate(_cell, InventoryList);
       cell.ItemRefresh(_item,InventoryList);
    }

    public GameObject findCell(Equip _equip)
    {
        for (int i = 0; i < _myCellItems.Count; i++)
        {
            if (_myCellItems[i].CellEquip() == _equip)
            {
                return _myCellItems[i].gameObject;
            }
        }

        return null;
    }
}

public enum typeChar
{
    Damage, Healts, Shield, CritChance
}
public enum Equip
{
    Helmet, Armor, Gloves,
    Boots, Weapon, Amulet
}
