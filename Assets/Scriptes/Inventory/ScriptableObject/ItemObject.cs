using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item",menuName = "Inventory/Item")]
public class ItemObject : ScriptableObject
{
   public Item _classItem;
  
}

[System.Serializable]
public class Item
{
    public Sprite _sprite;
    public string _name;
    public EquipType _typeEquip;
    public CharacterType _typeChar;
    public float _value;
    public float price;

}

