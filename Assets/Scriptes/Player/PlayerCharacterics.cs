using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacterics : MonoBehaviour 
{
   [SerializeField] private Animator _animator;
   [SerializeField] private Canvas _canvas;
   [SerializeField] private TextMeshProUGUI _text;
   private Camera _camera;
   public static PlayerCharacterics singleton;
   private float _maxHealts,_currentHealts, _damage, _crtChance, _shield,_energy ;
   private float _time, _value;
   private Dictionary<CharacterType, float> _dictionary = new Dictionary<CharacterType, float>();

   [Header("Indicator")]
   [SerializeField] private Image _imageHealts, _imageEnergy;
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
      _energy = 0f;
      
      _currentHealts = _maxHealts;
      _dictionary.Add(CharacterType.Damage,_damage); _dictionary.Add(CharacterType.CritChance,_crtChance);
      _dictionary.Add(CharacterType.Healts,_maxHealts); _dictionary.Add(CharacterType.Shield,_shield);
   }

   private void Start()
   {
      _playerEvent.AddListener(dmg => ApplyDamage(dmg));
      _playerEvent.AddListener(arg0 => HealtsAndEnergy());
      textStats();
   }
      
   public void tempBonus(CharacterType stat,float value,float time)
   {
      StartCoroutine(bonus(stat, value , time));
      
   }
   public void BonusItem (CharacterType stat, float value)
   {
      _dictionary[stat] += value;
   }

   public void textStats()
   {
      _textStats.text =
         String.Format("My Statistics \nHealts - {0} \nDamage - {1} \nShield - {2} \nCrit.Chance - {3} ",
            _dictionary[CharacterType.Healts],_dictionary[CharacterType.Damage],_dictionary[CharacterType.Shield],_dictionary[CharacterType.CritChance]);
   }

   private void HealtsAndEnergy()
   {
      _imageHealts.fillAmount = _currentHealts / _maxHealts;
      _imageEnergy.fillAmount = _energy / 100;
   }

   public float GetEnergy(float energy)
   {
      _energy += energy;
      _energy = Mathf.Clamp(_energy, 0, 100);
      HealtsAndEnergy();
      return _energy;
   }

   IEnumerator bonus (CharacterType stat ,float value, float time)
   {
      float tempValue = _dictionary[stat];
     _dictionary[stat] = _dictionary[stat] + value;
      yield return new WaitForSeconds(time);
     _dictionary[stat] = tempValue;
     
   }
   
   public float GetValue(CharacterType stat)
   {
      return _dictionary[stat];
   }

   public void Dead()
   {
      
   }
   
   private void ApplyDamage(float damage)
   {
      float _dmg = damage * (1 - _dictionary[CharacterType.Shield] / 1000);
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





