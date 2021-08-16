using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public Transform[] points;

    public GameObject monsterObject;

    public float createTime;

    public int maxMonster = 100;

    public bool isGameOver = false;

    void Start()
    {
        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();
        print(points.Length);
        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }
    }

    IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);
                
                int idx = Random.Range(1, points.Length);
                Vector3 random = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
                Instantiate(monsterObject, points[idx].position + random, points[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
}
