using System.Linq;

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
