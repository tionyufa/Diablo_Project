using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fxAttack;
    [SerializeField] private ParticleSystem _fxAttackPassive;
    [SerializeField] private ParticleSystem _fxActiveSkill;

   
    public void FXAttack()
    {
        _fxAttack.Play();
    }
    public void FxAttackPassive()
    {
        _fxAttackPassive.Play(); 
    }
    public void FxActiveSkill()
    {
        _fxActiveSkill.Play(); 
    }
}