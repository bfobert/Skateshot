using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

	GameObject resetGO;
	private Button reset;
	//Set this later for variable # of chars
	public int numOfPlayers;
	//TODO: Fix number of players
	private List<GameObject> players = new List<GameObject> ();

	void Start(){
		//gets array of all players
		for (int i = 1; i < numOfPlayers + 1; i++) {
				players [i-1] = GameObject.Find ("Player " + i);
			}
//		resetGO = GameObject.FindGameObjectWithTag ("Reset");
//		resetGO.SetActive (false);
	}


	//Checks if a player was hit. If they are, update life and score accordingly
	//If a player hits 20 score, display win message
	public void PlayerHit(GameObject playerGOHit, GameObject playerGOShoot, int damage){
		Player playerHit = (Player)playerGOHit.GetComponent<Player> ();
		Player playerShoot = (Player)playerGOShoot.GetComponent<Player> ();

		if (playerHit.GetLife() > 0) {
			playerHit.SetLife(playerHit.GetLife() - damage);
		}
		if (playerHit.GetLife() > 25) {
			playerHit.SetLifeText("Life: " + playerHit.GetLife());
		} else {
			playerHit.SetLifeText("Life: " + playerHit.GetLife() + "!");
		}
		if (playerHit.GetLife() <= 0) {
			Debug.Log (playerGOShoot.name + " killed " + playerGOHit.name);
			//Player instantly respawns, so this never displays
			//playerHit.getLifeText().text = "Dead!";
			if (playerGOShoot.tag == playerGOHit.tag) {
				playerShoot.SetScore (playerShoot.GetScore () - 1);
			} else {
				playerShoot.SetScore (playerShoot.GetScore () + 1);
			}
			playerShoot.SetScoreText ("Score: " + playerShoot.GetScore ());
			playerHit.Respawn ();
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