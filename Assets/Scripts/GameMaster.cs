using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster instance;
    //Подписать декремент на кничтожение кирпичика
    //ПОдписать декремент на уничтожение шарика
    public int BallsCount { get; set; }
    public int BricksCount { get; set; }

   // public delegate void MethodContainer();


    private void Awake()
    {
       instance = this;
    }

    void SetBonus()
    {

    }

    void RemoveBonus()
    {

    }

    public void BallsDecrement()
    {
        BallsCount--;
        if (BallsCount == 0)
        {
            Lose();
        }
    }

    public void BricksDecrement()
    {
        BallsCount--;
        if (BricksCount == 0)
        {
            Win();
        }
    }


    void Win()
    {
      //  Debug.Log("Win");
    }

    void Lose()
    {
       // Debug.Log("Lose");
    }

}
