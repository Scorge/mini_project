using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private float CharacterSpeed;
    
    private float ProjectileSpeed;

    [SerializeField]
    private GameObject Cursor;

    [SerializeField]
    private GameObject Gun;

    [SerializeField]
    private Transform FirePos;

    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private Sprite NormalSprite;

    [SerializeField]
    private Sprite HitSprite;

    private SpriteRenderer sprite;

    private Rigidbody rigid;

    [SerializeField]
    private int MaxHP;

    private int CurrentHP;

    [SerializeField]
    private float HitDelay;


    private Vector2 lookDirection;
    private float lookAngle;

    private enum State
    {
        Normal,
        Hit,
        Attack,
        Dead
    };

    private State state;

    // Start is called before the first frame update
    void Start()
    {
        ProjectileSpeed = 30.0f;
        CurrentHP = MaxHP;
        state = State.Normal;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = NormalSprite;
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                NormalState();
                break;
            case State.Hit:
                break;
            case State.Dead:
                Destroy(gameObject);
                break;
        }
    }
    private void NormalState()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        float DiffX = Input.GetAxis("Horizontal");
        float DiffY = Input.GetAxis("Vertical");

        rigid.velocity = new Vector3(DiffX, DiffY, 0.0f) * CharacterSpeed;

        if (Input.GetMouseButtonDown(0))
            FireProjectile();
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(Projectile, FirePos.position, FirePos.rotation);
        firedProjectile.GetComponent<Rigidbody>().velocity = FirePos.right * ProjectileSpeed;
    }

    void UpdateSprite()
    {
        switch (state)
        {
            case State.Normal:
                sprite.sprite = NormalSprite;
                break;
            case State.Hit:
                sprite.sprite = HitSprite;
                break;
            case State.Dead:
                break;
        }
    }

    public void Hit(int damage, Vector3 force)
    {
        if (state == State.Hit)
            return;
        rigid.velocity = Vector3.zero;
        state = State.Hit;
        CurrentHP = CurrentHP - damage;
        Debug.Log("Player Hit");
        rigid.AddForce(force, ForceMode.Impulse);
        StartCoroutine(this.Hited());
    }

    IEnumerator Hited()
    {
        UpdateSprite();
        yield return new WaitForSeconds(HitDelay);
        rigid.velocity = Vector3.zero;
        if (CurrentHP <= 0)
        {
            state = State.Dead;
            UpdateSprite();
        }
        else
        {
            state = State.Normal;
            UpdateSprite();
        }
        yield break;
    }
}
