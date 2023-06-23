using System;
using System.Collections;
using Inputs;
using PlayerAndMovement;
using UnityEngine;

namespace BossPlayer
{
    public class BossPlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput attackInput;
        private SwordAttack _swordAttack;
        [SerializeField] private Healthbar healthbar;
        [SerializeField] private float maxHealth = 100;
        [SerializeField] private AudioSource swingSound;
        [SerializeField] private AudioSource hitSound;
        private bool _canAttack = true;
    
        private Vector2 _movementInput;
        private Animator _animator;
        private PlayerController _playerController;
        private SpriteRenderer _spriteRenderer;
        private float _currentHealth;
        private Rigidbody2D _rb;
    
        private static readonly int Attack = Animator.StringToHash("SwordAttack");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int IsAlive = Animator.StringToHash("IsAlive");

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _playerController = GetComponent<PlayerController>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _swordAttack = GetComponentInChildren<SwordAttack>();
        
            healthbar.SetMaxHealth((int)maxHealth);
            _currentHealth = maxHealth;
        
            attackInput.OnAttack += OnAttack;
        }

        private void OnAttack(object sender, EventArgs eventArgs)
        {
            if (_canAttack){
                _animator.SetTrigger(Attack);
            }
        }
    
        public void SwordAttack()
        {
            if (!_canAttack) return;
            
            swingSound.Play();
            _playerController.DeActivatePlayerInputs();
            if (_spriteRenderer.flipX == true)
            {
                _swordAttack.AttackLeft();
            }
            else
            {
                _swordAttack.AttackRight();
            } 
            Cooldown();


        }
    
        public void EndSwordAttack()
        {
            _swordAttack.StopAttack();
            _playerController.ActivatePlayerInputs();
        }

        public void OnHit(float damage)
        {
            _animator.SetTrigger(Hit);
            hitSound.Play();
            _currentHealth -= damage;
            healthbar.SetHealth((int)_currentHealth);
        
            if (!(_currentHealth <= 0)) return;
        
            _animator.SetBool(IsAlive, false);
            healthbar.bar.gameObject.SetActive(false);
        }
    
        public void OnKnockBack(Vector2 direction)
        {
            _rb.AddForce(direction);
        }

        public void RemovePlayer()
        {
            Destroy(gameObject);
        }
    
        private void Cooldown()
        {
            _canAttack = false;
            StartCoroutine(CooldownCoroutine());
        }
    
        private IEnumerator CooldownCoroutine()
        {
            yield return new WaitForSeconds(1);
            _canAttack = true;
        }
    }
}
