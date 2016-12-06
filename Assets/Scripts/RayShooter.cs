using UnityEngine;
using System.Collections;

public class RayShooter : MonoBehaviour {

	public GameObject bulletPrefab;
	[SerializeField] private GameObject player;
	[SerializeField] private int playerNum;
	private Camera camera;


	void Start () {
		camera = GetComponent<Camera> ();

		Cursor.lockState = CursorLockMode.Locked; 
	}
	void Update () {
		if (Input.GetButtonDown ("Joy" + playerNum + "_Fire")) {
			Vector3 origin = new Vector3 (camera.pixelWidth / 2,
				camera.pixelHeight / 2,
				0);
			Ray ray = camera.ScreenPointToRay (origin);
			Debug.Log (ray);
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
	}
}
	//Mechanics fixed by Mark
//	void Update () {
//		if (Input.GetButtonDown ("Joy" + playerNum + "_Fire")) {
//			Vector3 origin = new Vector3 (camera.pixelWidth / 2,
//				camera.pixelHeight / 2,
//				0);
//			Ray ray = camera.ScreenPointToRay (origin);
//			RaycastHit hit;
//			if(Physics.Raycast(ray, out hit)){
//				StartCoroutine (Shoot (hit.point));    
//			}
//		}
//	}

//	private IEnumerator Shoot(Vector3 position){
//		GameObject bullet = Instantiate (bulletPrefab) as GameObject;
//		bullet.transform.position = transform.position;
//		bullet.transform.rotation = transform.rotation;
//		Bullet bulletScript = (Bullet)bullet.GetComponent<Bullet>();
//		bulletScript.setOwner (player);
//		yield return new WaitForSeconds (5);
//		Destroy (bullet);
//	} 


