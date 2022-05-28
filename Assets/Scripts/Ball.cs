using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> onBallDestroy;
    public void DestroyBall()
    {
        onBallDestroy?.Invoke(this);
        Destroy(this.gameObject);
    }
}
