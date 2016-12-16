using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BrickTypes { Easy, Medium, Hard };
public enum BonusTypes { SpeedBoost, IncreasePlatform, CloneBall };

public delegate void BrickDestroyEventHandler(IBrick brick);
public delegate void TakeBonusEventHandler(BonusTypes bonusType);
public delegate void RemoveBonusEventHandler(BonusTypes bonusType);
public delegate void OnUpdateEventHandler(BonusTypes bonusType);

public class GameMaster : MonoBehaviour  {

    public static GameMaster instance;
    public Text scoreLabel;
    public GameObject[] bonusesPrefs;
    public int BallsCount { get; set; }
    public int BricksCount { get; set; }

    public List<BallController> balls;

    public bool speedBoostActive;

    public int CurrentScoreCount {
        get { return _currentScoreCount ; }
        set
        {
            _currentScoreCount = value;
            scoreLabel.text = _currentScoreCount.ToString();
        }
    }

    private int _currentScoreCount;
    private float bonusChanse;

    // public delegate void MethodContainer();


    private void Awake()
    {
       instance = this;
    }


    int GetRewardForBrick(BrickTypes brickType)
    {
        int reward = 0;
        switch (brickType)
        {
            case BrickTypes.Easy:
                reward = 10;
                break;
            case BrickTypes.Medium:
                reward = 25;
                break;
            case BrickTypes.Hard:
                reward = 50;
                break;
            default:
                break;
        }
        return reward;
    }

    public void BallsDecrement()
    {
        BallsCount--;
        if (BallsCount == 0)
        {
            Lose();
        }
    }

    public void BricksDecrement(IBrick brick)
    {
        BricksCount--;
        CurrentScoreCount = CurrentScoreCount + GetRewardForBrick(brick.GetBrickType());
        if (BricksCount == 0)
        {
            Win();
        }

        CreateBonusObject(brick);
    }

    public void CreateBonusObject(IBrick brick)
    {
        if (Random.Range(0, 100) < bonusChanse) Instantiate(bonusesPrefs[Random.Range(0, bonusesPrefs.Length)], brick.GetBrickPosition(), Quaternion.identity);

    }
    void Start()
    {
        // SetBonus(BonusTypes.SpeedBoost);
        bonusChanse = 15;
       StartCoroutine( LevelsReader.JsonReader());
    }

    public void SetBonus(BonusTypes bonusType)
    {
        switch (bonusType)
        {
            case BonusTypes.SpeedBoost:

                SpeedBoost speedBoost = GameObject.FindObjectOfType<SpeedBoost>();
                if (speedBoost == null)
                {
                    GameObject go = new GameObject();
                    go.name = "SpeedBoost";
                    speedBoost = go.AddComponent<SpeedBoost>();
                }

                speedBoost.ApplyBonus();
                break;
            case BonusTypes.IncreasePlatform:
                break;
            case BonusTypes.CloneBall:
                CloneBall cloneBall = GameObject.FindObjectOfType<CloneBall>();
                if (cloneBall == null)
                {
                    GameObject go = new GameObject();
                    go.name = "CloneBall";
                    cloneBall = go.AddComponent<CloneBall>();
                }

                cloneBall.ApplyBonus();
                break;
            default:
                break;
        }
    }
    //public void RemoveBonus(BonusTypes bonusType)
    //{
    //    switch (bonusType)
    //    {
    //        case BonusTypes.SpeedBoost:
    //            //speedBoostActive = false;
    //            //foreach (BallController ball in balls)
    //            //{
    //            //    ball.SetSpeedMultyplier(1);
    //            //}
    //            break;
    //        case BonusTypes.IncreasePlatform:
    //            break;
    //        case BonusTypes.CloneBall:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    public void PrintMessage(string s)
    {
        Debug.Log(s);
    }



    void Win()
    {
      //  Debug.Log("Win");
    }

    void Lose()
    {
       // Debug.Log("Lose");
    }

    class BonusController : MonoBehaviour
    {

    }
}




