using UnityEngine;
using System.Collections;

public class PongPaddle : MonoBehaviour {
	public Transform hand;

	void Start () { }
	
	void Update () {
		transform.position = new Vector3(transform.position.x, 1.25f + 15 * hand.position.y, transform.position.z);

		if (transform.position.y > 8.75f) {
			transform.position = new Vector3(transform.position.x, 8.75f, transform.position.z);
		} else if (transform.position.y < 1.25f) {
			transform.position = new Vector3(transform.position.x, 1.25f, transform.position.z);
		}
	}
}
