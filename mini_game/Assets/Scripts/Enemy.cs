using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float AttackRange;

    [SerializeField]
    private float ChaseRange;

    [SerializeField]
    private float Speed;

    [SerializeField]
    private int MaxHP;

    private int CurrentHP;

    [SerializeField]
    private float HitDelay;

    [SerializeField]
    private Sprite NormalSprite;

    [SerializeField]
    private Sprite HitSprite;

    private SpriteRenderer sprite;

    private GameObject Target;
    private GameObject Player;
    private GameObject Base;

    private Rigidbody rigid;

    private enum State
    {
        Normal,
        Hit,
        Dead
    };

    private State state;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Base = GameObject.FindWithTag("Base");
        state = State.Normal;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = NormalSprite;
        rigid = GetComponent<Rigidbody>();
        CurrentHP = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                GetTarget();
                ChaseTarget();
                break;
            case State.Hit:
                break;
            case State.Dead:
                Destroy(gameObject);
                break;
        }
    }

    void UpdateSprite()
    {
        switch(state)
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

    void GetTarget()
    {
        if (Vector3.Distance(Player.transform.position, this.transform.position) < ChaseRange)
        {
            Target = Player;
        }
        else
        {
            Target = Base;
        }
    }

    void ChaseTarget()
    {
        Vector3 NewSpeed = (Target.transform.position - this.transform.position).normalized * Speed;
        NewSpeed.z = 0.0f;
        rigid.velocity = NewSpeed;
        Debug.Log(NewSpeed);
    }

    public void Hit(int damage, Vector3 force)
    {
        rigid.velocity = Vector3.zero;
        state = State.Hit;
        CurrentHP = CurrentHP - damage;
        Debug.Log(force);
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
