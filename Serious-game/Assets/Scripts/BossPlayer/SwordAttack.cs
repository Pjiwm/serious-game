using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordAttack : MonoBehaviour
{
    [SerializeField] private float knockBackForce = 3f;
    [SerializeField] private Collider2D swordCollider;
    [SerializeField] private float damage = 3;
    
    private Vector2 _rightAttackOffset;
    private const string EnemyTag = "Enemy";
    private void Start()
    {
        _rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {
        swordCollider.enabled = true;
        transform.localPosition= _rightAttackOffset;
    }

    public void AttackLeft()
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(_rightAttackOffset.x * -1, _rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnemyTag))
        { 
            var enemy = other.GetComponent<EnemyController>();

            if (enemy == null) return;
            enemy.OnHit(damage);
            enemy.OnKnockBack(KnockBack(other));
        }
    }

    private Vector2 KnockBack(Collider2D other)
    {
        var positionPlayer = gameObject.GetComponentInParent<Transform>().position;
        var direction = (Vector2)(other.gameObject.transform.position - positionPlayer).normalized;
        var knockback = direction * knockBackForce;
        return knockback;
    }
}
