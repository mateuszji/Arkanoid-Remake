using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private float paddlePosY;
    private float mousePosPixels;
    private float mousePosX;
    private float leftClamp = 125;
    private float rightClamp = 415;

    void Start()
    {
        if (!mainCamera) mainCamera = FindObjectOfType<Camera>();
        paddlePosY = this.transform.position.y;
    }

    void Update()
    {
        movePaddle();
    }

    private void movePaddle()
    {
        mousePosPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        mousePosX = mainCamera.ScreenToWorldPoint(new Vector3(mousePosPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePosX, paddlePosY, 0);
    }
}
