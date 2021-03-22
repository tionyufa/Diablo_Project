using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCell_Item : MonoBehaviour
{
   [SerializeField] private Equip _equip;

   public Equip CellEquip()
   {
      return _equip;
   }
}
