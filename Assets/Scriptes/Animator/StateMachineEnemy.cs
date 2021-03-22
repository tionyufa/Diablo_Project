using UnityEngine;

public class StateMachineEnemy : StateMachineBehaviour
{
    
    private bool _action;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var s = animator.GetComponent<EnemyGolem>();
        s.isHitAnimator(_action);
        _action = true;
       
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _action = false;
       
    }

   

    
}
