using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour {

    public BrickTypes brickType;
    private IBrick _brick;
    private Transform _myTransform;

    public void Init(IBrick brick)
    {
        this._myTransform = transform;
        this._brick = brick;
      //  brick = new SimpleBreak(this);
      //   Debug.Log("!!!!!!!!!!!!!!!!");
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("ball"))
        {
            _brick.TakeDamage();
        }
    }
        
    public void BrickDestroy()
    {
        Destroy(gameObject);
    }

    public Transform GetTransform()
    {
        return _myTransform;
    }
}





