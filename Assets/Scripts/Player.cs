using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	[SerializeField] private Text scoreText;
	[SerializeField] private Text ammoText;
	[SerializeField] private Text lifeText;
	[SerializeField] private Text endText;
	[SerializeField] private Text scoreEndText;
	[SerializeField] private int playerNum;
	[SerializeField] private GameObject playerGO;
	[SerializeField] private GameObject bullet;
	private Dictionary<string, int> ammo = new Dictionary<string, int>();
	private Gun gun = new Pistol ();
	public GameObject[] guns;

	private int life = 100;
	private int score = 0;

	private bool hasTeleported = false;
	private bool inPresent = true;

	void Start(){
		lifeText.text = "Life: " + life;
		scoreText.text = "Score: " + score;
		Cursor.lockState = CursorLockMode.Locked;
		gun.SetOwner (bullet, playerGO, playerNum);
		guns = GameObject.FindGameObjectsWithTag ("Gun" + playerNum);
		foreach (GameObject gunModel in guns) {
			if (gunModel.name.Equals ("Pistol")) {
				gunModel.SetActive (true);
			} else {
				gunModel.SetActive (false);
			}
		}
		BuildAmmoDictionary ();
	}
		

	void Update(){
		PlayerShoot ();
	}

	public void BuildAmmoDictionary(){
		ammo.Clear ();
		ammo.Add ("Pistol", 1);
		ammo.Add ("MachineGun", 0);
		ammo.Add ("Shotgun", 0);
	}

	public void SetAmmo(Gun g, int ammoAmt){
		ammo [g.gameObject.tag] = ammoAmt;
	}

	public int GetPlayerNum(){
		return playerNum;
	}

	public void SetPlayerNum(int i){
		playerNum = i;
	}

	public int GetLife(){
		return life;
	}

	public void SetLife(int l){
		life = l;
	}

	public int GetScore(){
		return score;
	}

	public void SetScore(int s){
		score = s;
	}

	public Text GetScoreText(){
		return scoreText;
	}

	public void SetScoreText(string t){
		scoreText.text = t;
	}

	public Text GetLifeText(){
		return lifeText;
	}

	public void SetLifeText(string t){
		lifeText.text = t;
	}

	public int GetAmmo(string key){
		return ammo[key];
	}

	public void UpdateAmmoText(string key){
		ammoText.text = "Ammo: " + ammo[key].ToString();
	}

	public Text GetEndText(){
		return endText;
	}

	public void SetEndText(string t){
		endText.text = t;
	} 

	public Text GetScoreEndText(){
		return scoreEndText;
	}

	public void SetScoreEndText(string t){
		scoreEndText.text = t;
	}

	public bool GetTeleportStatus(){
		return hasTeleported;
	}

	public bool GetTimeStatus(){
		return inPresent;
	}

	public void SetTimeStatus(string timezone){
		if(timezone.Equals("Present")){
			inPresent = true;
		} else{
			inPresent = false;
		}
	}

	public void TeamChange(string playerNum){
		gameObject.tag = playerNum;
	}

	public IEnumerator CheckTeleport(){
		hasTeleported = true;
		yield return new WaitForSeconds (3);
		hasTeleported = false;

	}

	public void WeaponSwap(Gun g){
		g.SetOwner (bullet, playerGO, playerNum);
		gun = g;
		foreach (GameObject gunModel in guns) {
			if (gunModel.name.Equals(g.gameObject.tag)) {
				gunModel.SetActive (true);
			} else {
				gunModel.SetActive (false);
			}
		}
	}

	//Written by Tanner
	public void Suicide(){
		Respawn ();
		life = 100;
		lifeText.text = "Life: " + life;
		score--;
		scoreText.text = "Score: " + score;
	}

	public void PlayerShoot(){
		if (Input.GetButton ("Joy" + playerNum + "_Fire")) {
			gun.Shoot ();
		}
	}
		
	//Written by Tanner
	public void Respawn(){
		BuildAmmoDictionary ();
		GameObject[] respawns = GameObject.FindGameObjectsWithTag ("Respawn");
		int i = Random.Range (1, respawns.Length);
		gameObject.transform.position = respawns [i].transform.position;
		life = 100;
		lifeText.text = ("Life: " + life);
		ammoText.text = ("Ammo: ∞");
		foreach (GameObject gunModel in guns) {
			if (gunModel.name.Equals ("Pistol")) {
				gunModel.SetActive (true);
			} else {
				gunModel.SetActive (false);
			}
		}
		gun = new Pistol ();
		gun.SetOwner (bullet, playerGO, playerNum);
		}
	}
		
