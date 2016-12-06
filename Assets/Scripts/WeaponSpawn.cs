using UnityEngine;
using System.Collections;

public class WeaponSpawn : MonoBehaviour {
	[SerializeField] private GameObject gun;
	private int respawnTime = 20;

	public WeaponSpawn ()
	{
		/*Gun g = gun.GetComponent (typeof(Gun)) as Gun;
		respawnTime = g.GetTime ();*/
	}

	void OnTriggerEnter(Collider coll){
		int i = 1;
		while (i < 5){
			if(coll.tag == "Player" + i){
				GameObject gunGOClone = Instantiate (gun);
				GameObject playerGO = coll.gameObject;
				Player p = playerGO.GetComponent<Player>();
				Gun g = gunGOClone.GetComponent (typeof(Gun)) as Gun;
				Debug.Log (g.ToString ());
				p.SetAmmo (g, g.GetAmmo());
				p.UpdateAmmoText (gameObject.tag);
				p.WeaponSwap(g);
				StartCoroutine(Respawn ());
			}
			i++;
		}
	}

	public IEnumerator Respawn(){
		gun.SetActive (false);
		yield return new WaitForSeconds (respawnTime);
		gun.SetActive (true);
	}

}


