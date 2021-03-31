using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyItem",menuName = "MyInventory")]
public class MyInventoryObj : ScriptableObject
{
   public List<ItemObject> _Myitems;
}
