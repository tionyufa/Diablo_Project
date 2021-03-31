using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class List_ItemObject : MonoBehaviour
{
   public List<GameObject> _Bags;
   public static List_ItemObject Instation;
   public List<ItemObject> _ListItems;
   private Dictionary<int, ItemObject> _dictionary = new Dictionary<int, ItemObject>();

   private void Start()
   {
      Instation = this;
      for (int i = 0; i < _ListItems.Count; i++)
      {
         _dictionary.Add(i,_ListItems[i]);
      }
   }

   public ItemObject RandomItem()
   {
      int random = Random.Range(0, _ListItems.Count);
      return _dictionary[random];
   }

   public GameObject respawnBags()
   {
      int random = Random.Range(0, 100);
      if (random <= 15)
      {
         for (int i = 0; i < _Bags.Count; i++)
         {
            if (!_Bags[i].activeSelf)
            {
               return _Bags[i];
            }
         }
      }

      return null;
   }
}
