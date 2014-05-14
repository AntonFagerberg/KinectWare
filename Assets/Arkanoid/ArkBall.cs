using UnityEngine;
using System.Collections;

public class ArkBall : MonoBehaviour {
	private Vector3 direction;
	private Vector3 bounceNormal;
	private float speedMultiplier;
	private bool runTime = false;
	private float waitUntil = 2f;
	private float time = 0f;
	
	void Start () {
		transform.position = new Vector3(0f, 0f, 0f);
		direction = new Vector3(0.1f, 1f, 0f).normalized;
		speedMultiplier = 0.1f;
	}
	
	void Update () {
		if (time <= waitUntil) {
			time += Time.deltaTime;
		}

		if (runTime && time > waitUntil) {
			transform.rotation = Quaternion.identity;
			transform.position += direction * speedMultiplier;
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		}

		if (transform.position.y < -5f) {
			PlayerPrefs.SetInt("Won",0);
			PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives")-1);
			Application.LoadLevel(1);
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		bounceNormal = collision.contacts[0].normal;
		
		if (collision.collider.name == "Paddle") {
			direction = new Vector3(transform.position.x - collision.collider.transform.position.x, direction.y, 0f).normalized;
		} 
		else if (collision.collider.name != "Left Wall" && collision.collider.name != "Right Wall" && collision.collider.name != "Roof") {
			Destroy(collision.collider.gameObject);
		}
		else if (collision.collider.name == "Roof"){
			PlayerPrefs.SetInt("Won",1);
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") +1);

			Application.LoadLevel(1);

		}

		audio.Play();
		
		direction = Vector3.Reflect(direction, bounceNormal);
	}

	void Zig_Update(ZigInput zig) {
		runTime = false;
		if (zig.TrackedUsers.Count > 0) {
			foreach (ZigTrackedUser u in zig.TrackedUsers.Values) {
				if (u.SkeletonTracked) {
					runTime = true;
				}
			}
		}
		rigidbody.isKinematic = !runTime;
	}
}
