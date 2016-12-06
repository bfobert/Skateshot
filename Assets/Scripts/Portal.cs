using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {
	
	[SerializeField] GameObject linkPortal;

	void OnTriggerEnter(Collider coll){
		GameObject collidedWith = coll.gameObject;
		int playerCount = 1;
		while (playerCount < 5) {
			if (collidedWith.tag == "Player" + playerCount) {
				Player player = collidedWith.GetComponent<Player> ();
				//Checks if the player has recently teleported
				if (player.GetTeleportStatus () == false) {
					StartCoroutine (player.CheckTeleport ());
					Vector3 destination = linkPortal.transform.position;
					collidedWith.transform.position = destination;
				}
			}
			playerCount++;
		}
	}
}
