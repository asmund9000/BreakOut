using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    private float _ballSpeed;
    private float _speedMultyplier = 1;
    private Vector2 _moveDirection = Vector3.up;
    private Transform _myTransform;

    private event BallDestroyEventHandler onBallDestroy;

    // Use this for initialization
    void Start () {
        _myTransform = transform;
        _ballSpeed = 20;
       

        onBallDestroy += GameMaster.instance.BallsDecrement;
    }

    public void SetSpeedMultyplier(float speedMultyplier)
    {
        this._speedMultyplier = speedMultyplier;
    }
	
    // Update is called once per frame
	void Update () {
     if(GameMaster.instance.gameStarted)   _myTransform.Translate(_moveDirection * Time.deltaTime * _ballSpeed * _speedMultyplier);
        
	}

    void OnCollisionEnter2D(Collision2D collision)
    {

            // Debug.Log("Collision");
        ContactPoint2D contact = collision.contacts[0];
        float f = 0f;


        if (collision.gameObject.CompareTag("Platform"))
        {
            //Координата центра
            float centerX = collision.collider.bounds.center.x;
            //Координата левого края
            float leftX = collision.collider.bounds.center.x - collision.collider.bounds.size.x / 2;
            //Координата правого края
            float rightX = collision.collider.bounds.center.x + collision.collider.bounds.size.x / 2;


            if (contact.point.x > centerX)
            {
                f = -(1 - (rightX - contact.point.x) / (rightX - centerX));
            }
            if (contact.point.x < centerX)
            {

                f = 1 - (contact.point.x - leftX) / (centerX - leftX);
            }

            //  

        }
        else if (collision.gameObject.CompareTag("ballDestroyer"))
        {
            Destroy(gameObject);
        }



         SetMoveDirection(Vector3.Reflect(_moveDirection, Quaternion.Euler(0, 0, 30 * f) * contact.normal));

       // _moveDirection = Vector3.Reflect(_moveDirection, Quaternion.Euler(0, 0, 30 * f) * contact.normal);


    }

    void OnDestroy()
    {
        onBallDestroy(this);
    }

    public float GetSpeedMultyplier()
    {
        return _speedMultyplier;
    }

    public void SetMoveDirection(Vector3 moveDirection)
    {
        this._moveDirection = moveDirection;
    }

    public Vector3 GetMoveDirection()
    {
        return _moveDirection;
    }


}
