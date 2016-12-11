using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(PopupPanel))]
public class FinishPopup : MonoBehaviour {

	[Header("Labels")]
	public Text winText;
	public Text loseText;

	[Header("Buttons")]
	public Button menuButton;
	public Button nextLevelButton;
	public Button replayButton;

	[HideInInspector]
	public PopupPanel panel;
	
	void Awake () {
		panel = GetComponent<PopupPanel> ();

		// нажатие кнопки меню обрабатывается в Game:Start
		
	}

	public void Configure(bool success) {
		winText.gameObject.SetActive(success);
		loseText.gameObject.SetActive(!success);
		nextLevelButton.gameObject.SetActive(success);
	}
}
