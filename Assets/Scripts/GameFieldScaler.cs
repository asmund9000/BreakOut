using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldScaler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float sourceAspect = 800f / 1280f;
        float newAspect = Camera.main.aspect;

        transform.localScale = new Vector2(newAspect / sourceAspect, newAspect / sourceAspect);
       // float orthographicSize = Camera.main.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
