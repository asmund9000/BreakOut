using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BrickTypes { Easy, Medium, Hard };
public enum BonusTypes { SpeedBoost, IncreasePlatform, CloneBall };

public delegate void BrickDestroyEventHandler(IBrick brick);
public delegate void BallDestroyEventHandler(BallController ball);

public class GameMaster : MonoBehaviour  {

    public static GameMaster instance;
    public Text scoreLabel;
    public FinishPopup finishPopup;
    public GameObject[] bonusesPrefs;
    public AudioSource brickHit;
    public AudioSource brickDestroy;

    public List<BallController> balls;


    public bool gameStarted;

    public int BricksCount {
        get
        {
            return _bricksCount;
        }
        set
        {
            _bricksCount = value;
            if (_bricksCount == 0)
            {
                Win();
            }
        }
    }

    public int BallsCount
    {
        get
        {
            return _ballsCount;
        }
        set
        {
            _ballsCount = value;
            if (_ballsCount == 0)
            {
                Lose();
            }
        }
    }

    public int CurrentScoreCount {
        get
        {
            return _currentScoreCount ;
        }
        set
        {
            _currentScoreCount = value;
            scoreLabel.text = _currentScoreCount.ToString();
        }
    }

    private int _ballsCount;
    private int _bricksCount;
    private int _currentScoreCount;
    private float bonusChanse;

    // public delegate void MethodContainer();


    private void Awake()
    {
      //  CreateBrickObject(6, 6);
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

    public void BallsDecrement(BallController ball)
    {
        BallsCount--;



        List<BallController> ballsToRemove = new List<BallController>();

        foreach (BallController _ball in balls)
        {
            if (_ball == ball)
            {
                ballsToRemove.Add(_ball);
            }
        }

        foreach (BallController _ball in ballsToRemove)
        {
            balls.Remove(_ball);
        }




        if (BallsCount == 0)
        {
            Debug.Log("!111111111111");
            Lose();
        }
    }

    public void BricksDecrement(IBrick brick)
    {
        BricksCount--;
        CurrentScoreCount = CurrentScoreCount + GetRewardForBrick(brick.GetBrickType());

        CreateBonusObject(brick);
    }

    public void CreateBonusObject(IBrick brick)
    {
        Vector3 pos = brick.GetBrickPosition();
        GameObject obj = bonusesPrefs[Random.Range(0, bonusesPrefs.Length)];

        if (Random.Range(0, 100) < bonusChanse)
        {
            Debug.Log(obj);
            Instantiate(obj, pos, Quaternion.identity);
        }
    }
    void Start()
    {
        // SetBonus(BonusTypes.SpeedBoost);
       bonusChanse = 5;
       BallsCount = 1;
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
        LevelManager.instance.CompleteLevel(0);
        finishPopup.panel.OpenPanel();
        finishPopup.Configure(true);
    }

    void Lose()
    {
        finishPopup.panel.OpenPanel();
        finishPopup.Configure(false);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Game");
    }
}




