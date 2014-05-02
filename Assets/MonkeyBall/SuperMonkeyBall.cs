using UnityEngine;
using System.Collections;

public class SuperMonkeyBall : MonoBehaviour {
	public int speed;
	public Transform torso, head;

	Vector3 direction = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		direction = torso.transform.position - head.transform.position;
		direction.y = 0;
		rigidbody.AddTorque(direction * speed);
	}
}
