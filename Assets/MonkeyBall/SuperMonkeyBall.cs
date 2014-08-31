using UnityEngine;
using System.Collections;

public class SuperMonkeyBall : MonoBehaviour {
	public int speed;
	public Transform torso, head;
	private bool runTime = false;

	Vector3 direction = Vector3.zero;

	void Update () {
		direction = torso.transform.position - head.transform.position;
		direction.y = 0;

		if (runTime) {
			rigidbody.AddTorque(direction * speed);
		}

		if (transform.position.y < -15f) {
			PlayerPrefs.SetInt("Won",0);
			PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives")-1);
			
			Application.LoadLevel(1);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.collider.name == "Goal") {
			PlayerPrefs.SetInt("Won",1);
			PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score")+1);
			
			Application.LoadLevel(1);
		}
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
