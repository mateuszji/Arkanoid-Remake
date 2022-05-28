using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton

    private static BallsManager _instance;
    public static BallsManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
            Destroy(gameObject);
        else
            _instance = this;
    }

    #endregion

    [SerializeField]
    private Ball ballPrefab;
    private Ball initBall;
    private Rigidbody2D initBallRb;
    [HideInInspector]
    public float initBallSpeed = 300;
    public List<Ball> Balls { get; set; }

    private void Start()
    {
        InitBall();
    }

    private void Update()
    {
        if(!GameManager.Instance.isPlayingNow)
        {
            Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
            Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + 0.35f, 0);
            initBall.transform.position = ballPos;

            if(Input.GetMouseButton(0))
            {
                GameManager.Instance.isPlayingNow = true;
                initBallRb.isKinematic = false;
                initBallRb.AddForce(new Vector2(0, initBallSpeed));
            }
        }
    }

    private void InitBall()
    {
        Vector3 paddlePos = Paddle.Instance.gameObject.transform.position;
        Vector3 startPos = new Vector3(paddlePos.x, paddlePos.y + 0.35f, 0);
        initBall = Instantiate(ballPrefab, startPos, Quaternion.identity);
        initBallRb = initBall.GetComponent<Rigidbody2D>();
        initBall.name = "Default Ball";

        Balls = new List<Ball> { initBall };
    }

    public void ResetBalls()
    {
        foreach(var ball in Balls.ToList())
        {
            Destroy(ball.gameObject);
        }

        InitBall();
    }
}
