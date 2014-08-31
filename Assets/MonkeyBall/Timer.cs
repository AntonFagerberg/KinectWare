using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
	private float maxTime;
	private float elapsedTime = 0f;
	private bool runTime = false;

	void Start () {
		maxTime = 100f - 10 * ((5 - PlayerPrefs.GetInt("Lives") + PlayerPrefs.GetInt("Score")) / (Application.levelCount - 2));
		if (maxTime < 30f) {
			maxTime = 30f;
		}
		
		transform.guiText.text = "" + (elapsedTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (runTime) {
			elapsedTime += Time.deltaTime;
		}

		transform.guiText.text = "" + (maxTime - elapsedTime).ToString("0.00");

		if (transform.position.y < -15f || elapsedTime > maxTime) {
			PlayerPrefs.SetInt("Won",0);
			PlayerPrefs.SetInt("Lives", PlayerPrefs.GetInt("Lives")-1);
			
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
	}
}
