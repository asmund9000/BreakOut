using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour {

    public Text levelIndexLabel;

    private Button button;
    private Image image;

    [SerializeField, HideInInspector]
    int _levelIndex = -1;

    private Level level;

    public int levelIndex
    {
        get
        {
            return _levelIndex; // int.Parse(gameObject.name.Substring(11)) - 1;
        }
        set
        {
            _levelIndex = value;
            if (levelIndexLabel)
                levelIndexLabel.text = (_levelIndex + 1).ToString();
        }
    }


    bool _available;
    Color tempColor;

    public bool available
    {
        get
        {
            return _available;
        }
        set
        {
            _available = value;

            button.interactable = _available;
            if (button.transition != Selectable.Transition.ColorTint)
            {
                tempColor = image.color;
                tempColor.a = _available ? 1f : 0.5f;
                image.color = tempColor;
            }
        }
    }

    // Use this for initialization
    void Start () {
        Init();

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Init()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        if (levelIndex < 0 || levelIndex >= LevelManager.instance.levelsTotal)
        {
            Debug.LogError("Incorrect level index: " + levelIndex);
            return;
        }

        // set OnClick
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            LevelManager.instance.currentLevel = levelIndex;
            SceneManager.LoadScene("Game");
        });
        // configure button using current level info
        level = LevelManager.instance.GetLevelPrefs(levelIndex);
        available = level.available;
    }
}
