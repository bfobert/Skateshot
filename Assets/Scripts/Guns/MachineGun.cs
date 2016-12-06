using UnityEngine;
using System.Collections;

public class MachineGun : Gun {

	float time = .21f;
	float fireTime = .15f;
	private int i =0;
	void Start(){
	}
	
	public MachineGun(){
		ammo = 100;
		damage = 20;
	}

	public override void Shoot(){
		Player p = player.GetComponent<Player> ();
		int pAmmo = p.GetAmmo (gameObject.tag);
		if (time > fireTime) {
			if (pAmmo > 0) {
				Vector3 origin = new Vector3 (.5f,
					               .5f,
					               0);
				Ray ray = camera.ViewportPointToRay (origin);
				RaycastHit hit;
				if (Physics.Raycast (ray, out hit)) {
					GameObject bullet = Instantiate (bulletPrefab) as GameObject;
					bullet.transform.position = hit.point;
					Debug.Log (hit.point + " " + i);
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
				}
			}
		time += Time.deltaTime;
		i++;
		}
	}

