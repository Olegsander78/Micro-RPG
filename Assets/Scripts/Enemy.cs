using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public float MoveSpeed;
    public int CurHP;
    public int MaxHP;
    public int XpToGive;

    [Header("Target")]
    public float ChaseRange;
    public float AttackRange;
    private Player _player;

    [Header("Attack")]
    public int Damage;
    public float AttackRate;
    private float _lastAttackTime;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();

        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, _player.transform.position);

        if (playerDistance <= AttackRange)
        {
            if (Time.time - _lastAttackTime >= AttackRate)
            {
                Attack();
            }
            _rigidbody2D.velocity = Vector2.zero;

        }
        else if (playerDistance <= ChaseRange)
        {
            Chase();
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void Chase()
    {
        Vector2 dir = (_player.transform.position - transform.position).normalized;

        _rigidbody2D.velocity = dir * MoveSpeed;
    }

    private void Attack()
    {
        _lastAttackTime = Time.time;
        _player.TakeDamage(Damage);
    }

    public void TakeDamage(int damageTaken)
    {
        CurHP -= damageTaken;

        if (CurHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _player.AddXp(XpToGive);

        Destroy(gameObject);
    }
}
