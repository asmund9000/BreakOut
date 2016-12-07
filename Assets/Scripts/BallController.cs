using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    private float _ballSpeed;
    private Vector2 _moveDirection;
    private Transform _myTransform;

	// Use this for initialization
	void Start () {
        _myTransform = transform;
        _ballSpeed = 7;
        _moveDirection = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(_moveDirection * Time.deltaTime * _ballSpeed);
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("Collision");
        ContactPoint2D contact = collision.contacts[0];
        _moveDirection = Vector3.Reflect(_moveDirection, contact.normal);
    }


}
