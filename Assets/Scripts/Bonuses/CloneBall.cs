using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneBall : MonoBehaviour, ISimpleBonus
{
    public void ApplyBonus()
    {
        for (int i = 0; i < 5; i++)
        {
            BallController ball = Instantiate(GameMaster.instance.balls[0]) ;
            ball.SetSpeedMultyplier(GameMaster.instance.balls[0].GetSpeedMultyplier());
             ball.SetMoveDirection(Quaternion.Euler(0, 0, 360 * UnityEngine.Random.value) * ball.GetMoveDirection() );
            GameMaster.instance.balls.Add(ball);
        }
       
    }
}
