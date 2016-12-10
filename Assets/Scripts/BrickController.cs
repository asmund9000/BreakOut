using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

    public int hp;
    public delegate void BrickDestroyEventHandler();

    public event BrickDestroyEventHandler onBrickDestroy;

    // Use this for initialization
    void Start () {
        onBrickDestroy += GameMaster.instance.BricksDecrement;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("ball"))
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        hp--;
        if (hp == 0)
        {
            BrickDestroy();
        }
    }

    void BrickDestroy()
    {
        onBrickDestroy();
        Destroy(gameObject);
    }
}
