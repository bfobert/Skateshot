using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float movementSpeed;
	private GameObject owner;
	private int damage;


	// Use this for initialization
	void Start () {
		
	}

	// Allows the ball to be shot as a projectile vs teleporting the ball
	void Update () {
		Transform posT = transform;
		Vector3 pos = transform.position;
		pos += posT.forward * Time.deltaTime * movementSpeed;
		transform.position = pos;
	}
	//Controls the collisions 
	public void OnCollisionEnter(Collision coll){
		Debug.Log ("coll enter");
		GameObject collidedWith = coll.gameObject;
		int playerCount = 1;
		Debug.Log (collidedWith.tag);
		while (playerCount < 5) {
			if (collidedWith.tag == "Player" + playerCount) {
				Debug.Log ("Player Hit");
				GameObject control = GameObject.FindGameObjectWithTag ("Control");
				Controller c = control.GetComponent<Controller> ();
				c.PlayerHit (collidedWith, owner, damage);
			}
			playerCount++;
		}
		Destroy (gameObject);
	}

	public void setOwner(GameObject o){
		owner = o;
	}

	public void setDamage(int i){
		damage = i;
	}
}