using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordAttack : MonoBehaviour
{
    public float knockBackForce = 3f;
    public Collider2D swordCollider;
    public float damage = 3;
    private Vector2 rightAttackOffset;

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackRight()
    {
        Debug.Log("Attacking right");
        swordCollider.enabled = true;
        transform.localPosition= rightAttackOffset;
    }

    public void AttackLeft()
    {
        Debug.Log("Attacking left");
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        { 
            EnemyController enemy = other.GetComponent<EnemyController>();
            
            if (enemy != null)
            {
                enemy.OnHit(damage);
                enemy.OnKnockBack(KnockBack(other));
            }
        }
    }

    private Vector2 KnockBack(Collider2D other)
    {
        Vector3 positionPlayer = gameObject.GetComponentInParent<Transform>().position;
        Vector2 direction = (Vector2)(other.gameObject.transform.position - positionPlayer).normalized;
        Vector2 knockback = direction * knockBackForce;
        return knockback;
    }
}
