using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public float MoveSpeed;
    public int CurHP;
    public int MaxHP;
    public int Damage;

    private Vector2 _facingDirection;

    [Header("Combat")]
    public KeyCode AttackKey;
    public float AttackRange;
    public float AttackRate;
    public float LastAttackTime;

    [Header("Sprites")]
    public Sprite DownSprite;
    public Sprite UpSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    private Rigidbody2D _rig;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKeyDown(AttackKey))
        {
            if ((Time.time - LastAttackTime) >= AttackRate)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        LastAttackTime = Time.time;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, AttackRange, 1 << 8);

        if(hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(Damage);
        }
    }

    private void Move()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector2 vel = new Vector2(xAxis, yAxis);

        if (vel.magnitude != 0)
            _facingDirection = vel;

        UpdateSpriteDirection();

        _rig.velocity = vel * MoveSpeed;
    }

    private void UpdateSpriteDirection()
    {
        if (_facingDirection == Vector2.up)
            _spriteRenderer.sprite = UpSprite;
        else if (_facingDirection == Vector2.down)
            _spriteRenderer.sprite = DownSprite;
        else if (_facingDirection == Vector2.left)
            _spriteRenderer.sprite = LeftSprite;
        else if (_facingDirection == Vector2.right)
            _spriteRenderer.sprite = RightSprite;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
