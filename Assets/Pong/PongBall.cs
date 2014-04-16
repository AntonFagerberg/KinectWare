using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {
	private Vector3 direction;
	private Vector3 bounceNormal;
	private float speedMultiplier;
	
	void Start () {
		transform.position = new Vector3(0f, 5f, 0f);
		direction = Vector3.right.normalized;
		speedMultiplier = 0.15f;
	}
	
	void Update () {
		transform.rotation = Quaternion.identity;
		transform.position += direction * speedMultiplier;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

		if (transform.position.x > 15f || transform.position.x < -15f) {
			Start();
		}
	}

	void OnCollisionEnter(Collision collision) {
		bounceNormal = collision.contacts[0].normal;

		if (collision.collider.name == "Paddle Right") {
			audio.Play();
			direction = new Vector3(1f, transform.position.y - collision.collider.transform.position.y, 0f);
		} else if (collision.collider.name == "Paddle Left") {
			audio.Play();
			direction = new Vector3(-1f, transform.position.y - collision.collider.transform.position.y, 0f);
		}

		direction = Vector3.Reflect(direction, bounceNormal);
	}
}
