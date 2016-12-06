using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	[SerializeField] private int playerNum;
	public enum RotationAxes
	{
		MouseXandY = 0,
		MouseX = 1,
		MouseY = 2
	}
	// Setting for how the mouse should look
	public RotationAxes axes = RotationAxes.MouseXandY;

	// General settings - sensitivity
	[SerializeField] private float sensitivityHor = 9.0f;
	[SerializeField] private float sensitivityVert = 9.0f;
	// General settings - y rotation constraints
	[SerializeField] private float maxVert = 45f;
	[SerializeField] private float minVert = -45f;


	private float rotationX = 0; 

	// Controls the players ability to look around
	void Update () {
		float currentXRot = transform.localEulerAngles.x;
		float currentYRot = transform.localEulerAngles.y;
		float xRot = getXRot ();
		float yRot = getYRot ();
		if (axes == RotationAxes.MouseY)
			yRot = currentYRot;
		if (axes == RotationAxes.MouseX)
			xRot = currentXRot;
		transform.localEulerAngles = new Vector3 (xRot, yRot, 0);
	}

	public float getXRot(){
		float newX = rotationX - Input.GetAxis ("Joy" + playerNum + "_RightStickVertical") * sensitivityVert;
		newX = Mathf.Clamp (newX, minVert, maxVert); 
		rotationX = newX;
		return newX;
	}

	public float getYRot(){
		float newY = transform.localEulerAngles.y + Input.GetAxis ("Joy" + playerNum + "_RightStickHorizontal") * sensitivityHor;
		return newY;
	}
}
