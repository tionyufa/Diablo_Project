using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType { Damage, Healts, Shield, CritChance }
public enum EquipType { Helmet, Armor, Gloves, Boots, Weapon, Amulet }

public class Inventory : MonoBehaviour
{
    public static Inventory instation;
    [SerializeField] private List<MyCell_Item> _myCellItems;
    [SerializeField] private Transform InventoryList;
    [SerializeField] private Cell_Item _cell;
    public MyInventoryObj _MyInventoryObj;

    private void Start()
    {
        instation = this;
        for (int i = 0; i < _MyInventoryObj._Myitems.Count; i++)
        {
            var cell = Instantiate(_cell, InventoryList);
            cell.ItemRefresh(_MyInventoryObj._Myitems[i],InventoryList);
        }
    }

    public void AddItem(ItemObject _item)
    {
        _MyInventoryObj._Myitems.Add(_item);
       var cell = Instantiate(_cell, InventoryList);
       cell.ItemRefresh(_item,InventoryList);
    }

    public GameObject findCell(EquipType _equip)
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

