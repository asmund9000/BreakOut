using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

    public IBrick brick;

    void Start()
    {
        brick = new SimpleBreak(this);
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("ball"))
        {
            brick.TakeDamage();
        }
    }
        
    public void BrickDestroy()
    {
        Destroy(gameObject);
    }
}





