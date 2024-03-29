﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public Transform player;

    public float XMax;
    public float XMin;
    public float YMax;
    public float YMin;

    float cameraXMin, cameraXMax, cameraYMin, cameraYMax;

    // 플레이어와 커서 사이 어느정도 위치에 카메라가 놓일지
    private float Ratio;

    // Start is called before the first frame update
    void Start()
    {
        SetLimit();
    }

    void SetLimit()
    {
        Camera cam = Camera.main;

        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        cameraXMin = XMin + width / 2;
        cameraXMax = XMax - width / 2;

        cameraYMin = YMin + height / 2;
        cameraYMax = YMax - height / 2;
    }

    // Update is called once per frame
    void Update()
    {
        float MinRatio = 3.0f;
        Vector3 Cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Diff = Cursor - transform.position;
        Ratio = Mathf.Max( Mathf.Sqrt(Diff.x * Diff.x + Diff.y * Diff.y), MinRatio);
        transform.position = new Vector3(Mathf.Clamp((player.position.x* Ratio + Cursor.x)/(Ratio+1.0f), cameraXMin, cameraXMax),
            Mathf.Clamp((player.position.y* Ratio + Cursor.y)/(Ratio + 1.0f), cameraYMin, cameraYMax), -10);
    }
}
