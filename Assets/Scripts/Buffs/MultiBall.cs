using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiBall : Buff
{
    protected override void ApplyBuffEffect()
    {
        foreach(Ball ball in BallsManager.Instance.balls.ToList())
        {
            BallsManager.Instance.SpawnBalls(ball.gameObject.transform, 2);
        }
    }
}
