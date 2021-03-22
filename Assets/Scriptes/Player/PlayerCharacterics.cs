using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCharacterics : MonoBehaviour 
{
   [SerializeField] private Animator _animator;
   [SerializeField] private Canvas _canvas;
   [SerializeField] private TextMeshProUGUI _text;
   private Camera _camera;
   public static PlayerCharacterics singleton;
   private float _maxHealts,_currentHealts, _damage, _crtChance, _shield ;
   private float _time, _value;
   private Dictionary<typeChar, float> _dictionary = new Dictionary<typeChar, float>();
   
   public HitEvent _playerEvent;
   [SerializeField] private TextMeshProUGUI _textStats;
   private void Awake()
   {
      _camera = Camera.main;
      singleton = this;
      _maxHealts = 50f;  
      _damage = 5f;
      _crtChance = 200;
      _shield = 200;
      
      _currentHealts = _maxHealts;
      _dictionary.Add(typeChar.Damage,_damage); _dictionary.Add(typeChar.CritChance,_crtChance);
      _dictionary.Add(typeChar.Healts,_maxHealts); _dictionary.Add(typeChar.Shield,_shield);
   }

   private void Start()
   {
      _playerEvent.AddListener(dmg => ApplyDamage(dmg));
      textStats();
   }

   private void OnGUI()
   {
      textStats();
   }

   public void tempBonus(typeChar stat,float value,float time)
   {
      StartCoroutine(bonus(stat, value , time));
      
   }
   public void BonusItem (typeChar stat, float value)
   {
      _dictionary[stat] += value;
   }

   private void textStats()
   {
      _textStats.text =
         String.Format("My Statistics \nHealts - {0} \nDamage - {1} \nShield - {2} \nCrit.Chance - {3} ",
            _dictionary[typeChar.Healts],_dictionary[typeChar.Damage],_dictionary[typeChar.Shield],_dictionary[typeChar.CritChance]);
   }

   IEnumerator bonus (typeChar stat ,float value, float time)
   {
      float tempValue = _dictionary[stat];
     _dictionary[stat] = _dictionary[stat] + value;
      yield return new WaitForSeconds(time);
     _dictionary[stat] = tempValue;
     
   }
   
   public float GetValue(typeChar stat)
   {
      return _dictionary[stat];
   } 
   
   private void ApplyDamage(float damage)
   {
      float _dmg = damage * (1 - _dictionary[typeChar.Shield] / 1000);
      _currentHealts -= _dmg;
      TextDamage(_dmg);
      if (_currentHealts <= 0)
      {
         _animator.SetBool(nameAnimator.Dead,true);
      }
   }

   private void TextDamage(float damage)
   {
      _canvas.transform.LookAt(_camera.transform);
      _text.CrossFadeAlpha(10f,0.1f,false);
      _text.text = damage.ToString();
      _text.CrossFadeAlpha(0f,1f,false);
   }
   
}





