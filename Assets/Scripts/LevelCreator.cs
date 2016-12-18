using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour {

    public static LevelCreator instance;
    public BrickController[] brickPrefs;
    private float _fieldSize;
    private float _gridSize;
    private float _allBricksSize;
    private float _scale;
    private float _gridSizeX;
    private float _brickSizeX;
    private float _brickSizeY;

    void Awake()
    {
        instance = this;
    }
	// Use this for initialization
	void Start () {
        _fieldSize = Camera.main.orthographicSize * Camera.main.aspect;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CreateBrickObject(int type_index, int columns, int rows, int col_index, int row_index)
    {

        //float aspect = 800f / 1280f;
        _gridSize = _fieldSize / columns;
        _allBricksSize = brickPrefs[0].GetComponent<SpriteRenderer>().bounds.size.x * columns;
        _scale = _fieldSize * 2 / _allBricksSize;
        _gridSizeX = _gridSizeX * _scale;

        _brickSizeX = brickPrefs[0].GetComponent<SpriteRenderer>().bounds.size.x * _scale;
        _brickSizeY = brickPrefs[0].GetComponent<SpriteRenderer>().bounds.size.y * _scale;

        if (type_index > 0)
        {
            BrickController brickController = Instantiate(brickPrefs[type_index - 1]);
            GameMaster.instance.BricksCount++;
            brickController.transform.localScale = new Vector2(_scale, _scale);
            brickController.transform.position = new Vector2(-_fieldSize + _brickSizeX * col_index + _brickSizeX / 2, _fieldSize / Camera.main.aspect - _brickSizeY * row_index - _brickSizeY / 2);
            brickController.Init(new SimpleBrick(brickController));
        }
    }
}
