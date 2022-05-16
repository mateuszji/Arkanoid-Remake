using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton

    private static Paddle _instance;
    public static Paddle Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag =="Ball")
        {
            Rigidbody2D ballRb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hit = other.contacts[0].point;
            Vector3 paddleCenter = new Vector3(transform.position.x, transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hit.x;

            if (hit.x < paddleCenter.x)
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), BallsManager.Instance.initBallSpeed));
            else
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), BallsManager.Instance.initBallSpeed));
        }
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
