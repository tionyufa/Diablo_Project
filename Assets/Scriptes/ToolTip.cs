using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public static ToolTip singleton;
   [SerializeField] private GameObject _panel;
   [SerializeField] private TextMeshProUGUI _name, _destration;
   [SerializeField] private Image _image;
   private Enemy _enemyChar;

   private void Awake()
   {
       singleton = this;
     
   }

   private void OnGUI()
   {
       if (_panel.activeSelf)
       {
           _image.fillAmount = _enemyChar.currentHP / _enemyChar.maxHealts;
       }
   }


   public void ToolTipAction(Enemy enemyChar)
   {
       _enemyChar = enemyChar;
       _panel.SetActive(true);
       _name.text = _enemyChar.Name;
       _destration.text = _enemyChar._myTyp.ToString();
       _enemyChar.AddOutline();
       
   }

   

   public void ToolTipExit()
   {
       _panel.SetActive(false);
       _enemyChar.ShowOutline();
   }
 }
