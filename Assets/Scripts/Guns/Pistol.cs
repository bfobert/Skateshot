using UnityEngine;
using System.Collections;

public class Pistol : Gun {

	[SerializeField] private AudioClip clip;
	[SerializeField] Transform playerTransform;

	void Start() {
	}
//
	public Pistol(){
		damage = 25;
	}

	void Update() {
//		if (Input.GetButtonDown ("Joy" + playerNum + "_Fire")) {
		//AudioSource.PlayClipAtPoint (clip, this.gameObject.transform.position);
//		}
	}

	public override void Shoot(){
		if (Input.GetButtonDown ("Joy" + playerNum + "_Fire")) {
			Debug.Log (damage);
//			AudioSource.PlayClipAtPoint (clip, playerTransform.position);
			Vector3 origin = new Vector3 (.5f,
				.5f, 0);
			Ray ray = camera.ViewportPointToRay (origin);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				GameObject bullet = Instantiate (bulletPrefab) as GameObject;
				bullet.transform.position = hit.point;
				bullet.transform.localScale = new Vector3 (0.25f, 0.25f, 0.25f);
				Bullet bulletScript = (Bullet)bullet.GetComponent<Bullet> ();
				bulletScript.setOwner (player);
				bulletScript.setDamage (damage);

				//Destroy (bullet);
			}
		}
	}
}
