using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SpeedBoost : ITimerBonus
{
    private BonusTypes bonusType;
    private float bonusDuration;

    public SpeedBoost()
    {
        GameMaster.instance.PrintMessage("Запустили таймер");
        bonusDuration = 30000f;
        bonusType = BonusTypes.SpeedBoost;
        ApplyBonus();

    }
    public void ApplyBonus()
    {
        Timer timer = new Timer(bonusDuration);
        timer.Elapsed += RemoveBonus;
        timer.Start();
    }

    public void RemoveBonus(object sender, ElapsedEventArgs e)
    {
        GameMaster.instance.PrintMessage("Таймер кончился");
        GameMaster.instance.RemoveBonus(bonusType);
    }
}
