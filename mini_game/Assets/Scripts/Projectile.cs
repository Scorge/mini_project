using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
        if(other.tag == "Wall")
            Destroy(gameObject);
    }
}
