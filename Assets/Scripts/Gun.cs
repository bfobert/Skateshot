using UnityEngine;
using System.Collections;

abstract public class Gun : MonoBehaviour {

	protected GameObject bulletPrefab;
	protected GameObject player;
	protected int playerNum;
	public Camera camera;
	protected int ammo;
	protected int respawnTime;
	protected int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Gun(){
	}

	public void SetOwner(GameObject bullet, GameObject p, int pNum){
		bulletPrefab = bullet;
		player = p;
		playerNum = pNum;
		camera = p.GetComponentInChildren<Camera>();
	}

	public virtual void Shoot(){
		Vector3 origin = new Vector3 (.5f,
			.5f,
			0);
		Ray ray = camera.ViewportPointToRay (origin);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				GameObject bullet = Instantiate (bulletPrefab) as GameObject;
				bullet.transform.position = hit.point;
				bullet.transform.localScale = new Vector3(0.25f,0.25f,0.25f);
				Bullet bulletScript = (Bullet)bullet.GetComponent<Bullet>();
				bulletScript.setOwner (player);

				//Destroy (bullet);
			}
	}

	public int GetAmmo(){
		return ammo;
	}

	public int GetTime(){
		return respawnTime;
	}
}
