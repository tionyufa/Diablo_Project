using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGolem : EnemySystem
{
    public override void Attack()
    {
        _agent.SetDestination(transform.position);
        
        transform.LookAt(Player.singleton.transform.position);

        if (!isAction)
        {
            isAction = true;
            Invoke("AttackGolem", 1f);
        } 
       
    }

    private void AttackGolem()
    {
        _animator.SetTrigger(nameAnimator.HashAttack);
        isAction = false;
    }
    

    public void isHitAnimator(bool action)
    {
        isAction = action;
    }
}
