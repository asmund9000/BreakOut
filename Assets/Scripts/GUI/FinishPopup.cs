using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
[RequireComponent(typeof(PopupPanel))]
public class FinishPopup : MonoBehaviour {

	[Header("Labels")]
	public Text scoreLabel;
    public Text loseLabel;

    [Header("Buttons")]
	public Button menuButton;
	public Button nextLevelButton;
	public Button replayButton;

	[HideInInspector]
	public PopupPanel panel;
	
	void Awake () {
		panel = GetComponent<PopupPanel> ();

        // нажатие кнопки меню обрабатывается в Game:Start
        nextLevelButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            bool nextAvaliable = LevelManager.instance.NextLevel();
            string nextScene;
            if (nextAvaliable)
                nextScene = "Game";
            else
                nextScene = "Levels";

            SceneManager.LoadScene(nextScene);
        });

        menuButton.onClick.AddListener(() => ToMenu());
        replayButton.onClick.AddListener(() => ReloadScene());

    }
    void ToMenu()
    {
        SceneManager.LoadScene("Levels");
    }

    void ReloadScene()
    {
        SceneManager.LoadScene("Game");
    }


    public void Configure(bool success) {
		nextLevelButton.gameObject.SetActive(success);
        scoreLabel.enabled = success;
        scoreLabel.text = "Score: " + GameMaster.instance.CurrentScoreCount;
        loseLabel.enabled = !success;
	}
}
