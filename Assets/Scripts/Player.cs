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
    public float InteractRange;
    public List<string> Inventory = new List<string>();

    private Vector2 _facingDirection;

    [Header("Combat")]
    public KeyCode AttackKey;
    public float AttackRange;
    public float AttackRate;
    public float LastAttackTime;

    [Header("Experience")]
    public int CurLevel;
    public int CurXp;
    public int XpToNextLevel;
    public float LevelXpModifier;

    [Header("Sprites")]
    public Sprite DownSprite;
    public Sprite UpSprite;
    public Sprite LeftSprite;
    public Sprite RightSprite;

    private Rigidbody2D _rig;
    private SpriteRenderer _spriteRenderer;
    private ParticleSystem _hitEffect;
    private PlayerUI _ui;

    private void Awake()
    {
        _rig = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _hitEffect = gameObject.GetComponentInChildren<ParticleSystem>();
        _ui = FindObjectOfType<PlayerUI>();
    }

    private void Start()
    {
        _ui.UpdateHealthBar();
        _ui.UpdateXPBar();
        _ui.UpdateLevelText();
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

        CheckInteract();
    }

    private void CheckInteract()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, InteractRange, 1 << 9);

        if (hit.collider != null)
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            _ui.SetInteractText(hit.collider.transform.position, interactable.InteractDescription);

            if (Input.GetKeyDown(AttackKey))
            {
                interactable.Interact();
            }
        }else
        {
            _ui.DisableInteractText();
        }
    }

    private void Attack()
    {
        LastAttackTime = Time.time;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _facingDirection, AttackRange, 1 << 8);

        if(hit.collider != null)
        {
            hit.collider.GetComponent<Enemy>().TakeDamage(Damage);

            _hitEffect.transform.position = hit.collider.transform.position;
            _hitEffect.Play();
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

        _ui.UpdateHealthBar();

        if (CurHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void AddXp(int xp)
    {
        CurXp += xp;
        _ui.UpdateXPBar();

        if (CurXp >= XpToNextLevel)
            LevelUp();
    }

    private void LevelUp()
    {
        CurXp -= XpToNextLevel;
        CurLevel++;
        XpToNextLevel = Mathf.RoundToInt(XpToNextLevel * LevelXpModifier);

        _ui.UpdateLevelText();
        _ui.UpdateXPBar();
    }

    public void AddItemToInventory(string item)
    {
        Inventory.Add(item);
        _ui.UpdateInventoryText();
    }
}
