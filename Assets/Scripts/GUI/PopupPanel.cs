using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PopupPanel : MonoBehaviour {

	private const float minScale = 0.0001f;
	private const float minAlpha = 0.0001f;

	public enum TweenType {
		None = 0,
		Move,
		Scale,
		Fade
	};

	public enum MoveDirections {
		Left = 0,
		Right,
		Top,
		Bottom,
		LeftToRight,
		RightToLeft,
		TopToBottom,
		BottomToTop
	}

    [Header("Popup Panel Options")]
	public bool scaleTimeToZero = true;
    public bool closeOnBackButton = true;    
    [Header("Fade")]
    public Button fade;
    public bool closeOnFadeClick = true;
    [Header("Close Button")]
    public Button closeButton;
    [Header("Transition")]
	public TweenType tweenType = TweenType.Scale;
	public LeanTweenType easeType = LeanTweenType.linear;
	public float tweenTime = 0.5f;
	public MoveDirections direction;
	#if UNITY_EDITOR
	[HideInInspector]
	public bool expandEvents;
	#endif	    
	[SerializeField]
	public UnityEvent onOpenPanel, onPanelOpened, onClosePanel, onPanelClosed;
		
	private CanvasGroup mainGroup, fadeGroup;
	private RectTransform rectTransform, fadeRectTransform;
	private Vector2 canvasSize;

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="PopupPanel"/> is showing.
	/// </summary>
	/// <value><c>true</c> if is showing; otherwise, <c>false</c>.</value>
	public bool isShowing {
		get {
			return gameObject.activeSelf;
		}
		set {
			if (value) OpenPanel ();
			else ClosePanel ();
		}
	}

	void Awake () {
		canvasSize = GetComponentInParent<Canvas>().GetComponent<RectTransform> ().rect.size;
	}

	private float resumeTimeScale;
    protected virtual void Init() {        
		if (scaleTimeToZero) {
            resumeTimeScale = Time.timeScale;
            Time.timeScale = 0f;

        }        

		if (fade && fade.gameObject.activeSelf) {
            fade.onClick.RemoveAllListeners();
            if (closeOnFadeClick)
                fade.onClick.AddListener(() => ClosePanel());
            else
                fade.onClick.AddListener(delegate { });
        }

        if (closeButton && closeButton.gameObject.activeSelf) {
            closeButton.onClick.RemoveAllListeners();
            closeButton.onClick.AddListener(() => ClosePanel());
        }


        
		if (fade && fade.gameObject.activeSelf) {
			fadeRectTransform = fade.GetComponent<RectTransform>();
            fadeGroup = fade.gameObject.GetComponent<CanvasGroup>();
            if (fadeGroup == null) fadeGroup = fade.gameObject.AddComponent<CanvasGroup>();            
            if (tweenType == TweenType.Fade) {
				fadeGroup.alpha = 1f;
				fadeGroup.ignoreParentGroups = false;
			}
			else
				fadeGroup.alpha = 0f;
        }

		rectTransform = GetComponent<RectTransform>();
		mainGroup = GetComponent<CanvasGroup>();

		switch (tweenType) {
            case TweenType.None:
				rectTransform.anchoredPosition = Vector2.zero;
				transform.localScale = Vector3.one;				
				if (mainGroup != null) 
					mainGroup.alpha = 1f;
				if (fade && fade.gameObject.activeSelf) {
					fade.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
					fade.transform.localScale = Vector3.one;
					fadeGroup.alpha = 1f;
				}
                break;

            case TweenType.Move:
				isHorizontal = (direction == MoveDirections.Left || direction == MoveDirections.LeftToRight || direction == MoveDirections.Right || direction == MoveDirections.RightToLeft);				
				aPos = rectTransform.anchoredPosition;
				switch (direction) {
					case MoveDirections.Left:
					case MoveDirections.LeftToRight:
						aPos.x = -canvasSize.x;
						break;
					case MoveDirections.Right:
					case MoveDirections.RightToLeft:
						aPos.x = canvasSize.x;
						break;
					case MoveDirections.Top:
					case MoveDirections.TopToBottom:
						aPos.y = canvasSize.y;
						break;
					case MoveDirections.Bottom:
					case MoveDirections.BottomToTop:
						aPos.y = -canvasSize.y;
						break;
				}
				rectTransform.anchoredPosition = aPos;
				if (fade && fade.gameObject.activeSelf) {					
					aPosFade = fadeRectTransform.anchoredPosition;
					aPosFade.x = - aPos.x;
					fadeRectTransform.anchoredPosition = aPosFade;
				}
                break;

            case TweenType.Scale:
                transform.localScale = Vector3.one * minScale;
				if (fade && fade.gameObject.activeSelf) 
					fade.transform.localScale = Vector3.one / minScale;
                break;

            case TweenType.Fade:                
				mainGroup = GetComponent<CanvasGroup>();
				if (mainGroup == null) mainGroup = gameObject.AddComponent<CanvasGroup>();
				mainGroup.alpha = 0f;
                break;
        }
    }

	bool isOpening = false;
	TweenType resumeTweenType;
	/// <summary>
	/// Показать панель.
	/// </summary>
	/// <param name="withoutTween">При <c>true</c> открыть без анимации.</param>
	public virtual void OpenPanel (bool withoutTween = false) {        
		if (isShowing) return;
		if (isOpening) return;

		if (withoutTween) {
			resumeTweenType = tweenType;
			tweenType = TweenType.None;
			onPanelOpened.AddListener(() => { tweenType = resumeTweenType; });
		}

		isOpening = true;
        Init();        
		gameObject.SetActive(true);

		if (tweenType != TweenType.Fade && tweenType != TweenType.None && fade && fade.gameObject.activeSelf)
			TweenFadeIn ();

		onOpenPanel.Invoke();

		switch (tweenType) {
		case TweenType.None:
			OnOpened();
			break;
		case TweenType.Move:
			TweenMovePanelIn();
			break;
		case TweenType.Scale:            
			TweenScalePanelIn(); 
			break;
		case TweenType.Fade:
			TweenFadePanelIn();
			break;
		}
	}

	bool isClosing = false;
	/// <summary>
	/// Закрыть панель.
	/// </summary>
	public virtual void ClosePanel () {        
		if (!isShowing) return;
		if (isClosing) return;

		isClosing = true;

		if (closeButton && closeButton.gameObject.activeSelf) 
			closeButton.onClick.RemoveAllListeners();

	

		switch (tweenType) {
		    case TweenType.None:
				OnClosed();
				break;
		    case TweenType.Move:
				TweenMovePanelOut();
			    break;
		    case TweenType.Scale:
				TweenScalePanelOut();
			    break;
		    case TweenType.Fade:
				TweenFadePanelOut();
			    break;
		}

		if (tweenType != TweenType.Fade && tweenType != TweenType.None && fade && fade.gameObject.activeSelf)
			TweenFadeOut ();

		onClosePanel.Invoke();
	}

	protected virtual void OnOpened () {
		isOpening = false;
		onPanelOpened.Invoke();
	}

	protected virtual void OnClosed () {
		isClosing = false;
		gameObject.SetActive (false);
		if (scaleTimeToZero) {
			Time.timeScale = resumeTimeScale;
		}
		onPanelClosed.Invoke();
	}

	void OnDestroy () {
		if (isShowing && scaleTimeToZero) {
			Time.timeScale = resumeTimeScale;
		//	if (NTMusic.instance.audio != null)
			//	NTMusic.instance.UnPause();
			//NTSound.UnpauseAllLoops();
		}
	}

	/*
	 * Helpers
	 */

	Transform found;
	public GameObject GetGameObject (string name) {
		found = transform.FindChild(name);
		if (found == null) {
			for (int i = 0; i < transform.childCount; i++) {
				found = transform.GetChild(i).FindChild(name);
				if (found != null)
					break;
			}
		}
		return found.gameObject;
	}

	public Text GetLabel (string name) {
		return GetGameObject(name).GetComponent<Text>();
	}

	public Button GetButton (string name) {
		return GetGameObject(name).GetComponent<Button>();
	}

	public Toggle GetToggle (string name) {
		return GetGameObject(name).GetComponent<Toggle>();
	}

	public Slider GetSlider (string name) {
		return GetGameObject(name).GetComponent<Slider>();
	}

	/*
	 * Editor
	 */
	#if UNITY_EDITOR
	[CanEditMultipleObjects]
	[CustomEditor(typeof(PopupPanel))]
	public class PopupPanelInspector : Editor {
		string[] propertyFilter1 = new string[] {"m_Script", "direction", "onOpenPanel", "onClosePanel", "onPanelOpened", "onPanelClosed"};
		string[] propertyFilter2 = new string[] {"m_Script", "direction", "scaleTimeToZero", "closeOnBackButton", "fade", "closeOnFadeClick", "closeButton", "tweenType", "easeType", "tweenTime"};

		SerializedProperty tweenType, direction, expandEvents;
		string[] values;

		void OnEnable () {
			expandEvents = serializedObject.FindProperty("expandEvents");
			tweenType = serializedObject.FindProperty("tweenType");
			direction = serializedObject.FindProperty("direction");
			values = new string[8];
			for (int i = 0; i < values.Length; i++)
				values[i] = ((MoveDirections)i).ToString();
		}
		
		public override void OnInspectorGUI () {
			serializedObject.Update();

			DrawPropertiesExcluding(serializedObject, propertyFilter1);
			if (tweenType == null) return;
			if ((TweenType)tweenType.intValue == TweenType.Move) {
				direction.intValue = EditorGUILayout.Popup("Direction", direction.intValue, values);
			}
			expandEvents.boolValue = EditorGUILayout.Foldout(expandEvents.boolValue, "Events");
			if (expandEvents.boolValue) {
				DrawPropertiesExcluding(serializedObject, propertyFilter2);
			}
			
			serializedObject.ApplyModifiedProperties();
		}
	}
	
	[MenuItem("GameObject/UI/Popup Panel", false, 3000)]
	static void UICreatePanel() {
		CreatePopupPanel("Popup Panel");
	}	
	
	public static void CreatePopupPanel (string name) {
		// TODO
		Debug.LogError("Не реализовано");
	}
	#endif

	/*
	 * Tweens
	 */

	// Fade tweens

	void TweenFadeIn () {
		LeanTween.value(fade.gameObject, SetFadeAlpha, 0f, 1f, tweenTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
	}
	void TweenFadeOut () {
		LeanTween.value(fade.gameObject, SetFadeAlpha, fadeGroup.alpha, 0f, tweenTime).setEase(LeanTweenType.linear).setIgnoreTimeScale(true);
	}
	void SetFadeAlpha(float alpha) {
		fadeGroup.alpha = alpha;
	}

	// Panel Tweens

	// Move
	void TweenMovePanelIn () {
		float startValue = 0f;
		switch (direction) {
		case MoveDirections.Left:
		case MoveDirections.LeftToRight:
			startValue = -canvasSize.x;
			break;
		case MoveDirections.Right:
		case MoveDirections.RightToLeft:
			startValue = canvasSize.x;
			break;
		case MoveDirections.Top:
		case MoveDirections.TopToBottom:
			startValue = canvasSize.y;
			break;
		case MoveDirections.Bottom:
		case MoveDirections.BottomToTop:
			startValue = -canvasSize.y;
			break;
		}
		LeanTween.value(gameObject, SetMovePanel, startValue, 0f, tweenTime).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnOpened);
	}
	void TweenMovePanelOut () {
		float endValue = 0f;
		switch (direction) {
		case MoveDirections.Left:
		case MoveDirections.RightToLeft:
			endValue = -canvasSize.x;
			break;
		case MoveDirections.Right:
		case MoveDirections.LeftToRight:
			endValue = canvasSize.x;
			break;
		case MoveDirections.Top:
		case MoveDirections.BottomToTop:
			endValue = canvasSize.y;
			break;
		case MoveDirections.Bottom:
		case MoveDirections.TopToBottom:
			endValue = -canvasSize.y;
			break;
		}
		LeanTween.value(gameObject, SetMovePanel, isHorizontal ? rectTransform.anchoredPosition.x : rectTransform.anchoredPosition.y, endValue, tweenTime)
			.setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnClosed);
	}
	bool isHorizontal;
	Vector2 aPos, aPosFade;
	void SetMovePanel (float a) {
		if (isHorizontal)
			aPos.x = a;
		else 
			aPos.y = a;
		rectTransform.anchoredPosition = aPos;
		if (fade && fade.gameObject.activeSelf) {
			if (isHorizontal)
				aPosFade.x = -a;
			else 
				aPosFade.y = -a;
			fadeRectTransform.anchoredPosition = aPosFade;
		}
	}

	// Scale
	void TweenScalePanelIn () {
		LeanTween.value(gameObject, SetScalePanel, Vector3.one * minScale, Vector3.one, tweenTime).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnOpened);
	}
	void TweenScalePanelOut () {
		LeanTween.value(gameObject, SetScalePanel, transform.localScale, Vector3.one * minScale, tweenTime).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnClosed);
	}
	Vector3 fscale;
	void SetScalePanel (Vector3 scale) {
		transform.localScale = scale;
		if (fade && fade.gameObject.activeSelf) {
			fscale.x = 1f / scale.x;
			fscale.y = 1f / scale.y;
			fscale.z = 1f / scale.z;
			fade.transform.localScale = fscale;
		}
	}

	// Fade
	void TweenFadePanelIn () {
		LeanTween.value(gameObject, SetMainAlpha, 0f, 1f, tweenTime).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnOpened);
	}
	void TweenFadePanelOut () {
		LeanTween.value(gameObject, SetMainAlpha, mainGroup.alpha, 0f, tweenTime).setEase(easeType).setIgnoreTimeScale(true).setOnComplete(OnClosed);
	}
	void SetMainAlpha(float alpha) {
		mainGroup.alpha = alpha;
	}
}
