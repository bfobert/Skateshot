using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	private int level = 0;

	// Use this for initialization
	void Start () {
	
	}

	//Written by Patrick
	public void setLevel(Dropdown d){
		level = d.value;
	}

	public void LoadScene (string sceneName){
		
		SceneManager.LoadScene (sceneName);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
