using UnityEngine;
using System.Collections;

public class MonkeyBallWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)) {
			transform.Rotate(Vector3.forward);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.Rotate(Vector3.back);
		}

		if (Input.GetKey(KeyCode.LeftArrow)) {
			transform.Rotate(Vector3.left);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			transform.Rotate(Vector3.right);
		}
	}
}
