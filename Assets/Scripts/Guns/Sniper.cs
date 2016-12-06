using UnityEngine;
using System.Collections;

public class Sniper : Gun {

	float time = 1.3f;
	float fireTime = 1.2f;

	void Start() {
	}

	void Update(){
		time += Time.deltaTime;
	}

	public Sniper() {
		ammo = 5;
		damage = 100;
	}

	public override void Shoot(){
		Player p = player.GetComponent<Player> ();
		int pAmmo = p.GetAmmo (gameObject.tag);
		if (pAmmo > 0) {
			if (time > fireTime) {
				Vector3 origin = new Vector3 (.5f,
					                 .5f,
					                 0);
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
				time = 0;
				pAmmo = pAmmo - 1;
				p.SetAmmo (this, pAmmo);
				p.UpdateAmmoText (gameObject.tag);
				Debug.Log ("ammo: " + pAmmo);
			}
		} else {
			p.ResetGun ();
		}

	}
}

