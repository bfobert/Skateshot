using UnityEngine;
using System.Collections;

public class CheckMap : MonoBehaviour {

	void OnTriggerEnter(Collider coll){
		GameObject collidedWith = coll.gameObject;
		int playerCount = 1;
		while (playerCount < 5) {
			if (collidedWith.tag == "Player" + playerCount) {
				Player player = collidedWith.GetComponent<Player> ();
				player.SetTimeStatus(gameObject.tag);
				}
			playerCount++;
			}
		}
	}