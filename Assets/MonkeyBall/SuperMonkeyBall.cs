using UnityEngine;
using System.Collections;

public class SuperMonkeyBall : MonoBehaviour {
	public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			rigidbody.AddTorque(Vector3.forward * speed);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			rigidbody.AddTorque(Vector3.back * speed);
		}
		
		if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.AddTorque(Vector3.left * speed);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			rigidbody.AddTorque(Vector3.right * speed);
		}
	}
}
