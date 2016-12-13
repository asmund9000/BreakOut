using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SpeedBoost : MonoBehaviour, ITimerBonus
{
    private static SpeedBoost instance;
    private BonusTypes bonusType;
    private float bonusDuration = 30f;
    private float timeLeft;
    Timer timer;


    public void Update()
    {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            RemoveBonus();
            Destroy(gameObject);
        }
    }


    public void ApplyBonus()
    {
        GameMaster.instance.PrintMessage("Запустили таймер");
        timeLeft = timeLeft + bonusDuration;
        bonusType = BonusTypes.SpeedBoost;
        timeLeft = bonusDuration;

        foreach (BallController ball in GameMaster.instance.balls)
        {
            // GameObject speedBonus = Instantiate(new GameObject());
            ball.SetSpeedMultyplier(1.4f);
        }
    }

    public void RemoveBonus()
    {
        GameMaster.instance.PrintMessage("Таймер кончился");
        foreach (BallController ball in GameMaster.instance.balls)
        {
            ball.SetSpeedMultyplier(1);
        }
       // Destroy(gameObject);
    }

}
