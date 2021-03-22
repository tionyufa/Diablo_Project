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
   private Dictionary<Stat, float> _dictionary = new Dictionary<Stat, float>();
   
   public HitEvent _playerEvent;
   private void Awake()
   {
      _camera = Camera.main;
      singleton = this;
      _maxHealts = 50f;  //Object
      _damage = 5f;
      _crtChance = 20;
      _shield = 20;
      
      _currentHealts = _maxHealts;
      _dictionary.Add(Stat.Damage,_damage); _dictionary.Add(Stat.Crit,_crtChance);
      _dictionary.Add(Stat.Healts,_maxHealts); _dictionary.Add(Stat.Shield,_shield);
   }

   private void Start()
   {
      _playerEvent.AddListener(dmg => ApplyDamage(dmg));
   }

   public void tempBonus(Stat stat,float value,float time)
   {
      
      StartCoroutine(bonus(stat, value , time));
      
   }

   IEnumerator bonus (Stat stat ,float value, float time)
   {
      float tempValue = _dictionary[stat];
     _dictionary[stat] = _dictionary[stat] + value;
      yield return new WaitForSeconds(time);
     _dictionary[stat] = tempValue;
     
   }
   
   public float GetValue(Stat stat)
   {
      return _dictionary[stat];
   } 
   
   private void ApplyDamage(float damage)
   {
      float _dmg = damage * (1 - _dictionary[Stat.Shield] / 100);
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



public enum Stat
{
   Healts,
   Damage,
   Crit,
   Shield
}

