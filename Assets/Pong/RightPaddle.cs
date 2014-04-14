using UnityEngine;
using System.Collections;

public class RightPaddle : MonoBehaviour {

	private float speed;

	void Start () {
		speed = 0.2f;	
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed;
		}

		if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed;
		}

		if (transform.position.y > 8.75f) {
			transform.position = new Vector3(transform.position.x, 8.75f, transform.position.z);
		} else if (transform.position.y < 1.25f) {
			transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
		}
	}
}
