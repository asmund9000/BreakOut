using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour {

    public Button playButton;
    // Use this for initialization
    void Start()
    {
       
        playButton.onClick.AddListener(() =>
        { 
            SceneManager.LoadScene("Levels");
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
