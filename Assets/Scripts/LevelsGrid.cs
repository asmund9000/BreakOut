using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelsGrid : MonoBehaviour {

	[Header("Levels")]
	public GridLayoutGroup grid;
	public bool showStars;
	public bool allAvaliable;
	public int totalLevels;
	public int buttonSize = 240;
	public GameObject buttonPrefab;

	protected void Awake () {
	

		// first start
		if (LevelManager.instance.levelsTotal != totalLevels) {
			LevelManager.instance.levelsTotal = totalLevels;
		}

		LevelManager.instance.Reload();
	}

	public void RespawnButtons () {
		// delete old buttons
		GameObject[] children = new GameObject[grid.transform.childCount];
		for (int i = 0; i < children.Length; i++)
			children[i] = grid.transform.GetChild(i).gameObject;
		foreach (GameObject child in children)
			DestroyImmediate(child);
		
		// spawn new
		LevelManager.instance.levelsTotal = totalLevels;
		for (int i = 0; i < totalLevels; i++) {
			AddLevelButton(i);
		}
	}
	
	protected virtual void AddLevelButton (int levelIndex) {
		if (buttonPrefab == null) {
			Debug.LogError("Добавьте префаб кнопки");
			return;
		}


		GameObject newButton = Instantiate(buttonPrefab) as GameObject;
		newButton.transform.SetParent(grid.transform, false);
		newButton.transform.SetSiblingIndex(levelIndex);
		newButton.name = "LevelButton" + (levelIndex+1);

        LevelButton button = newButton.GetComponent<LevelButton>();
        if (button != null)
        {
            button.levelIndex = levelIndex;

        #if UNITY_EDITOR
                    if (!EditorApplication.isPlaying)
                button.Init();
        #endif
        }

    }

	#if UNITY_EDITOR
	[CustomEditor(typeof(LevelsGrid))]
	public class LevelsGridInspector : Editor {

		LevelsGrid instance;

		string[] propertyFilter = new string[] {"m_Script", "totalLevels", "buttonSize"};

		void OnEnable () {
			instance = (LevelsGrid)target;

			if (instance.grid == null) {
				instance.grid = instance.GetComponentInChildren<GridLayoutGroup>();
			}		
		}

		public override void OnInspectorGUI () {
			if (EditorApplication.isPlaying)
				return;

			serializedObject.Update();			
			DrawPropertiesExcluding(serializedObject, propertyFilter);
			serializedObject.ApplyModifiedProperties();

			EditorGUILayout.Space();
			int totalLevels = EditorGUILayout.IntSlider("Number Of Levels", instance.totalLevels, 1, LevelManager.maxLevels);
			if (totalLevels != instance.totalLevels) {
				instance.totalLevels = totalLevels;
				LevelManager.instance.levelsTotal = totalLevels;
				instance.RespawnButtons();
			}

			EditorGUILayout.Space();
			int size = EditorGUILayout.IntSlider("Level Button Size", instance.buttonSize, 50, 500);
			if (size != instance.buttonSize) {
				instance.buttonSize = size;
				Vector2 prefabSize = instance.buttonPrefab.GetComponent<RectTransform>().sizeDelta;
				float prefabAspect = prefabSize.x / prefabSize.y;
				instance.grid.cellSize = new Vector2(size, (int)(size / prefabAspect));
				instance.RespawnButtons();
			}

			EditorGUILayout.Space();
			if (GUILayout.Button("Respawn Buttons"))
				instance.RespawnButtons();
		}
	}
	#endif
}
