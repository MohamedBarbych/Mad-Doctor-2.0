﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform LookAt;
    public float boundX = 4.0f;
    public float boundY = 2.0f;
    private Camera m_OrthographicCamera;

    private void Start()
    { m_OrthographicCamera = GetComponent<Camera>(); }

    private void LateUpdate()
    {
        //zoom based on mouse scroll
        float cameraScroll = Input.GetAxisRaw("MouseScrollWheel");
        if (cameraScroll > 0)
            m_OrthographicCamera.orthographicSize -= 0.5f;
        if (cameraScroll < 0)
            m_OrthographicCamera.orthographicSize += 0.5f;
        Vector3 delta = Vector3.zero;

        //follow player
        float dx = LookAt.position.x - transform.position.x;
        if (dx > boundX || dx < -boundX)
        {
            if (transform.position.x < LookAt.position.x)
            { delta.x = dx - boundX; }
            else
            { delta.x = dx + boundX; }
        }
        float dy = LookAt.position.y - transform.position.y;
        if (dy > boundY || dy < -boundY)
        {
            if (transform.position.y < LookAt.position.y)
            { delta.y = dy - boundY; }
            else
            { delta.y = dy + boundY; }
        }
        transform.position = transform.position + delta;
    }
}
