using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {
	
	private Vector2 facingAngle = new Vector2();
	
	// Use this for initialization
	void Start () {
		transform.eulerAngles = facingAngle;
	}
	
	// Update is called once per frame
	void Update () {
		facingAngle.x -= Input.GetAxis("Mouse Y") * 2f;
		facingAngle.y += Input.GetAxis("Mouse X") * 5f;
		transform.position += (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"))* Time.deltaTime * 20f;
		transform.eulerAngles = facingAngle;
	}
}
