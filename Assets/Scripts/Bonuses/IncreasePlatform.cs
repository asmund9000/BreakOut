using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class IncreasePlatform : MonoBehaviour, ITimerBonus
{
    private static IncreasePlatform instance;
    private BonusTypes bonusType;
    private float bonusDuration = 30f;
    private float timeLeft;
    private bool _platformIncrease;
    private bool _platformDecrease;
    private Transform _platform;
    private Vector2 bonusScale = new Vector2(2f, 1);
    private float _speedIncrease = 2;
    Timer timer;

    void Start()
    {
        _platform = GameObject.FindGameObjectWithTag("Platform").transform;
    }

    public void Update()
    {
        if (_platformIncrease && _platform.localScale.x <= bonusScale.x)
        {
            Debug.Log("&&&");
            _platform.localScale = Vector2.Lerp(_platform.localScale, bonusScale, 1 * Time.deltaTime * _speedIncrease);
        }

        if (_platformDecrease && _platform.localScale.x > Vector2.one.x)
        {
            Debug.Log("!!!");
            _platform.localScale = Vector2.Lerp(_platform.localScale, Vector2.one, 1 * Time.deltaTime * _speedIncrease);
        }

        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            RemoveBonus();
           // Destroy(gameObject);
        }
    }


    public void ApplyBonus()
    {
        GameMaster.instance.PrintMessage("Запустили таймер");
        timeLeft = timeLeft + bonusDuration;
        bonusType = BonusTypes.IncreasePlatform;
        timeLeft = bonusDuration;
        _platformIncrease = true;
        _platformDecrease = false;

    }

    public void RemoveBonus()
    {
           _platformIncrease = false;
           _platformDecrease = true;
        GameMaster.instance.PrintMessage("Таймер кончился");

       // Destroy(gameObject);
    }

}
