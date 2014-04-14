using UnityEngine;
using System.Collections;

public class ArkBall : MonoBehaviour {
	
	public int speed;
	
	private Vector3 direction;
	private Vector3 bounceNormal;
	private float speedMultiplier;
	
	void Start () {
		transform.position = new Vector3(0f, 0f, 0f);
		direction = new Vector3(0.1f, -1f, 0f).normalized;
		speedMultiplier = 0.1f;
	}
	
	void Update () {
		transform.rotation = Quaternion.identity;
		transform.position += direction * speedMultiplier * speed;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

		if (transform.position.y < -15f) {
			Start();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		bounceNormal = collision.contacts[0].normal;
		
		if (collision.collider.name == "Paddle") {
			direction = new Vector3(transform.position.x - collision.collider.transform.position.x, direction.y, 0f).normalized;
		} else if (collision.collider.name != "Left Wall" && collision.collider.name != "Right Wall" && collision.collider.name != "Roof") {
			Destroy(collision.collider.gameObject);
		}

		audio.Play();
		
		direction = Vector3.Reflect(direction, bounceNormal);
	}
}
