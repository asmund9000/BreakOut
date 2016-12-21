using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    private float _platformSpeed;
    private Transform _myTransform;
    private Vector2 _screenPoint;
    private float _platformBoundSize;
    private Vector2 _curScreenPoint;
    private Vector2 _curPosition;
    private float _leftBorder;
    private float _rightBorder;
    private Camera _mainCamera;
    private Transform _cameraTransform;

    // Use this for initialization
    void Start() {
        _myTransform = transform;
        _mainCamera = Camera.main;
        _cameraTransform = _mainCamera.transform;
        _platformBoundSize = GetComponent<SpriteRenderer>().bounds.size.x;
        _platformSpeed = 4;
    }

    // Update is called once per frame
    void Update() {
     
        //  float pos = _myTransform.position.x + (Input.GetAxis("Horizontal") * _platformSpeed * Time.deltaTime);
        //  _myTransform.position = new Vector2(pos, _myTransform.position.y);
    }

    void OnMouseDown()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(_myTransform.position);



        //  Vector2 offset = _myTransform.position - Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

    }


    void OnMouseDrag()
    {
       
        _curScreenPoint = new Vector2(Input.mousePosition.x, _screenPoint.y);


        _curPosition = _mainCamera.ScreenToWorldPoint(_curScreenPoint);

         _leftBorder = _cameraTransform.position.x - _mainCamera.orthographicSize * _mainCamera.aspect;
         _rightBorder = _cameraTransform.position.x + _mainCamera.orthographicSize * _mainCamera.aspect;

        //if (curPosition.x > leftBorder + _platformBoundSize / 2 && curPosition.x < rightBorder - _platformBoundSize / 2)
        //{
        //    _myTransform.position = curPosition;
        //}

        
        float posX = Mathf.Clamp(_curPosition.x, _leftBorder + _platformBoundSize * _myTransform.localScale.x / 2, _rightBorder - _platformBoundSize * _myTransform.localScale.x / 2);
        _myTransform.position = new Vector3(posX, _curPosition.y);



    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("SpeedBoost"))
        {
            GameMaster.instance.SetBonus(BonusTypes.SpeedBoost);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("IncreasePlatform"))
        {
            Debug.Log("Increase");
            GameMaster.instance.SetBonus(BonusTypes.IncreasePlatform);
            Destroy(coll.gameObject);
        }

        if (coll.gameObject.CompareTag("CloneBall"))
        {
            Debug.Log("CLONE");
            GameMaster.instance.SetBonus(BonusTypes.CloneBall);
            Destroy(coll.gameObject);
        }
    }
}
