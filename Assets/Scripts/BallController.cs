using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {


    private float _ballSpeed;
    private Vector2 _moveDirection;
    private Transform _myTransform;

    public delegate void BallDestroyEventHandler();

    public event BallDestroyEventHandler onBallDestroy;

    // Use this for initialization
    void Start () {
        _myTransform = transform;
        _ballSpeed = 20;
        _moveDirection = Vector3.up;

        onBallDestroy += GameMaster.instance.BallsDecrement;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(_moveDirection * Time.deltaTime * _ballSpeed);
        
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

            
        }


        _moveDirection = Vector3.Reflect(_moveDirection, Quaternion.Euler(0, 0, 30 * f) * contact.normal);



    }


}
