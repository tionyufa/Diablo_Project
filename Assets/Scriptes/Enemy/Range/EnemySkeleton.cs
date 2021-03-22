using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySkeleton : EnemySystem
{
  

    public override void Attack()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(Player.singleton.transform.position);
        
        if (!isAction)
        {
            isAction = true;
            Invoke("AttackSpider", 1f);
        } 
    }

    private void AttackSpider()
    {
        
        _animator.SetTrigger(nameAnimator.HashAttack);
        isAction = false;
    }

    }



