using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCell_Item : MonoBehaviour
{
   [SerializeField] private EquipType _equip;

   public EquipType CellEquip()
   {
      return _equip;
   }
}
