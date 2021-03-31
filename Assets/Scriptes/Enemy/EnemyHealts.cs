using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealts : MonoBehaviour
{
    private Enemy _myChar;
    private float _currentHP;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Canvas _canvas;
    [HideInInspector] public HitEvent _hitEvent;
    [HideInInspector] public UnityEvent _DeathEvent;
    [SerializeField] private Animator _animator;
    private float _maxHP;

    private void Awake()
    {
        _myChar = gameObject.GetComponent<EnemySystem>().getChar();
        _currentHP = _myChar.maxHealts;
        _canvas.worldCamera = Camera.main;
    }

    private void Start()
    {
       _hitEvent.AddListener(arg0 => ReceiveDamage(arg0));
       
       _DeathEvent.AddListener(() => gameObject.GetComponent<EnemySystem>().enabled = false);
       _DeathEvent.AddListener(() => gameObject.GetComponent<EnemyAttack>().enabled = false);
       _DeathEvent.AddListener(() => gameObject.GetComponent<EnemyHealts>().enabled = false);
       _DeathEvent.AddListener(() => gameObject.GetComponent<Collider>().enabled = false);
       _DeathEvent.AddListener(() => gameObject.GetComponent<Animator>().SetTrigger(nameAnimator.Dead));
    }

    

    public void ReceiveDamage(float damage)
    {
        _animator.Play("GetHit");
       _currentHP -= damage;
       _currentHP = Mathf.Clamp(_currentHP, 0, _myChar.maxHealts);
       _myChar.setHP(_currentHP);
       TextDamage(damage);
       if (_currentHP <= 0)
       {
           _DeathEvent.Invoke();
           
       }
    }

    public float GetCurrentHP() { return _currentHP;}


    public void notActive()
    {
        var bag = List_ItemObject.Instation.respawnBags();
        if (bag != null)
        {
            bag.transform.position = transform.position + new Vector3(0, 1, 0);
            bag.SetActive(true);
            
        }
        gameObject.SetActive(false);
    }
    

    public void TextDamage(float damage)
    {
        _canvas.transform.LookAt(Camera.main.transform);
        _text.CrossFadeAlpha(10f,0.1f,false);
        _text.text = damage.ToString();
        _text.CrossFadeAlpha(0f,1f,false);
    }

}
[System.Serializable]
public class HitEvent : UnityEvent <float> {}

