using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private SpriteRenderer sr;
    private float paddlePosY;
    private float mousePosPixels;
    private float mousePosX;
    private float defaultPaddleWithInPixels = 192;
    private float defaultClamp = 128;

    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        sr = GetComponent<SpriteRenderer>();
        paddlePosY = transform.position.y;
    }


    void Update()
    {
        if (!mainCamera || !sr) return;

        movePaddle();
    }

    private void movePaddle()
    {
        float paddleShift = (defaultPaddleWithInPixels - ((defaultPaddleWithInPixels / 2) * sr.size.x)) / 2f;
        float leftClamp = defaultClamp - paddleShift;
        float rightClamp = 540 - defaultClamp + paddleShift;
        mousePosPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        mousePosX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixels, 0, 0)).x;
        transform.position = new Vector3(mousePosX, paddlePosY, 0);
    }
}
