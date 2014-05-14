using UnityEngine;
using System.Collections;

public class PongBall : MonoBehaviour {
	private Vector3 direction;
	private Vector3 bounceNormal;
	private float speedMultiplier;
	private int bounces;
	private bool runTime = false;
	private float waitUntil = 2f;
	private float time = 0f;
	public GUIText score;
	
	void Start () {
		direction = Vector3.right.normalized;
		speedMultiplier = 0.15f;
		bounces = 5 + 5 * ((5 - PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Score")) / (Application.levelCount - 2));
		score.text = "" + bounces;
	}
	
	void Update () {
		if (time <= waitUntil) {
			time += Time.deltaTime;
		}

		transform.rotation = Quaternion.identity;
		if (runTime && time > waitUntil) {
			transform.position += direction * speedMultiplier;
			transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		}

		if (transform.position.x > 15f || transform.position.x < -15f) {
			PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives")-1);
			PlayerPrefs.SetInt("Won",0);
			Application.LoadLevel(1);
		}
	}

	void OnCollisionEnter(Collision collision) {
		bounceNormal = collision.contacts[0].normal;

		if (collision.collider.name == "Paddle Right") {
			bounces--;
			audio.Play();
			direction = new Vector3(1f, transform.position.y - collision.collider.transform.position.y, 0f);
		} else if (collision.collider.name == "Paddle Left") {
			bounces--;
			audio.Play();
			direction = new Vector3(-1f, transform.position.y - collision.collider.transform.position.y, 0f);
		}

		score.text = "" + bounces;

		if (bounces <= 0) {
			PlayerPrefs.SetInt("Won",1);
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
			
			Application.LoadLevel(1);
		}


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
