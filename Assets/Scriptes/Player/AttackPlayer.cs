using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
   [SerializeField] private Transform _weapon;

   [SerializeField] private float _radius, _distation;
   [SerializeField] private float _radiusPassive_1, _distationPassive_1 ,_radiusPassive_2, _distationPassive_2;
   private RaycastHit[] _hits;
   private int _index;
   private GameObject _target;

   public void SetTarget(GameObject target)
   {
      _target = target;
   }

   public void AttackDamage()
   {
      if (_target.GetComponent<EnemyHealts>() != null)
      {
         _target.GetComponent<EnemyHealts>()._hitEvent.Invoke(Damage());
         _target = null;

      }

   }

   public float Damage()
   {
      float random = Random.Range(0, 100);
      float dmg = PlayerCharacterics.singleton.GetValue(CharacterType.Damage);
      if (random <= PlayerCharacterics.singleton.GetValue(CharacterType.CritChance) / 10)
      {
         dmg *= 2;
         return dmg;
      }

      return dmg;

   }

//    public void AttackPassive_1()
//    {
//       Ray ray = new Ray(transform.position, transform.forward);
//       _hits = Physics.SphereCastAll(ray, _radiusPassive_1, _distationPassive_1);
//
//       for (int i = 0; i < _hits.Length; i++)
//       {
//          Collider col = _hits[i].collider;
//          Rigidbody rb = _hits[i].rigidbody;
//          if (col.GetComponent<EnemyHealts>() != null)
//          {
//             rb.AddRelativeForce(-ray.direction * 550f);
//             col.GetComponent<EnemyHealts>()._hitEvent.Invoke(Damage());
//          }
//       }
//    }
// }
//
   public void AttackPassive_2()
   {
      Ray ray = new Ray(_weapon.position,_weapon.forward);
     
      _hits = Physics.SphereCastAll(ray, _radiusPassive_2,_distationPassive_2);
      
      for (int i = 0; i < _hits.Length; i++)
      {
         Collider col = _hits[i].collider;
         if (col.GetComponent<EnemyHealts>() != null)
         {
           col.GetComponent<EnemyHealts>()._hitEvent.Invoke(Damage());
         }
      }
     
   }

   

   
}
