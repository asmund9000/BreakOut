using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level  {

	/**
	 * Список переходов на уровни, доступные из текущего
	 * */
	public List<int> transitions = null;

	private bool _available = false;
	/**
	 * Открыт ли уровень
	 * */
	public bool available {
		get {
			return _available;
		}
	}

	private bool _completed = false;
	/**
	 * Пройден ли уровень
	 * */
	public bool completed {
		get {
			return _completed;
		}
	}

	private int _stars = 0;
	/**
	 * Количество звёзд: от 0 до 3
	 * */
	public int stars {
		get {
			return _stars;
		}
	}

	public bool visited = false;

	public Level(bool available, bool completed, int stars, bool visited){
		Update (available, completed, stars, visited);
	}

	public void Update (bool available, bool completed, int stars, bool visited){
		_available = available;
		_completed = completed;
		_stars = stars;
		this.visited = visited;

	}
	public void Update (bool available, bool completed, int stars){
		_available = available;
		_completed = completed;
		_stars = stars;
	}

	public void Update (bool available, bool completed){
		_available = available;
		_completed = completed;
	}

	public void Update (bool available){
		_available = available;
	}

}
