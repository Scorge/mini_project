using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    Rigidbody rb;

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

    private Vector2 lookDirection;
    private float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ProjectileSpeed = 30.0f;
    }

    // Update is called once per frame
    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        Gun.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        float DiffX = Input.GetAxis("Horizontal");
        float DiffY = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(DiffX, DiffY, 0.0f) * CharacterSpeed;

       if (Input.GetMouseButtonDown(0))
            FireProjectile();
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(Projectile, FirePos.position, FirePos.rotation);
        firedProjectile.GetComponent<Rigidbody>().velocity = FirePos.right * ProjectileSpeed;
    }
}
