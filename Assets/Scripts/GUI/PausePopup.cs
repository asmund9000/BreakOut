using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(PopupPanel))]
public class PausePopup : MonoBehaviour {

	public Button menuButton, replayButton;

	[HideInInspector]
	public PopupPanel panel;
	
	void Awake () {
		panel = GetComponent<PopupPanel>();



	}

	/*
	void LoadLevels () {
		Main.CreateActionList();
		Main.AddBackAction(() => {
			Main.LoadSceneS("Menu");	
		});
		Application.LoadLevel("LevelsGrid");
	}//*/

	#if UNITY_EDITOR
	[CanEditMultipleObjects]
	[CustomEditor(typeof(PausePopup))]
	public class PausePopupInspector : Editor {
		string[] propertyFilter = new string[] {"m_Script"};
		public override void OnInspectorGUI () {
			serializedObject.Update();			
			DrawPropertiesExcluding(serializedObject, propertyFilter);
			serializedObject.ApplyModifiedProperties();
		}
	}
	#endif
}
