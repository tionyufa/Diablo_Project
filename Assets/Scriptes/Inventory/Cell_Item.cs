using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Cell_Item : MonoBehaviour 
{
   private ItemObject _item;
   [SerializeField] private Image _image;
   [SerializeField] private RectTransform _rect;
   private bool isPutOn;
   private Transform _inventory;
  
   
   public void ItemRefresh(ItemObject item , Transform _transform)
   {
      _item = item;
      _image.sprite = _item._classItem._sprite;
      _inventory = _transform;
   }
   
   public void PutOnItem()  // настроить количество ячеек
   {
         var item = Inventory.instation.findCell(_item._classItem._typeEquip);
         if (item.transform.childCount == 0 && !isPutOn)
         {
           
            gameObject.transform.SetParent(item.transform);
            PlayerCharacterics.singleton.BonusItem(_item._classItem._typeChar,_item._classItem._value);
            isPutOn = true;
         }

         else if (item.transform.childCount >= 1 && !isPutOn)
         {
            item.transform.GetChild(0).GetComponent<Cell_Item>().ReverceAtInventory();
            gameObject.transform.SetParent(item.transform);
            PlayerCharacterics.singleton.BonusItem(_item._classItem._typeChar,_item._classItem._value);
            isPutOn = true;
         }
         else if (isPutOn)
         {
            ReverceAtInventory();
         }
   }

   public void ReverceAtInventory()
   {
      isPutOn = false;
      gameObject.transform.SetParent(_inventory);
      PlayerCharacterics.singleton.BonusItem(_item._classItem._typeChar,-_item._classItem._value);
   }
   
   

   private void OnMouseEnter()
   {
      ToolTip_Item.instation.onTool(_item);
   }

   private void OnMouseExit()
   {
      ToolTip_Item.instation.offTool();
   }
}
