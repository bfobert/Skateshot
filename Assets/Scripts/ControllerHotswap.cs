using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ControllerHotswap : MonoBehaviour {

	GameObject resetGO;
	private Button reset;
	//Set this later for variable # of chars
	//private int numOfPlayers = 4;
	private GameObject[] players = new GameObject[4];

	void Start(){
		//gets array of all players
		for (int i = 1; i < players.Length + 1; i++) {
			players [i-1] = GameObject.Find ("Player " + i);
		}
		resetGO = GameObject.FindGameObjectWithTag ("Reset");
		resetGO.SetActive (false);
		TeamSwap ();
	}

	void Update () {
	}

	//Written by Tanner
	//Shuffles array of team names, swaps teams, then calls itself.
	public IEnumerator TeamSwap(){
		yield return new WaitForSeconds (30);
		string[] teamStrings = new string[]{"Player1", "Player1", "Player2", "Player2"};
		for (int i = 0; i < teamStrings.Length; i++)
		{
			int rand = Random.Range(0, teamStrings.Length / 2);
			string x = teamStrings[i];
			string y = teamStrings[rand];
			teamStrings[i] = y;
			teamStrings[rand] = x;
		}
		for (int i = 0; i < teamStrings.Length; i++) {
			Player pControl = (Player)players[i].GetComponent<Player> ();
			pControl.TeamChange (teamStrings [i]);
		}
		TeamSwap ();
	}


	//Checks if a player was hit. If they are, update life and score accordingly
	//If a player hits 20 score, display win message
	public void PlayerHit(GameObject playerGOHit, GameObject playerGOShoot){
		Player playerHit = (Player)playerGOHit.GetComponent<Player> ();
		Player playerShoot = (Player)playerGOShoot.GetComponent<Player> ();

		if (playerHit.GetLife() > 0) {
			playerHit.SetLife(playerHit.GetLife() - 25);
		}
		if (playerHit.GetLife() > 25) {
			playerHit.SetLifeText("Life: " + playerHit.GetLife());
		} else {
			playerHit.SetLifeText("Life: " + playerHit.GetLife() + "!");
		}
		if (playerHit.GetLife() <= 0) {
			Debug.Log (playerGOShoot.tag + " killed " + playerGOHit.tag);
			//Player instantly respawns, so this never displays
			//playerHit.getLifeText().text = "Dead!";
			if (playerGOShoot.tag == playerGOHit.tag) {
				playerShoot.SetScore (playerShoot.GetScore () - 1);
			} else {
				playerShoot.SetScore (playerShoot.GetScore () + 1);
				GameObject[] team = GameObject.FindGameObjectsWithTag (playerGOShoot.tag);
				foreach (GameObject player in team) {
					Player pControl = (Player)player.GetComponent<Player> ();
					pControl.SetScore (pControl.GetScore () + 1);
					pControl.SetScoreText ("Score: " + pControl.GetScore ());
				}
			}
			playerHit.Respawn ();
			playerHit.SetLife (100);
			playerHit.SetLifeText ("Life: " + playerHit.GetLife ());
			if (playerShoot.GetScore () >= 10) {
				foreach (GameObject player in players) {
					Player pControl = (Player)player.GetComponent<Player> ();
					pControl.SetEndText(playerGOShoot.name + " wins!");
					pControl.SetScoreEndText ("Final Score: " + pControl.GetScore ());
				}
			}
			//Lock Screen on death
			//No use for demo!!!
			/*GameObject p = GameObject.FindGameObjectWithTag ("Player");
			Destroy (p.GetComponent<MouseLook>());
			Destroy (p.GetComponent<FPSInput> ());
			Destroy (Camera.main.GetComponent<MouseLook>());
			Destroy (Camera.main.GetComponent<RayShooter>());
			Cursor.lockState = CursorLockMode.None;
			*/
		}
	}
}