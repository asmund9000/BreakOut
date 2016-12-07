using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    private float _platformSpeed;
    private Transform _myTransform;
    private Vector2 _screenPoint;
    private float _platformBoundSize;

    // Use this for initialization
    void Start() {
        _myTransform = transform;
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
       
        Vector2 curScreenPoint = new Vector2(Input.mousePosition.x, _screenPoint.y);


        Vector2 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);

        float leftBorder = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        float rightBorder = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        //if (curPosition.x > leftBorder + _platformBoundSize / 2 && curPosition.x < rightBorder - _platformBoundSize / 2)
        //{
        //    _myTransform.position = curPosition;
        //}

        
        float posX = Mathf.Clamp(curPosition.x, leftBorder + _platformBoundSize / 2, rightBorder - _platformBoundSize / 2);
        _myTransform.position = new Vector3(posX, curPosition.y);
;


    }
}
