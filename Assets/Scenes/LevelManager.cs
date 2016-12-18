using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager {

	// прогресс уровней
	private List<Level> _levelsPrefs;

	// баланс уровней
	private LevelsInfo _levelsInfo;

	private bool isInited = false;

	// максимально доступное количество уровней
	public const int maxLevels = 100;

	private int _levelsTotal = 10;
	/**
	 * Общее количество уровней
	 **/
	public int levelsTotal {
		get {
			return _levelsTotal;
		}
		set {
			if (value > 0){
				if (value != _levelsTotal){
					_levelsTotal = value;
					Reload();
				}
			}
		}
	}

	private int _currentLevel;
	/**
	 * Текущий уровень
	 **/
	public int currentLevel {
		get {
			return _currentLevel;
		}
		set {
			if (value >= 0 && value < _levelsTotal && value != _currentLevel){
				if (value == 0){
					ClearVisitedLevels();
				}
				_currentLevel = value;
				GetLevelPrefs(_currentLevel).visited = true;

				PlayerPrefs.SetInt ("currentLevel", _currentLevel);

				SavePrefs();

			}
		}
	}
	/**
	 * Возвращает список индексов уровней, доступных для перехода из данного уровня
	 * */
	public List<int> GetLevelTransitions(int levelIndex){
		if (levelIndex >= 0 && levelIndex < _levelsTotal) {
			return _levelsInfo.transitions[levelIndex];
		}
		Debug.Log("Warning! LevelManager level transitions not found. levelIndex = "+levelIndex);
		return null;
	}
	/**
	 * Возвращает прогресс прохождения уровня и переходы на другие уровни 
	 * */
	public Level GetLevelPrefs(int levelIndex){
		if (levelIndex >= 0 && levelIndex < _levelsTotal) {
			return _levelsPrefs[levelIndex];
		}
		Debug.Log("Warning! LevelManager level prefs not found. levelIndex = "+levelIndex);
		return null;
	}
	/**
	 * Возвращает прогресс прохождения текущего уровня и переходы на другие уровни  
	 * */
	public Level currentLevelPrefs {
		get {
			if (_currentLevel >= 0 && _currentLevel < _levelsPrefs.Count){
				return _levelsPrefs[_currentLevel];
			}
			Debug.Log("Warning! current Level prefs not found. CurrentLevel = "+_currentLevel);
			return null;
		}
	}
	/**
	 * Возвращает баланс текущего уровня из levels.xml
	 * */
	public LevelInfo currentLevelInfo {
		get {
			if (_currentLevel >= 0 && _currentLevel < _levelsInfo.levels.Count){
				return _levelsInfo.levels[_currentLevel];
			}
			Debug.Log("Warning! current LevelInfo not found. CurrentLevel = "+_currentLevel);
			return null;
		}
	}
	/**
	 * Возвращает баланс уровня из levels.xml по индексу
	 * */
	public LevelInfo GetLevelInfo (int levelIndex) {
		return _levelsInfo.levels[levelIndex];
	}

	private static LevelManager _instance;
	/**
	 * Менеджер уровней
	 * */
	public static LevelManager instance	{
		get {
			if (_instance == null) {
				_instance = new  LevelManager ();
			}
			return _instance;
		}
	}

	public LevelManager(){
		ParseXml ();

		InitPrefs ();
		LoadPrefs ();
	}

	/**
	 * Сбросить настройки прохождения уровней
	 * */
	public void Reset(){
		InitPrefs (true);
		LoadPrefs ();
	}

	public void Reload ()
	{
		LoadPrefs ();
	}

	void ClearVisitedLevels ()
	{
		for (int i = 0; i < _levelsPrefs.Count; i++) {
			_levelsPrefs[i].visited = false;
		}
	}

	private void InitPrefs (bool reset = false)
	{
		bool [] levelsAvailable = PlayerPrefsX.GetBoolArray ("levelsAvalable");
		if (levelsAvailable.Length == 0 || reset) {
			levelsAvailable = new bool[maxLevels];
			bool [] levelsCompleted = new bool[maxLevels];
			bool [] levelsVisited = new bool[maxLevels];
			int [] levelsStars = new int[maxLevels];

			for (int i = 0; i < maxLevels; i++) {
				levelsAvailable[i] = (i == 0);
				levelsCompleted[i] = false;
				levelsVisited[i] = false;
				levelsStars[i] = 0;
			}
			PlayerPrefsX.SetBoolArray ("levelsAvalable", levelsAvailable);
			PlayerPrefsX.SetBoolArray ("levelsCompleted", levelsCompleted);
			PlayerPrefsX.SetBoolArray ("levelsVisited", levelsVisited);
			PlayerPrefsX.SetIntArray ("levelsStars", levelsStars);

			PlayerPrefs.SetInt ("currentLevel", 0);
		} 

		if (!PlayerPrefs.HasKey ("currentLevel")) {
			PlayerPrefs.SetInt ("currentLevel", 0);
		} 

	}

	private void LoadPrefs(){

		_currentLevel = PlayerPrefs.GetInt ("currentLevel");

		bool [] levelsAvailable = PlayerPrefsX.GetBoolArray ("levelsAvalable");
		bool [] levelsCompleted = PlayerPrefsX.GetBoolArray ("levelsCompleted");
		bool [] levelsVisited = PlayerPrefsX.GetBoolArray ("levelsVisited");
		int [] levelsStars = PlayerPrefsX.GetIntArray ("levelsStars");

		_levelsPrefs = new List<Level> ();
		for (int i = 0; i < levelsTotal; i++) {
			_levelsPrefs.Add(new Level(levelsAvailable[i], levelsCompleted[i], levelsStars[i], levelsVisited[i]));
			if (i < levelsTotal - 1){
				if (_levelsInfo.transitions != null && i < _levelsInfo.transitions.Count){
					_levelsPrefs[i].transitions = _levelsInfo.transitions[i];
				} else {
					_levelsPrefs[i].transitions = new List<int>();
					_levelsPrefs[i].transitions.Add(i+1);
				}
			} else {
				_levelsPrefs[i].transitions = null;
			}
		}
	}

	private void SavePrefs ()
	{
		bool [] levelsAvailable = PlayerPrefsX.GetBoolArray ("levelsAvalable");
		bool [] levelsCompleted = PlayerPrefsX.GetBoolArray ("levelsCompleted");
		bool [] levelsVisited = PlayerPrefsX.GetBoolArray ("levelsVisited");
		int [] levelsStars = PlayerPrefsX.GetIntArray ("levelsStars");

		for (int i = 0; i < levelsTotal; i++) {
			levelsAvailable[i] = _levelsPrefs[i].available;
			levelsCompleted[i] = _levelsPrefs[i].completed;
			levelsVisited[i] = _levelsPrefs[i].visited;
			levelsStars[i] = _levelsPrefs[i].stars;
		}

		PlayerPrefsX.SetBoolArray ("levelsAvalable", levelsAvailable);
		PlayerPrefsX.SetBoolArray ("levelsCompleted", levelsCompleted);
		PlayerPrefsX.SetBoolArray ("levelsVisited", levelsCompleted);
		PlayerPrefsX.SetIntArray ("levelsStars", levelsStars);
		PlayerPrefs.Save();
	}

	void ParseXml ()
	{
		_levelsInfo = new LevelsInfo ("levels");
	}

	/**
	 * Завершить текущий уровень и сохранить звёзды
	 * */
	public void CompleteLevel(int stars = 0){
		// сохранить прогресс уровня
		Level lvl = currentLevelPrefs;
		if (lvl.stars < stars) {
			lvl.Update (true, true, stars);
		} else {
			lvl.Update (true, true);
		}
		// разблокировать уровни
		List<int> trans = currentLevelPrefs.transitions;
		if (trans != null) {
			for (int i = 0; i < trans.Count; i++) {
				GetLevelPrefs(trans[i]).Update(true);
			}
		}
		// сохранить в PalyerPrefs
		SavePrefs ();
	}

	/**
	 * Перевести указатель currentLevel на следующий доступный уровень после currentLevel
	 * возвращает false если нет не пройденных уровней и ставит currentLevel = 0
	 * */
	public bool NextLevel() {
		// перебрать все переходы и выбрать первый доступный
		List<int> trans = currentLevelPrefs.transitions;

		if (trans != null){
			for(int i = 0; i < trans.Count; i++){
				if (!GetLevelPrefs(trans[i]).visited){
					currentLevel = trans[i];
					SavePrefs();
					return true;
				}
			}
		}
		// если нет доступного уровня из текущего, то перейти на первый открытый не пройденный уровень
		// если нет не пройденных уровней - перейти на нулевой уровень
		int nextLvl = FindNotVisitedLevel (0);
		if (nextLvl >= 0) {
			currentLevel = nextLvl;
			//SavePrefs();
			return true;
		} else {
			currentLevel = 0;
			//SavePrefs();
			return false;
		}
	}

	int FindNotVisitedLevel (int levelIndex)
	{
		Level lvl = GetLevelPrefs(levelIndex);
		if (lvl.available && !lvl.visited) {
			return levelIndex;
		}
		List<int> trans = lvl.transitions;
		if (trans != null) {
			int nextLvl;
			for(int i = 0; i < trans.Count; i++){
				nextLvl = FindNotVisitedLevel(trans[i]);
				if (nextLvl >= 0){
					return nextLvl;
				}
			}
		}
		return -1;
	}



}
