using UnityEngine;
using System.Collections;

public class MonkeyBallBlockEnemy : MonoBehaviour {
	public float distance;
	public bool negate;
	Vector3 position;
	Vector3 movement;
	float x = 0f;

	// Use this for initialization
	void Start () {
		position = transform.position;
		movement = new Vector3(0f, 0f, distance);
	}
	
	// Update is called once per frame
	void Update () {
		x = (x + 0.05f) % (2 * Mathf.PI);
		transform.position = position + movement * Mathf.Sin(x) * (negate ? -1f : 1f);

	}
}
