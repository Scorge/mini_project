using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int Damage;

    [SerializeField]
    private float Force;

    private Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Diff = Camera.main.transform.position - transform.position;
        if (Diff.x* Diff.x + Diff.y * Diff.y > 900.0f)
            Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall")
            Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            Vector3 force = (rigid.velocity).normalized * Force;
            enemy.Hit(Damage, force);
            Destroy(gameObject);
        }
    }
}
